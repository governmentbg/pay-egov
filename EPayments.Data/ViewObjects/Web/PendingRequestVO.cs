﻿using EPayments.Model.Enums;
using System;

namespace EPayments.Data.ViewObjects.Web
{
    public class PendingRequestVO
    {
        public Guid Gid { get; set; }
        public DateTime CreateDate { get; set; }
        public string ApplicantName { get; set; }
        public string PaymentRequestIdentifier { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ServiceProviderName { get; set; }
        public string PaymentReason { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsEpayVposEnabled { get; set; }
        public bool IsCvposEnabled { get; set; }
        public Vpos? Vpos { get; set; }
        public int? ObligationStatusId { get; set; }
        public bool SelectedForPayment { get; set; }

        public string ObligationType { get; set; }

        public string InitiatorName { get; set; }
    }
}
