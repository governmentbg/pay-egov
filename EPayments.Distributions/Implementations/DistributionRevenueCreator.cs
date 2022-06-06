using EPayments.Common.Data;
using EPayments.Distributions.Interfaces;
using EPayments.Model.Enums;
using EPayments.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading;

namespace EPayments.Distributions.Implementations
{
    public class DistributionRevenueCreator : IDistributionRevenueCreatable, IDisposable
    {
        private readonly int BoricaTransactionsToTake;
        private readonly IUnitOfWork UnitOfWork;

        private bool _isDisposed = false;
        private HashSet<EserviceClient> cashedClients = new HashSet<EserviceClient>();

        public DistributionRevenueCreator(
            IUnitOfWork unitOfWork,
            int boricaTransactionsToTake)
        {
            this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork is null");
            BoricaTransactionsToTake = boricaTransactionsToTake;
        }

        public List<DistributionRevenue> Distribute(CancellationToken token)
        {
            List<DistributionRevenue> distributionRevenues = null;

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                
                List<BoricaTransaction> boricaTransactions = this.UnitOfWork.DbContext.Set<BoricaTransaction>()
                    .Include(bt => bt.PaymentRequests)
                    .Where(bt => bt.TransactionStatusId == (int)BoricaTransactionStatusEnum.TaxReceived)
                    .OrderBy(bt => bt.BoricaTransactionId)
                    .Take(BoricaTransactionsToTake)
                    .ToList();

                if (boricaTransactions == null || boricaTransactions.Count == 0)
                {
                    break;
                }

                List<PaymentRequest> payments = boricaTransactions
                    .SelectMany(bt => bt.PaymentRequests.Where(pr => pr.ObligationStatusId == ObligationStatusEnum.Paid))
                    .ToList();

                if (payments.Count > 0)
                {
                    if (distributionRevenues == null)
                    {
                        distributionRevenues = new List<DistributionRevenue>();
                    }
                    
                    this.UpdateClientsCash(payments);
                    DateTime now = DateTime.Now.ToUniversalTime();

                    Dictionary<int, HashSet<DistributionRevenuePayment>> distributionsById = this.GetDistributionsByDistributionId(payments,
                        boricaTransactions);

                    foreach (int distributionTypeId in distributionsById.Keys)
                    {
                        if (distributionsById[distributionTypeId].Count > 0)
                        {
                            DistributionRevenue distributionRevenue = distributionRevenues
                                .FirstOrDefault(dr => dr.DistributionTypeId == distributionTypeId);

                            if (distributionRevenue == null)
                            {
                                distributionRevenue = new DistributionRevenue()
                                {
                                    CreatedAt = DateTime.Now.ToUniversalTime(),
                                    IsFileGenerated = false,
                                    IsDistributed = false,
                                    DistributionTypeId = distributionTypeId
                                };

                                distributionRevenues.Add(distributionRevenue);
                            }

                            foreach (var key in distributionsById[distributionTypeId])
                            {
                                if (!distributionRevenue.DistributionRevenuePayments.Any(drp => drp.DistributionRevenuePaymentId == key.DistributionRevenuePaymentId))
                                {
                                    distributionRevenue.DistributionRevenuePayments.Add(key);
                                    distributionRevenue.TotalSum += key.PaymentRequest.PaymentAmount;
                                }
                            }
                        }
                    }
                }

                using (var transaction = UnitOfWork.BeginTransaction())
                {
                    if (payments.Count > 0)
                    {
                        foreach (PaymentRequest paymentRequest in payments)
                        {
                            paymentRequest.ObligationStatusId = ObligationStatusEnum.ForDistribution;
                        }
                    }

                    foreach (BoricaTransaction boricaTransaction in boricaTransactions)
                    {
                        boricaTransaction.TransactionStatusId = (int)BoricaTransactionStatusEnum.Distributed;
                    }

                    if (distributionRevenues != null && distributionRevenues.Count > 0)
                    {
                        IEnumerable<DistributionRevenue> newDistributionRevenues =
                            distributionRevenues.Where(dr => this.UnitOfWork.DbContext.Entry(dr).State == EntityState.Detached);

                        if (newDistributionRevenues != null && newDistributionRevenues.Count() > 0)
                        {
                            foreach (DistributionRevenue newDistributionRevenue in newDistributionRevenues)
                            {
                                this.UnitOfWork.DbContext.Entry(newDistributionRevenue).State = EntityState.Added;
                            }
                        }
                    }

                    this.UnitOfWork.Save();

                    transaction.Commit();
                }
            }

