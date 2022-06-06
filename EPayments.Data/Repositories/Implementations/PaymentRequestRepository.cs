using EPayments.Common.Data;
using EPayments.Common.Linq;
using EPayments.Data.Repositories.Interfaces;
using EPayments.Data.ViewObjects.Admin;
using EPayments.Model.Enums;
using EPayments.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPayments.Data.Repositories.Implementations
{
    public class PaymentRequestRepository : IPaymentRequestRepository
    {
        private readonly IUnitOfWork UnitOfWork;

        public PaymentRequestRepository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork is null");
        }
        
        public async Task<int> CountPaymentRequests(string filterPaymentIdentifier,
            string filterPaymentReferenceNumber,
            DateTime? filterDateFrom, 
            DateTime? filterDateTo, 
            decimal? filterAmountFrom, 
            decimal? filterAmountTo, 
            string filterServiceProvider, 
            string filterPaymentReason, 
            PaymentRequestStatus? paymentRequestStatus, 
            ObligationStatusEnum? obligationStatus, 
            string applicantName, 
            string applicantUin)
        {
            var predicate = CreatePaymentRequestPredicate(
                filterPaymentIdentifier,
                filterPaymentReferenceNumber,
                filterDateFrom,
                filterDateTo,
                filterAmountFrom,
                filterAmountTo,
                filterServiceProvider,
                filterPaymentReason,
                paymentRequestStatus,
                obligationStatus,
                applicantName,
                applicantUin);

            return await this.UnitOfWork.DbContext.Set<PaymentRequest>()
                .Where(predicate)
                .CountAsync();
        }

        public async Task<List<PaymentRequestVO>> GetPaymentRequests(string filterPaymentIdentifier,
            string filterPaymentReferenceNumber,
            DateTime? filterDateFrom,
            DateTime? filterDateTo,
            decimal? filterAmountFrom,
            decimal? filterAmountTo,
            string filterServiceProvider,
            string filterPaymentReason,
            PaymentRequestStatus? filterRequestStatus,
            ObligationStatusEnum? obligationStatus,
            string applicantName,
            string applicantUin,
            string sortBy,
            bool sortDescending,
            int page,
            int resultsPerPage)
        {
            var predicate = CreatePaymentRequestPredicate(filterPaymentIdentifier,
                filterPaymentReferenceNumber,
                filterDateFrom,
                filterDateTo,
                filterAmountFrom,
                filterAmountTo,
                filterServiceProvider,
                filterPaymentReason,
                filterRequestStatus,
                obligationStatus,
                applicantName,
                applicantUin);

            return await this.SortPaymentRequests(this.UnitOfWork.DbContext.Set<PaymentRequest>()
                .Where(predicate), sortBy, sortDescending)
                .Skip((page - 1) * resultsPerPage)
                .Take(resultsPerPage)
                .Select(PaymentRequestVO.Map)
                .ToListAsync();
        }

        public async Task<List<PaymentRequestVO>> GetAllRequests(string filterPaymentIdentifier, 
            string filterPaymentReferenceNumber, 
            DateTime? filterDateFrom, 
            DateTime? filterDateTo, 
            decimal? filterAmountFrom, 
            decimal? filterAmountTo, 
            string filterServiceProvider, 
            string filterPaymentReason, 
            PaymentRequestStatus? filterRequestStatus, 
            ObligationStatusEnum? obligationStatus, 
            string applicantName, 
            string applicantUin, 
            string sortBy, 
            bool sortDescending)
        {
            var predicate = CreatePaymentRequestPredicate(filterPaymentIdentifier,
                filterPaymentReferenceNumber,
                filterDateFrom,
                filterDateTo,
                filterAmountFrom,
                filterAmountTo,
                filterServiceProvider,
                filterPaymentReason,
                filterRequestStatus,
                obligationStatus,
                applicantName,
                applicantUin);

            return await this.SortPaymentRequests(this.UnitOfWork.DbContext.Set<PaymentRequest>()
                .Where(predicate), sortBy, sortDescending)
                .Select(PaymentRequestVO.Map)
                .ToListAsync();
        }

        private Expression<Func<PaymentRequest, bool>> CreatePaymentRequestPredicate(string filterPaymentIdentifier,
            string filterPaymentReferenceNumber,
            DateTime? filterDateFrom,
            DateTime? filterDateTo,
            decimal? filterAmountFrom,
            decimal? filterAmountTo,
            string filterServiceProvider,
            string filterPaymentReason,
            PaymentRequestStatus? paymentRequestStatus,
            ObligationStatusEnum? obligationStatus,
            string applicantName,
            string applicantUin)
        {
            var predicate = PredicateBuilder.True<PaymentRequest>();

            if (!String.IsNullOrWhiteSpace(filterPaymentIdentifier))
            {
                predicate = predicate.And(e => e.PaymentRequestIdentifier == filterPaymentIdentifier);
            }

            if (!String.IsNullOrWhiteSpace(filterPaymentReferenceNumber))
            {
                predicate = predicate.And(e => e.PaymentReferenceNumber == filterPaymentReferenceNumber);
            }

            if (filterDateFrom.HasValue)
            {
                predicate = predicate.And(e => e.PaymentRequestStatusChangeTime >= filterDateFrom.Value);
            }

            if (filterDateTo.HasValue)
            {
                predicate = predicate.And(e => e.PaymentRequestStatusChangeTime <= filterDateTo.Value);
            }

            if (filterAmountFrom.HasValue)
            {
                predicate = predicate.And(e =>
                    ((e.PaymentRequestStatusId == PaymentRequestStatus.Authorized ||
                      e.PaymentRequestStatusId == PaymentRequestStatus.Ordered ||
                      e.PaymentRequestStatusId == PaymentRequestStatus.Paid) ? e.PaymentAmount : 0) >= filterAmountFrom.Value);
            }

            if (filterAmountTo.HasValue)
            {
                predicate = predicate.And(e =>
                    ((e.PaymentRequestStatusId == PaymentRequestStatus.Authorized ||
                      e.PaymentRequestStatusId == PaymentRequestStatus.Ordered ||
                      e.PaymentRequestStatusId == PaymentRequestStatus.Paid) ? e.PaymentAmount : 0) <= filterAmountTo.Value);
            }

            if (!String.IsNullOrWhiteSpace(filterServiceProvider))
            {
                predicate = predicate.AndStringContains(e => e.ServiceProviderName, filterServiceProvider);
            }

            if (!String.IsNullOrWhiteSpace(filterPaymentReason))
            {
                predicate = predicate.AndStringContains(e => e.PaymentReason, filterPaymentReason);
            }

            if (paymentRequestStatus.HasValue)
            {
                predicate = predicate.And(e => e.PaymentRequestStatusId == paymentRequestStatus.Value);
            }

            if (obligationStatus.HasValue)
            {
                predicate = predicate.And(e => e.ObligationStatusId == obligationStatus.Value);
            }

            if (!String.IsNullOrWhiteSpace(applicantUin))
            {
                predicate = predicate.And(e => e.ApplicantUin == applicantUin);
            }

            if (!String.IsNullOrWhiteSpace(applicantName))
            {
                predicate = predicate.AndStringContains(e => e.ApplicantName, applicantName);
            }

            if (!String.IsNullOrWhiteSpace(applicantUin))
            {
                predicate = predicate.And(e => e.ApplicantUin == applicantUin);
            }

            return predicate;
        }

        private IQueryable<PaymentRequest> SortPaymentRequests(
            IQueryable<PaymentRequest> query, 
            string sortBy, 
            bool sortDescending)
        {
            switch (sortBy)
            {
                case nameof(PaymentRequest.CreateDate):
                    query = sortDescending == false ? 
                        query.OrderBy(pr => pr.CreateDate) : 
                        query.OrderByDescending(pr => pr.CreateDate);
                    break;
                case nameof(PaymentRequest.ApplicantName):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.ApplicantName) :
                        query.OrderByDescending(pr => pr.ApplicantName);
                    break;
                case nameof(PaymentRequest.PaymentRequestIdentifier):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.PaymentRequestIdentifier) :
                        query.OrderByDescending(pr => pr.PaymentRequestIdentifier);
                    break;
                case nameof(PaymentRequest.ExpirationDate):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.ExpirationDate) :
                        query.OrderByDescending(pr => pr.ExpirationDate);
                    break;
                case nameof(PaymentRequest.ServiceProviderName):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.ServiceProviderName) :
                        query.OrderByDescending(pr => pr.ServiceProviderName);
                    break;
                case nameof(PaymentRequest.PaymentReason):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.PaymentReason) :
                        query.OrderByDescending(pr => pr.PaymentReason);
                    break;
                case nameof(PaymentRequest.PaymentAmount):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.PaymentAmount) :
                        query.OrderByDescending(pr => pr.PaymentAmount);
                    break;
                case nameof(PaymentRequest.PaymentRequestStatusId):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.PaymentRequestStatusId) :
                        query.OrderByDescending(pr => pr.PaymentRequestStatusId);
                    break;
                case nameof(PaymentRequest.PaymentReferenceNumber):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.PaymentReferenceNumber) :
                        query.OrderByDescending(pr => pr.PaymentReferenceNumber);
                    break;
                case nameof(PaymentRequest.ObligationStatusId):
                    query = sortDescending == false ?
                        query.OrderBy(pr => pr.ObligationStatusId) :
                        query.OrderByDescending(pr => pr.ObligationStatusId);
                    break;
            }

            return query;
        }

    }
}
