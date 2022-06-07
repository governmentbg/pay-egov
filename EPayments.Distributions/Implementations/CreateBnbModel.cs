using EPayments.Distributions.Enums;
using EPayments.Distributions.Interfaces;
using EPayments.Distributions.Models.BNB;
using EPayments.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace EPayments.Distributions.Implementations
{
    public class CreateBnbModel : ICreateBnbModel
    {
        private int counter = 1;

        public BnbFile Create(DistributionRevenue distributionRevenue,
            string bulstat,
            string senderName,
            string iban,
            string bicCode,
            string vpn,
            string vd,
            string firstDescription,
            string secondDescription)
        {
            Dictionary<int, EserviceClient> clientsById = new Dictionary<int, EserviceClient>();
            
            foreach (DistributionRevenuePayment distributionRevenuePayment in distributionRevenue.DistributionRevenuePayments)
            {
                if (!clientsById.ContainsKey(distributionRevenuePayment.EserviceClient.EserviceClientId))
                {
                    clientsById.Add(distributionRevenuePayment.EserviceClient.EserviceClientId, distributionRevenuePayment.EserviceClient);
                }
            }

            return new BnbFile()
            {
                Header = new BnbHeader()
                {
                    RefId = distributionRevenue.DistributionRevenueId.ToString(),
                    TimeStamp = distributionRevenue.CreatedAt,
                    Sender = bulstat,
                    SenderName = senderName,
                },
                Accounts = new List<BnbAccount>()
                {
                    new BnbAccount()
                    {
                        Acc = iban,
                        Bic = bicCode,
                        Do = distributionRevenue.TotalSum,
                        Documents = new List<BnbDocument>(clientsById.Values
                            .Select(ec =>
                            {
                                string nextNumber = counter.ToString();
                                
                                BnbDocument bnbDocument = new BnbDocument()
                                {
                                    Doc = DocumentEnum.PNVB,
                                    Nok = nextNumber,
                                    IPol = ec.AisName,
                                    Iban = ec.AccountIBAN,
                                    Bic = ec.AccountBIC,
                                    Su = distributionRevenue.DistributionRevenuePayments.Where(drp => drp.EserviceClientId == ec.EserviceClientId).Sum(drp => drp.PaymentRequest.PaymentAmount),
                                    O1 = (firstDescription != null && firstDescription.Length > 35 ? firstDescription.Substring(0, 35) : firstDescription),
                                    //O2 = (secondDescription != null && secondDescription.Length > 35 ? secondDescription.Substring(0, 35) : secondDescription),
                                    O2 = $"refid {distributionRevenue.DistributionRevenueId.ToString()}",
                                    Sys = distributionRevenue.TotalSum <= 100000 ?  PaymentSystemEnum.BISERA : PaymentSystemEnum.RINGS
                                };

                                if (iban != null && iban.Length >= 13 && iban[12] == '8')
                                {
                                     bnbDocument.Vpn = vpn;
                                }

                                if (ec.AccountIBAN != null && ec.AccountIBAN.Length >= 13 && ec.AccountIBAN[12] == '8')
                                {
                                    bnbDocument.Vpp = ec.BudgetCode;
                                }
                                else
                                {
                                    bnbDocument.Vpp = null;
                                }

                                bnbDocument.BnbBudget = new BnbBudget() 
                                { 
                                    Vd = vd,
                                    Bul = ec.Department.UniqueIdentificationNumber,
                                    Db = distributionRevenue.CreatedAt,
                                    De = distributionRevenue.CreatedAt
                                };

                                counter++;

                                return bnbDocument;
                            }))
                    }
                }
            };
        }
    }
}