            return distributionRevenues;
        }

        public void Dispose()
        {
            if (!this._isDisposed)
            {
                cashedClients = null;
                this._isDisposed = !this._isDisposed;
            }
        }

        private Dictionary<int, HashSet<DistributionRevenuePayment>> GetDistributionsByDistributionId(
            IEnumerable<PaymentRequest> paymentRequests, 
            List<BoricaTransaction> boricaTransactions)
        {
            Dictionary<int, HashSet<DistributionRevenuePayment>> distributionsById = 
                new Dictionary<int, HashSet<DistributionRevenuePayment>>();

            foreach (PaymentRequest paymentRequest in paymentRequests)
            {
                DistributionRevenuePayment distributionRevenuePayment = this.CreateModel(paymentRequest, 
                    boricaTransactions.First(bt => bt.PaymentRequests.Contains(paymentRequest)));

                if (!distributionsById.TryGetValue(distributionRevenuePayment.EserviceClient.DistributionTypeId, 
                    out HashSet<DistributionRevenuePayment> distributionRevenuePayments))
                {
                    distributionRevenuePayments = new HashSet<DistributionRevenuePayment>();
                    distributionsById.Add(distributionRevenuePayment.EserviceClient.DistributionTypeId, distributionRevenuePayments);
                }

                distributionRevenuePayments.Add(distributionRevenuePayment);
            }

            return distributionsById;
        }

        private void UpdateClientsCash(IEnumerable<PaymentRequest> payments)
        {
            HashSet<int> ids = new HashSet<int>(payments.Select(pr => pr.EserviceClientId));

            IEnumerable<EserviceClient> clients = this.UnitOfWork.DbContext
                .Set<EserviceClient>()
                .Where(ec => ids.Contains(ec.EserviceClientId));

            this.cashedClients.UnionWith(clients);
        }

        private EserviceClient GetEserviceClient(int id)
        {
            EserviceClient client = this.cashedClients.FirstOrDefault(c => c.EserviceClientId == id);

            if (client != null)
            {
                return client;
            }

            client = this.UnitOfWork.DbContext.Set<EserviceClient>()
                .SingleOrDefault(ec => ec.EserviceClientId == id);

            if (client == null)
            {
                return null;
            }

            this.cashedClients.Add(client);

            return client;
        }

        private DistributionRevenuePayment CreateModel(PaymentRequest paymentRequest, BoricaTransaction boricaTransaction)
        {
            EserviceClient currentClient = this.GetEserviceClient(paymentRequest.EserviceClientId);

            if (currentClient == null)
            {
                throw new InvalidOperationException(string.Format("Eservice client with id {0} was not found.", paymentRequest.EserviceClientId));
            }

            while (true)
            {
                if (currentClient.AggregateToParent == false)
                {
                    return new DistributionRevenuePayment()
                    {
                        EserviceClientId = currentClient.EserviceClientId,
                        EserviceClient = currentClient,
                        DistributionRevenuePaymentId = paymentRequest.PaymentRequestId,
                        PaymentRequest = paymentRequest,
                        BoricaTransactionId = boricaTransaction.BoricaTransactionId,
                        BoricaTransaction = boricaTransaction
                    };
                }
                else
                {
                    if (currentClient.ParentId == null)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Eservice client with id {0} does not have parent.",
                            currentClient.EserviceClientId));
                    }

                    currentClient = this.GetEserviceClient((int)currentClient.ParentId);

                    if (currentClient == null)
                    {
                        throw new NullReferenceException(string.Format(
                            "Eservice client with id {0} does not exist.",
                            currentClient.ParentId));
                    }
                }
            }
        }
    }
}
