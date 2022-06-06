using EPayments.Data.ViewObjects.Admin;
using EPayments.Model.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPayments.Data.Repositories.Interfaces
{
    public interface IPaymentRequestRepository
    {
        Task<int> CountPaymentRequests(string filterPaymentIdentifier,
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
            string applicantUin);

        Task<List<PaymentRequestVO>> GetPaymentRequests(string filterPaymentIdentifier,
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
            int resultsPerPage);

        Task<List<PaymentRequestVO>> GetAllRequests(string filterPaymentIdentifier,
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
            bool sortDescending);
    }
}
