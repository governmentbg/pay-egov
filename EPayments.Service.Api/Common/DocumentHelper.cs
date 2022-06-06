using EPayments.Common.Helpers;
using EPayments.Data.Repositories.Interfaces;
using EPayments.Model.Enums;
using EPayments.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPayments.Service.Api.Common
{
    public class DocumentHelper
    {
        public static Tuple<UinType, string, string> GetUinTypeUinAndName(R_0009_000015.ElectronicServiceRecipient electronicServiceRecipient)
        {
            UinType? applicantUinType = null;
            string applicantUin = null;
            string applicantName = null;

            if (electronicServiceRecipient.Person != null)
            {
                if (!String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Identifier.EGN))
                {
                    applicantUinType = UinType.Egn;
                    applicantUin = electronicServiceRecipient.Person.Identifier.EGN;
                }
                else if (!String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Identifier.LNCh))
                {
                    applicantUinType = UinType.Lnch;
                    applicantUin = electronicServiceRecipient.Person.Identifier.LNCh;
                }

                applicantName = Formatter.FormatName(
                    electronicServiceRecipient.Person.Names.First,
                    electronicServiceRecipient.Person.Names.Middle,
                    electronicServiceRecipient.Person.Names.Last);
            }
            else if (electronicServiceRecipient.Entity != null)
            {
                applicantUinType = UinType.Bulstat;
                applicantUin = electronicServiceRecipient.Entity.Identifier;
                applicantName = electronicServiceRecipient.Entity.Name;
            }

            if (!applicantUinType.HasValue || String.IsNullOrWhiteSpace(applicantUin) || String.IsNullOrWhiteSpace(applicantName))
            {
                /**/
                throw new Exception("Missing data for electronicServiceApplicant");
            }

            return new Tuple<UinType, string, string>(applicantUinType.Value, applicantUin, applicantName);
        }

        public static List<string> ValidatePaymentRequestData(IApiRepository apiRepository, R_10046.PaymentRequest paymentRequest, int eserviceClientId, string aisPaymentId, out PaymentRequest existingRequest)
        {
            existingRequest = null;

            List<string> errors = new List<string>();

            bool hasValidBankAccountIBAN = false;
            bool hasValidPaymentReferenceNumber = false;
            bool hasValidPaymentReferenceDate = false;
            bool hasValidExpirationDate = false;

            if (paymentRequest.ElectronicServiceProviderBasicData == null)
            {
                errors.Add("Element 'ElectronicServiceProviderBasicData' is required.");
            }
            else
            {
                if (paymentRequest.ElectronicServiceProviderBasicData.EntityBasicData == null)
                {
                    errors.Add("Element 'ElectronicServiceProviderBasicData.EntityBasicData' is required.");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(paymentRequest.ElectronicServiceProviderBasicData.EntityBasicData.Name))
                    {
                        errors.Add("Element 'ElectronicServiceProviderBasicData.EntityBasicData.Name' is required.");
                    }
                }
            }

            if (paymentRequest.ElectronicServiceProviderBankAccount == null)
            {
                errors.Add("Element 'ElectronicServiceProviderBankAccount' is required.");
            }
            else
            {
                if (paymentRequest.ElectronicServiceProviderBankAccount.EntityBasicData == null)
                {
                    errors.Add("Element 'ElectronicServiceProviderBankAccount.EntityBasicData' is required.");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(paymentRequest.ElectronicServiceProviderBankAccount.EntityBasicData.Name))
                    {
                        errors.Add("Element 'ElectronicServiceProviderBankAccount.EntityBasicData.Name' is required.");
                    }
                }

                if (String.IsNullOrWhiteSpace(paymentRequest.ElectronicServiceProviderBankAccount.BIC))
                {
                    errors.Add("Element 'ElectronicServiceProviderBankAccount.BIC' is required.");
                }

                if (String.IsNullOrWhiteSpace(paymentRequest.ElectronicServiceProviderBankAccount.IBAN))
                {
                    errors.Add("Element 'ElectronicServiceProviderBankAccount.IBAN' is required.");
                }
                else
                {
                    hasValidBankAccountIBAN = true;
                }
            }

            if (String.IsNullOrWhiteSpace(paymentRequest.Currency))
            {
                errors.Add("Element 'Currency' is required.");
            }

            if (String.IsNullOrWhiteSpace(paymentRequest.PaymentReason))
            {
                errors.Add("Element 'PaymentReason' is required.");
            }

            if (paymentRequest.PaymentReference == null)
            {
                errors.Add("Element 'PaymentReference' is required.");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(paymentRequest.PaymentReference.PaymentReferenceNumber))
                {
                    errors.Add("Element 'PaymentReference.PaymentReferenceNumber' is required.");
                }
                else
                {
                    hasValidPaymentReferenceNumber = true;
                }

                if (!paymentRequest.PaymentReference.PaymentReferenceDate.HasValue)
                {
                    errors.Add("Element 'PaymentReference.PaymentReferenceDate' is required.");
                }
                else
                {
                    hasValidPaymentReferenceDate = true;
                }
            }

            if (!paymentRequest.PaymentRequestExpirationDate.HasValue)
            {
                errors.Add("Element 'PaymentRequestExpirationDate' is required.");
            }
            else
            {
                if (paymentRequest.PaymentRequestExpirationDate.Value <= DateTime.Now)
                {
                    errors.Add("Value of 'PaymentRequestExpirationDate' should be greater than current date.");
                }
                else
                {
                    hasValidExpirationDate = true;
                }
            }

            if (hasValidBankAccountIBAN && hasValidPaymentReferenceNumber && hasValidPaymentReferenceDate && hasValidExpirationDate)
            {
                if (String.IsNullOrWhiteSpace(aisPaymentId))
                {
                    bool isRequestExist = apiRepository.IsValidRequestWithKeyDataExist(
                        paymentRequest.ElectronicServiceProviderBankAccount.IBAN.Trim(),
                        paymentRequest.PaymentReference.PaymentReferenceNumber.Trim(),
                        paymentRequest.PaymentReference.PaymentReferenceDate.Value.Date);

                    if (isRequestExist)
                    {
                        errors.Add("Already exist valid payment request with the same values for 'ElectronicServiceProviderBankAccount.IBAN', 'PaymentReference.PaymentReferenceNumber' and 'PaymentReference.PaymentReferenceDate'.");
                    }
                }
                else
                {
                    existingRequest = apiRepository.GetPaymentRequestByAisPaymentId(eserviceClientId, aisPaymentId);
                    if(existingRequest != null)
                    {
                        if (existingRequest.PaymentRequestStatusId != PaymentRequestStatus.Pending)
                            errors.Add(String.Format("Payment request with corresponding aisPaymentId has status '{0}'. Payment request replacing is allowed only when request status is 'Pending'.", existingRequest.PaymentRequestStatusId.ToString()));
                    }
                }
            }

            if (paymentRequest.ElectronicServiceRecipient == null)
            {
                errors.Add("Element 'ElectronicServiceRecipient' is required.");
            }
            else
            {
                var electronicServiceRecipient = paymentRequest.ElectronicServiceRecipient;

                bool hasUinTypeSpecified = false;
                bool hasUinSpecified = false;
                bool hasNameSpecified = false;

                if (electronicServiceRecipient.Person != null)
                {
                    hasUinTypeSpecified = true;

                    if (electronicServiceRecipient.Person.Identifier != null)
                    {
                        if (!String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Identifier.EGN) ||
                            !String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Identifier.LNCh))
                        {
                            hasUinSpecified = true;
                        }
                    }

                    if (electronicServiceRecipient.Person.Names != null)
                    {
                        if (!String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Names.First) ||
                            !String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Names.Middle) ||
                            !String.IsNullOrWhiteSpace(electronicServiceRecipient.Person.Names.Last))
                        {
                            hasNameSpecified = true;
                        }
                    }
                }
                else if (electronicServiceRecipient.Entity != null)
                {
                    hasUinTypeSpecified = true;

                    if (!String.IsNullOrWhiteSpace(electronicServiceRecipient.Entity.Identifier))
                    {
                        hasUinSpecified = true;
                    }

                    if (!String.IsNullOrWhiteSpace(electronicServiceRecipient.Entity.Name))
                    {
                        hasNameSpecified = true;
                    }
                }

                if (!hasUinTypeSpecified)
                {
                    errors.Add("Element 'ElectronicServiceRecipient' (Person or Entity) is required.");
                }

                if (!hasUinSpecified)
                {
                    errors.Add("Recipient identifier in 'ElectronicServiceRecipient' (Person or Entity) is required.");
                }

                if (!hasNameSpecified)
                {
                    errors.Add("Recipient name in 'ElectronicServiceRecipient' (Person or Entity) is required.");
                }
            }

            return errors;
        }
    }
}
