﻿using EPayments.Common.Data;
using EPayments.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EPayments.Model.Models;
using EPayments.Model.Enums;
using EPayments.Common.Helpers;
using EPayments.Data.ViewObjects.Api;
using System;
using EPayments.Data.ViewObjects.Web;

namespace EPayments.Data.Repositories.Implementations
{
    internal class ApiRepository : BaseRepository, IApiRepository
    {
        public ApiRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<RequestXmlVO> GetRequestXmlsByIndetifiers(List<string> identifiers)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    join prx in this.unitOfWork.DbContext.Set<PaymentRequestXml>() on pr.PaymentRequestXmlId equals prx.PaymentRequestXmlId
                    where identifiers.Contains(pr.PaymentRequestIdentifier)
                    select new RequestXmlVO
                    {
                        Id = pr.PaymentRequestIdentifier,
                        RequestXml = prx.RequestContent,
                        PaymentRequest = pr
                    })
                   .ToList();
        }

        public IList<RequestStatusVO> GetRequestStatusesByIdentifiers(List<string> identifiers)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    where identifiers.Contains(pr.PaymentRequestIdentifier)
                    select new RequestStatusVO
                    {
                        Id = pr.PaymentRequestIdentifier,
                        Status = pr.PaymentRequestStatusId,
                        ChangeTime = pr.PaymentRequestStatusChangeTime
                    })
                    .ToList();
        }

        public List<Tuple<int, RequestInfoVO>> GetRequestInfosByApplicant(UinType applicantUinTypeId, string applicantUin)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    join prx in this.unitOfWork.DbContext.Set<PaymentRequestXml>() on pr.PaymentRequestXmlId equals prx.PaymentRequestXmlId
                    where pr.ApplicantUinTypeId == applicantUinTypeId && pr.ApplicantUin == applicantUin && pr.PaymentRequestStatusId == PaymentRequestStatus.Pending
                    select new
                    {
                        paymentRequestId = pr.PaymentRequestId,
                        requestInfoVO = new RequestInfoVO
                        {
                            Id = pr.PaymentRequestIdentifier,
                            Status = pr.PaymentRequestStatusId,
                            ChangeTime = pr.PaymentRequestStatusChangeTime,
                            RequestXml = prx.RequestContent
                        }
                    })
                    .ToList()
                    .Select(e => new Tuple<int, RequestInfoVO>(e.paymentRequestId, e.requestInfoVO))
                    .ToList();
        }

        public List<Tuple<int, RequestInfoParsedVO>> GetParsedRequestInfosByApplicant(UinType applicantUinTypeId, string applicantUin)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    where pr.ApplicantUinTypeId == applicantUinTypeId && pr.ApplicantUin == applicantUin && pr.PaymentRequestStatusId == PaymentRequestStatus.Pending
                    select new
                    {
                        paymentRequestId = pr.PaymentRequestId,
                        requestInfoParsedVO = new RequestInfoParsedVO
                        {
                            Id = pr.PaymentRequestIdentifier,
                            Status = pr.PaymentRequestStatusId,
                            StatusChangeTime = pr.PaymentRequestStatusChangeTime,
                            ServiceProviderName = pr.ServiceProviderName,
                            ServiceProviderBank = pr.ServiceProviderBank,
                            ServiceProviderBIC = pr.ServiceProviderBIC,
                            ServiceProviderIBAN = pr.ServiceProviderIBAN,
                            Currency = pr.Currency,
                            PaymentTypeCode = pr.PaymentTypeCode,
                            PaymentAmount = pr.PaymentAmount,
                            PaymentReason = pr.PaymentReason,
                            ApplicantUinType = pr.ApplicantUinTypeId,
                            ApplicantUin = pr.ApplicantUin,
                            ApplicantName = pr.ApplicantName,
                            PaymentReferenceType = pr.PaymentReferenceType,
                            PaymentReferenceNumber = pr.PaymentReferenceNumber,
                            PaymentReferenceDate = pr.PaymentReferenceDate,
                            ExpirationDate = pr.ExpirationDate,
                            AdditionalInformation = pr.AdditionalInformation,
                            CreateDate = pr.CreateDate
                        }
                    })
                    .ToList()
                    .Select(e => new Tuple<int, RequestInfoParsedVO>(e.paymentRequestId, e.requestInfoParsedVO))
                    .ToList();
        }

        public List<Tuple<int, RequestPaymentInfoParsedVO>> GetParsedPendingPaymentRequestInfosByApplicant(string applicantUin)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    join es in unitOfWork.DbContext.Set<EserviceClient>() on pr.EserviceClientId equals es.EserviceClientId
                    where (pr.ApplicantUinTypeId == UinType.Egn || pr.ApplicantUinTypeId == UinType.Lnch) 
                    && pr.ApplicantUin == applicantUin && pr.PaymentRequestStatusId == PaymentRequestStatus.Pending
                    select new
                    {
                        paymentRequestId = pr.PaymentRequestId,
                        requestPaymentInfoParsedVO = new RequestPaymentInfoParsedVO
                        {
                            Id = pr.PaymentRequestIdentifier,
                            EserviceClientAisName = es.AisName,
                            EserviceClientServiceName = es.ServiceName,
                            EserviceClientDepartmentId = es.DepartmentId.ToString(),
                            Status = pr.PaymentRequestStatusId,
                            StatusChangeTime = pr.PaymentRequestStatusChangeTime,
                            ServiceProviderName = pr.ServiceProviderName,
                            ServiceProviderBank = pr.ServiceProviderBank,
                            ServiceProviderBIC = pr.ServiceProviderBIC,
                            ServiceProviderIBAN = pr.ServiceProviderIBAN,
                            Currency = pr.Currency,
                            PaymentTypeCode = pr.PaymentTypeCode,
                            PaymentAmount = pr.PaymentAmount,
                            PaymentReason = pr.PaymentReason,
                            ApplicantUinType = pr.ApplicantUinTypeId,
                            ApplicantUin = pr.ApplicantUin,
                            ApplicantName = pr.ApplicantName,
                            PaymentReferenceType = pr.PaymentReferenceType,
                            PaymentReferenceNumber = pr.PaymentReferenceNumber,
                            PaymentReferenceDate = pr.PaymentReferenceDate,
                            ExpirationDate = pr.ExpirationDate,
                            AdditionalInformation = pr.AdditionalInformation,
                            CreateDate = pr.CreateDate
                        }
                    })
                    .ToList()
                    .Select(e => new Tuple<int, RequestPaymentInfoParsedVO>(e.paymentRequestId, e.requestPaymentInfoParsedVO))
                    .ToList();
        }

        public List<Tuple<int, RequestPaymentInfoParsedVO>> GetParsedPaymentRequestInfosByApplicant(string applicantUin)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    join es in unitOfWork.DbContext.Set<EserviceClient>() on pr.EserviceClientId equals es.EserviceClientId
                    where (pr.ApplicantUinTypeId == UinType.Egn || pr.ApplicantUinTypeId == UinType.Lnch) 
                    && pr.ApplicantUin == applicantUin && pr.PaymentRequestStatusId != PaymentRequestStatus.Pending
                    select new
                    {
                        paymentRequestId = pr.PaymentRequestId,
                        requestPaymentInfoParsedVO = new RequestPaymentInfoParsedVO
                        {
                            Id = pr.PaymentRequestIdentifier,
                            EserviceClientAisName = es.AisName,
                            EserviceClientServiceName = es.ServiceName,
                            EserviceClientDepartmentId = es.DepartmentId.ToString(),
                            Status = pr.PaymentRequestStatusId,
                            StatusChangeTime = pr.PaymentRequestStatusChangeTime,
                            ServiceProviderName = pr.ServiceProviderName,
                            ServiceProviderBank = pr.ServiceProviderBank,
                            ServiceProviderBIC = pr.ServiceProviderBIC,
                            ServiceProviderIBAN = pr.ServiceProviderIBAN,
                            Currency = pr.Currency,
                            PaymentTypeCode = pr.PaymentTypeCode,
                            PaymentAmount = pr.PaymentAmount,
                            PaymentReason = pr.PaymentReason,
                            ApplicantUinType = pr.ApplicantUinTypeId,
                            ApplicantUin = pr.ApplicantUin,
                            ApplicantName = pr.ApplicantName,
                            PaymentReferenceType = pr.PaymentReferenceType,
                            PaymentReferenceNumber = pr.PaymentReferenceNumber,
                            PaymentReferenceDate = pr.PaymentReferenceDate,
                            ExpirationDate = pr.ExpirationDate,
                            AdditionalInformation = pr.AdditionalInformation,
                            CreateDate = pr.CreateDate
                        }
                    })
                    .ToList()
                    .Select(e => new Tuple<int, RequestPaymentInfoParsedVO>(e.paymentRequestId, e.requestPaymentInfoParsedVO))
                    .ToList();
        }

        public List<RequestEikInfoVO> GetServiceClientByEik(string eiknumber) 
        {
            var result = from dep in this.unitOfWork.DbContext.Set<Department>()
                         join esc in this.unitOfWork.DbContext.Set<EserviceClient>() on dep.DepartmentId equals esc.DepartmentId
                         where dep.UniqueIdentificationNumber == eiknumber && dep.IsActive
                         select
                             new RequestEikInfoVO()
                             {
                                 DepartmentId = dep.DepartmentId,
                                 Name = esc.ServiceName,
                                 UniqueIdentificationNumber = dep.UniqueIdentificationNumber,
                                 IsActive = dep.IsActive,
                                 EserviceClientId = esc.EserviceClientId,
                                 AisName = esc.AisName,
                                 AccountBank = esc.AccountBank,
                                 AccountBIC = esc.AccountBIC,
                                 AccountIBAN = esc.AccountIBAN
                             };
             return result.ToList();
            }

        public List<Tuple<int, RequestEikInfoVO>> GetParsedRequestInfoByEik(string eiknumber)
        {
            return (from dep in this.unitOfWork.DbContext.Set<Department>()
                    join esc in this.unitOfWork.DbContext.Set<EserviceClient>() on dep.DepartmentId equals esc.DepartmentId
                    where dep.UniqueIdentificationNumber == eiknumber && dep.IsActive 
                    select new
                    {
                        DepartmentId = dep.DepartmentId,
                        requestEikInfoVO = new RequestEikInfoVO()
                        {
                            DepartmentId = dep.DepartmentId,
                            Name = dep.Name,
                            UniqueIdentificationNumber = dep.UniqueIdentificationNumber,
                            IsActive = dep.IsActive,
                            EserviceClientId = esc.EserviceClientId,
                            AisName = esc.AisName,
                            AccountBank = esc.AccountBank,
                            AccountBIC = esc.AccountBIC,
                            AccountIBAN = esc.AccountIBAN
                        }

                    }).ToList()
                    .Select( e => new Tuple<int, RequestEikInfoVO>(e.DepartmentId, e.requestEikInfoVO))
                    .ToList();
        }

        public List<Tuple<int, RequestRefidInfoVO>> GetParsedRequestInfoByRefid(int refid, string clientId)
        {

            return (from dr in unitOfWork.DbContext.Set<DistributionRevenue>()
                    join drp in unitOfWork.DbContext.Set<DistributionRevenuePayment>() on dr.DistributionRevenueId equals drp.DistributionRevenueId
                    join bt in unitOfWork.DbContext.Set<BoricaTransaction>() on drp.BoricaTransactionId equals bt.BoricaTransactionId
                    join es in unitOfWork.DbContext.Set<EserviceClient>() on drp.EserviceClientId equals es.EserviceClientId
                    join depart in unitOfWork.DbContext.Set<Department>() on es.DepartmentId equals depart.DepartmentId
                    where dr.DistributionRevenueId == refid && es.ClientId == clientId
                    select new
                    {
                        BoricaTransactionId = bt.BoricaTransactionId,
                        DistributionRevenueId = dr.DistributionRevenueId,
                        requestRefidInfoVO = new RequestRefidInfoVO()
                        {
                            DistributionRevenueId = dr.DistributionRevenueId,
                            TotalSum = dr.TotalSum,
                            CreatedAt = dr.CreatedAt,
                            Order = bt.Order,
                            Amount = bt.Amount,
                            TransactionDate = bt.TransactionDate,
                            Rrn = bt.Rrn,
                            PaymentInfo =
                            bt.PaymentRequests.Select(pr =>  new RequestPaymentInfoParsedVO
                            {
                                Id = pr.PaymentRequestId.ToString(),
                                Status = pr.PaymentRequestStatusId,
                                StatusChangeTime = pr.PaymentRequestStatusChangeTime,
                                ServiceProviderName = pr.ServiceProviderName,
                                ServiceProviderBank = pr.ServiceProviderBank,
                                ServiceProviderBIC = pr.ServiceProviderBIC,
                                ServiceProviderIBAN = pr.ServiceProviderIBAN,
                                Currency = pr.Currency,
                                PaymentTypeCode = pr.PaymentTypeCode,
                                PaymentAmount = pr.PaymentAmount,
                                PaymentReason = pr.PaymentReason,
                                ApplicantUinType = pr.ApplicantUinTypeId,
                                ApplicantUin = pr.ApplicantUin,
                                ApplicantName = pr.ApplicantName,
                                PaymentReferenceType = pr.PaymentReferenceType,
                                PaymentReferenceNumber = pr.PaymentReferenceNumber,
                                PaymentReferenceDate = pr.PaymentReferenceDate,
                                ExpirationDate = pr.ExpirationDate,
                                AdditionalInformation = pr.AdditionalInformation,
                                CreateDate = pr.CreateDate,
                                EserviceClientAisName = es.AisName,
                                EserviceClientServiceName = es.ServiceName,
                                ClientId = es.ClientId,
                                EserviceClientDepartmentId = es.DepartmentId.ToString()
                            }),
                            DepartmentName = depart.Name,
                            DepartmentUniqueIdentificationNumber = depart.UniqueIdentificationNumber
                        }
                    })
                    .GroupBy(g => g.BoricaTransactionId)
                    .Select(s => s.FirstOrDefault()).ToList()
                    .Select(e => new Tuple<int, RequestRefidInfoVO>(e.DistributionRevenueId, e.requestRefidInfoVO))
                    .ToList();

        }

        public int GetNumberOfPaymentsByMonth(string request_type, string month)
        {
            int result = 0;
            try
            {
                if (string.IsNullOrEmpty(month) == false || string.IsNullOrWhiteSpace(month))
                {
                    DateTime date = DateTime.Parse(month);
                    int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                    DateTime startDate = new DateTime(date.Year, date.Month, 1);
                    DateTime endDate = new DateTime(date.Year, date.Month, daysInMonth);

                    if (request_type == "request")
                    {
                        result = this.unitOfWork.DbContext.Set<PaymentRequest>().
                            Count(pr => (pr.CreateDate >= startDate && pr.CreateDate <= endDate));
                    }
                    else if (request_type == "payments")
                    {
                        result = this.unitOfWork.DbContext.Set<PaymentRequest>().
                            Count(pr => (pr.CreateDate >= startDate && pr.CreateDate <= endDate)
                            && pr.PaymentRequestStatusId == PaymentRequestStatus.Paid);
                    }
                }
                else
                {
                    DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
                    DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                    for (var m = startDate.Month; m <= endDate.Month; m++)
                    {

                        if (request_type == "request")
                        {
                            result = this.unitOfWork.DbContext.Set<PaymentRequest>().
                                Count(pr => (pr.CreateDate >= startDate && pr.CreateDate <= endDate));
                        }
                        else if (request_type == "payments")
                        {
                            result = this.unitOfWork.DbContext.Set<PaymentRequest>().
                                Count(pr => (pr.CreateDate >= startDate && pr.CreateDate <= endDate)
                                && pr.PaymentRequestStatusId == PaymentRequestStatus.Paid);
                        }
                    }
                }
            }
            
            catch (Exception ex)
            {

            }

            return result;
        }

        public int GetNumberOfPaymentsFromDateToDate(string request_type, string startDate, string endDate)
        {
            int result = 0;
            try
            {
                DateTime startdate = DateTime.Parse(startDate);
                DateTime enddate = DateTime.Parse(endDate);

                if (request_type == "request")
                {
                    result = this.unitOfWork.DbContext.Set<PaymentRequest>().
                        Count(pr => (pr.CreateDate >= startdate && pr.CreateDate <= enddate));
                }
                else if (request_type == "payments")
                {
                    result = this.unitOfWork.DbContext.Set<PaymentRequest>().
                        Count(pr => (pr.CreateDate >= startdate && pr.CreateDate <= enddate)
                        && pr.PaymentRequestStatusId == PaymentRequestStatus.Paid);
                }
            }

            catch (Exception ex)
            { }

            return result;
        }

        public object GetNumberOfVposePayCvposPaidRequestsFromDateToDate(string start_date, string end_date)
        {
            object result = new object();

            try
            {
                DateTime startdate = DateTime.Parse(start_date);
                DateTime enddate = DateTime.Parse(end_date);

                var vposres = this.unitOfWork.DbContext.Set<VposFiBankRequest>().
                        Count(fb => (fb.CreateDate >= startdate && fb.CreateDate <= enddate && fb.IsPaymentSuccessful == true))
                        + this.unitOfWork.DbContext.Set<VposDskEcommRequest>().
                        Count(ds => (ds.CreateDate >= startdate && ds.CreateDate <= enddate && ds.IsPaymentSuccessful == true));

                var epayres = this.unitOfWork.DbContext.Set<VposEpayRequest>().
                        Count(ep => (ep.CreateDate >= startdate && ep.CreateDate <= enddate && ep.IsPaymentSuccessful == true));

                var cvposres = this.unitOfWork.DbContext.Set<BoricaTransaction>().
                        Count(br => (br.TransactionDate >= startdate && br.TransactionDate <= enddate && (br.TransactionStatusId == 2 ||
                        br.TransactionStatusId == 5)));

                var res = new
                {
                    vpos = new
                    {
                        from = $"{start_date} to {end_date}",
                        value = vposres
                    }, 
                    epay = new
                    {
                        from = $"{start_date} to {end_date}",
                        value = epayres
                    },
                    cvpos = new
                    {
                        from = $"{start_date} to {end_date}",
                        value = cvposres
                    }     
                };

                result = res;
            }

            catch (Exception ex)
            { }

            return result;
        }

        public object GetAdministrationsWithTheHighestNumberOfPaymentsFromDateToDate(string start_date, string end_date)
        {
            object result = new object();

            try
            {
                DateTime startdate = DateTime.Parse(start_date);
                DateTime enddate = DateTime.Parse(end_date);
                var resadmin = (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                           join esc in unitOfWork.DbContext.Set<EserviceClient>() on pr.EserviceClientId equals esc.EserviceClientId
                           join dep in unitOfWork.DbContext.Set<Department>() on esc.DepartmentId equals dep.DepartmentId

                           where pr.CreateDate >= startdate && pr.CreateDate <= enddate && pr.PaymentRequestStatusId == PaymentRequestStatus.Paid
                           select new
                           {
                               AdministrationId = pr.EserviceClientId,
                               ApplicantName = pr.ApplicantName,
                               AdministrationName = dep.Name
                           }).ToList().GroupBy(r => r.AdministrationId).OrderByDescending(r => r.Count()).ToList().FirstOrDefault();

                if(resadmin.Count() > 0)
                {
                    result = new
                    {
                        from = $"{start_date} to {end_date}",
                        Administration = resadmin.FirstOrDefault().AdministrationName,
                        value = resadmin.Count()
                    };
                }
            }

            
            catch (Exception ex)
            { }

            return result;
        }

        public Tuple<int, RequestInfoVO> GetRequestInfoByAccessCode(string accessCode)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    join prx in this.unitOfWork.DbContext.Set<PaymentRequestXml>() on pr.PaymentRequestXmlId equals prx.PaymentRequestXmlId
                    where pr.PaymentRequestAccessCode == accessCode
                    select new
                    {
                        paymentRequestId = pr.PaymentRequestId,
                        requestInfoVO = new RequestInfoVO
                        {
                            Id = pr.PaymentRequestIdentifier,
                            Status = pr.PaymentRequestStatusId,
                            ChangeTime = pr.PaymentRequestStatusChangeTime,
                            RequestXml = prx.RequestContent
                        }
                    })
                    .ToList()
                    .Select(e => new Tuple<int, RequestInfoVO>(e.paymentRequestId, e.requestInfoVO))
                    .SingleOrDefault();
        }

        public Tuple<int, RequestInfoParsedVO> GetParsedRequestInfoByAccessCode(string accessCode)
        {
            return (from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    where pr.PaymentRequestAccessCode == accessCode
                    select new
                    {
                        paymentRequestId = pr.PaymentRequestId,
                        requestInfoParsedVO = new RequestInfoParsedVO
                        {
                            Id = pr.PaymentRequestIdentifier,
                            Status = pr.PaymentRequestStatusId,
                            StatusChangeTime = pr.PaymentRequestStatusChangeTime,
                            ServiceProviderName = pr.ServiceProviderName,
                            ServiceProviderBank = pr.ServiceProviderBank,
                            ServiceProviderBIC = pr.ServiceProviderBIC,
                            ServiceProviderIBAN = pr.ServiceProviderIBAN,
                            Currency = pr.Currency,
                            PaymentTypeCode = pr.PaymentTypeCode,
                            PaymentAmount = pr.PaymentAmount,
                            PaymentReason = pr.PaymentReason,
                            ApplicantUinType = pr.ApplicantUinTypeId,
                            ApplicantUin = pr.ApplicantUin,
                            ApplicantName = pr.ApplicantName,
                            PaymentReferenceType = pr.PaymentReferenceType,
                            PaymentReferenceNumber = pr.PaymentReferenceNumber,
                            PaymentReferenceDate = pr.PaymentReferenceDate,
                            ExpirationDate = pr.ExpirationDate,
                            AdditionalInformation = pr.AdditionalInformation,
                            CreateDate = pr.CreateDate
                        }
                    })
                   .ToList()
                   .Select(e => new Tuple<int, RequestInfoParsedVO>(e.paymentRequestId, e.requestInfoParsedVO))
                   .SingleOrDefault();
        }

        public bool IsValidRequestWithKeyDataExist(string serviceProviderIBAN, string paymentReferenceNumber, DateTime paymentReferenceDate)
        {
            return this.unitOfWork.DbContext.Set<PaymentRequest>()
                .Any(e =>
                    e.ServiceProviderIBAN == serviceProviderIBAN &&
                    e.PaymentReferenceNumber == paymentReferenceNumber &&
                    e.PaymentReferenceDate == paymentReferenceDate &&
                    e.ExpirationDate > DateTime.Now &&
                    e.PaymentRequestStatusId != PaymentRequestStatus.Expired &&
                    e.PaymentRequestStatusId != PaymentRequestStatus.Canceled &&
                    e.PaymentRequestStatusId != PaymentRequestStatus.Suspended);
        }

        public bool IsClientAuthorizedToAccessRequests(string clientId, List<string> paymentRequestIdentifiers)
        {
            //checking only found requests by paymentRequestIdentifier
            return !(from pr in this.unitOfWork.DbContext.Set<PaymentRequest>()
                    join esc in this.unitOfWork.DbContext.Set<EserviceClient>() on pr.EserviceClientId equals esc.EserviceClientId
                    where paymentRequestIdentifiers.Contains(pr.PaymentRequestIdentifier) && esc.ClientId != clientId
                    select pr)
                    .Any();
        }

        public PaymentRequest GetPaymentRequestByAisPaymentId(int eserviceClientId, string aisPaymentId)
        {
            return this.unitOfWork.DbContext.Set<PaymentRequest>()
                .Where(e => e.EserviceClientId == eserviceClientId && e.AisPaymentId == aisPaymentId)
                .SingleOrDefault();
        }
    }
}
