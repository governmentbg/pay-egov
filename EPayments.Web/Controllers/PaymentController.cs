﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using EPayments.Common;
using EPayments.Common.Data;
using EPayments.Common.Helpers;
using EPayments.Data.Repositories.Interfaces;
using EPayments.Data.ViewObjects.Web;
using EPayments.Model.DataObjects.EmailTemplateContext;
using EPayments.Model.Enums;
using EPayments.Model.Models;
using EPayments.Web.Auth;
using EPayments.Web.Common;
using EPayments.Web.DataObjects;
using EPayments.Web.Models.Payments;
using EPayments.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPayments.Web.Controllers
{
    [Authorize]
    public partial class PaymentController : BaseController
    {
        private IWebRepository webRepository;
        private ICommonRepository commonRepository;
        private ISystemRepository systemRepository;
        private IUnitOfWork unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork, IWebRepository webRepository, ICommonRepository commonRepository, ISystemRepository systemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.webRepository = webRepository;
            this.commonRepository = commonRepository;
            this.systemRepository = systemRepository;
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult List(ListSearchDO searchDO)
        {
            ListVM model = new ListVM();

            model.PendingPaymentsPagingOptions = new PagingVM();
            model.ProcessedPaymentsPagingOptions = new PagingVM();

            model.SearchDO = searchDO;

            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                //Pending payments

                model.PendingPaymentsPagingOptions.CurrentPageIndex = searchDO.PPage;
                model.PendingPaymentsPagingOptions.ControllerName = MVC.Payment.Name;
                model.PendingPaymentsPagingOptions.ActionName = MVC.Payment.ActionNames.List;
                model.PendingPaymentsPagingOptions.PageIndexParameterName = "pPage";
                model.PendingPaymentsPagingOptions.RouteValues = searchDO.ToPendingRequestsRouteValues();

                model.PendingPaymentsPagingOptions.TotalItemCount = this.webRepository.CountPendingRequestsByUin(this.CurrentUser.Uin);

                model.PendingPayments = this.webRepository.GetPendingRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PSortBy,
                    searchDO.PSortDesc,
                    searchDO.PPage,
                    AppSettings.EPaymentsWeb_MaxSearchResultsPerPage);

                //Processed payments

                model.ProcessedPaymentsPagingOptions.CurrentPageIndex = searchDO.PrPage;
                model.ProcessedPaymentsPagingOptions.ControllerName = MVC.Payment.Name;
                model.ProcessedPaymentsPagingOptions.ActionName = MVC.Payment.ActionNames.List;
                model.ProcessedPaymentsPagingOptions.PageIndexParameterName = "prPage";
                model.ProcessedPaymentsPagingOptions.RouteValues = searchDO.ToProcessedRequestsRouteValues();

                model.ProcessedPaymentsPagingOptions.TotalItemCount = this.webRepository.CountProcessedRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PrId,
                    Parser.GetDateFirstMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateFrom)),
                    Parser.GetDateLastMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateTo)),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountFrom),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountTo),
                    searchDO.PrProvider,
                    searchDO.PrReason,
                    searchDO.PrStatus);

                model.ProcessedPayments = this.webRepository.GetProcessedRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PrId,
                    Parser.GetDateFirstMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateFrom)),
                    Parser.GetDateLastMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateTo)),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountFrom),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountTo),
                    searchDO.PrProvider,
                    searchDO.PrReason,
                    searchDO.PrStatus,
                    searchDO.PrSortBy,
                    searchDO.PrSortDesc,
                    searchDO.PrPage,
                    AppSettings.EPaymentsWeb_MaxSearchResultsPerPage);
            }
            else
            {
                model.PendingPayments = this.webRepository.GetPendingRequestsByAccessCode(this.CurrentUser.AccessCode);
                model.PendingPaymentsPagingOptions.TotalItemCount = model.PendingPayments.Count;

                model.ProcessedPayments = this.webRepository.GetProcessedRequestsByAccessCode(this.CurrentUser.AccessCode);
                model.ProcessedPaymentsPagingOptions.TotalItemCount = model.ProcessedPayments.Count;
            }

            return View(model);
        }

        [HttpPost]
        [WebUserAuthorize]
        public virtual ActionResult ListSearch(ListSearchDO searchDO)
        {
            TempData[TempDataKeys.SearchPerformed] = true;

            searchDO.PrPage = 1;
            searchDO.Focus = Constants.ProcessedPaymentsFocusId;

            return RedirectToListAction(searchDO);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult ListSort(ListSearchDO searchDO)
        {
            return RedirectToListAction(searchDO);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult PaymentRequestsExportAsPdf(ListSearchDO searchDO)
        {
            ListVM model = new ListVM();

            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                model.PendingPayments = this.webRepository.GetAllPendingRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PSortBy,
                    searchDO.PSortDesc);

                model.ProcessedPayments = this.webRepository.GetAllProcessedRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PrId,
                    Parser.GetDateFirstMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateFrom)),
                    Parser.GetDateLastMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateTo)),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountFrom),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountTo),
                    searchDO.PrProvider,
                    searchDO.PrReason,
                    searchDO.PrStatus,
                    searchDO.PrSortBy,
                    searchDO.PrSortDesc);
            }
            else
            {
                model.PendingPayments = this.webRepository.GetPendingRequestsByAccessCode(this.CurrentUser.AccessCode);

                model.ProcessedPayments = this.webRepository.GetProcessedRequestsByAccessCode(this.CurrentUser.AccessCode);
            }

            string htmlContent = RenderHelper.RenderHtmlByMvcView(MVC.Shared.Views._UserPaymentRequestsPrintPdf, model);

            byte[] data = RenderHelper.RenderPdf(MVC.Shared.Views._PrintPdf, htmlContent);

            string fileName = "Задължения" + MimeTypeFileExtension.GetFileExtenstionByMimeType(MimeTypeFileExtension.MIME_APPLICATION_PDF);

            return File(data, MimeTypeFileExtension.MIME_APPLICATION_PDF, fileName);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult PaymentRequestsExportAsExcel(ListSearchDO searchDO)
        {
            IList<PendingRequestVO> pendingPayments;
            IList<ProcessedRequestVO> processedPayments;

            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                pendingPayments = this.webRepository.GetAllPendingRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PSortBy,
                    searchDO.PSortDesc);

                processedPayments = this.webRepository.GetAllProcessedRequestsByUin(
                    this.CurrentUser.Uin,
                    searchDO.PrId,
                    Parser.GetDateFirstMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateFrom)),
                    Parser.GetDateLastMinute(Parser.BgFormatDateStringToDateTime(searchDO.PrDateTo)),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountFrom),
                    Parser.TwoDecimalPlacesFormatStringToDecimal(searchDO.PrAmountTo),
                    searchDO.PrProvider,
                    searchDO.PrReason,
                    searchDO.PrStatus,
                    searchDO.PrSortBy,
                    searchDO.PrSortDesc);
            }
            else
            {
                pendingPayments = this.webRepository.GetPendingRequestsByAccessCode(this.CurrentUser.AccessCode);

                processedPayments = this.webRepository.GetProcessedRequestsByAccessCode(this.CurrentUser.AccessCode);
            }

            XLWorkbook workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Транзакции");

            worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 6)).Merge();
            worksheet.Cell("A1").Value = "Задължения за плащане";
            worksheet.Cell("A1").Style.Font.Bold = true;

            //Set column headers
            worksheet.Cell("A2").Value = "Номер";
            worksheet.Cell("B2").Value = "Дата и час";
            worksheet.Cell("C2").Value = "Валидно до";
            worksheet.Cell("D2").Value = "Получател";
            worksheet.Cell("E2").Value = "Основание за плащане";
            worksheet.Cell("F2").Value = "Вид на задължението";
            worksheet.Cell("G2").Value = "Сума";

            worksheet.Cell("A2").Style.Font.Bold = true;
            worksheet.Cell("B2").Style.Font.Bold = true;
            worksheet.Cell("C2").Style.Font.Bold = true;
            worksheet.Cell("D2").Style.Font.Bold = true;
            worksheet.Cell("E2").Style.Font.Bold = true;
            worksheet.Cell("F2").Style.Font.Bold = true;
            worksheet.Cell("G2").Style.Font.Bold = true;

            int nextStartIndex = 5;

            if (pendingPayments != null && pendingPayments.Count > 0)
            {
                for (int i = 0; i < pendingPayments.Count; i++)
                {
                    worksheet.Cell(String.Format("A{0}", i + 3)).SetValue<string>(pendingPayments[i].PaymentRequestIdentifier);
                    worksheet.Cell(String.Format("B{0}", i + 3)).SetValue<string>(Formatter.DateTimeToBgFormatWithoutSeconds(pendingPayments[i].CreateDate));
                    worksheet.Cell(String.Format("C{0}", i + 3)).SetValue<string>(Formatter.DateTimeToBgFormatWithoutSeconds(pendingPayments[i].ExpirationDate));
                    worksheet.Cell(String.Format("D{0}", i + 3)).SetValue<string>(pendingPayments[i].ServiceProviderName);
                    worksheet.Cell(String.Format("E{0}", i + 3)).SetValue<string>(pendingPayments[i].PaymentReason);
                    worksheet.Cell(String.Format("F{0}", i + 3)).SetValue<string>(pendingPayments[i].ObligationType);
                    worksheet.Cell(String.Format("G{0}", i + 3)).SetValue<string>(Formatter.DecimalToTwoDecimalPlacesFormat(pendingPayments[i].PaymentAmount) + " лв.");
                }

                nextStartIndex += pendingPayments.Count - 1;
            }
            else
            {
                worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(3, 6)).Merge();
                worksheet.Cell("A3").Value = "Няма намерени резултати";
            }

            worksheet.Range(worksheet.Cell(nextStartIndex, 1), worksheet.Cell(nextStartIndex, 6)).Merge();
            worksheet.Cell(string.Format("A{0}", nextStartIndex)).Value = "Последни движения";
            worksheet.Cell(string.Format("A{0}", nextStartIndex)).Style.Font.Bold = true;

            nextStartIndex++;

            worksheet.Cell(string.Format("A{0}", nextStartIndex)).Value = "Номер";
            worksheet.Cell(string.Format("B{0}", nextStartIndex)).Value = "Дата и час";
            worksheet.Cell(string.Format("C{0}", nextStartIndex)).Value = "Получател";
            worksheet.Cell(string.Format("D{0}", nextStartIndex)).Value = "Основание за плащане";
            worksheet.Cell(string.Format("E{0}", nextStartIndex)).Value = "Вид на задължението";
            worksheet.Cell(string.Format("F{0}", nextStartIndex)).Value = "Платена сума";
            worksheet.Cell(string.Format("G{0}", nextStartIndex)).Value = "Статус";

            worksheet.Cell(string.Format("A{0}", nextStartIndex)).Style.Font.Bold = true;
            worksheet.Cell(string.Format("B{0}", nextStartIndex)).Style.Font.Bold = true;
            worksheet.Cell(string.Format("C{0}", nextStartIndex)).Style.Font.Bold = true;
            worksheet.Cell(string.Format("D{0}", nextStartIndex)).Style.Font.Bold = true;
            worksheet.Cell(string.Format("E{0}", nextStartIndex)).Style.Font.Bold = true;
            worksheet.Cell(string.Format("F{0}", nextStartIndex)).Style.Font.Bold = true;
            worksheet.Cell(string.Format("G{0}", nextStartIndex)).Style.Font.Bold = true;

            nextStartIndex++;

            if (processedPayments != null && processedPayments.Count > 0)
            {
                for (int i = 0; i < processedPayments.Count; i++)
                {
                    worksheet.Cell(String.Format("A{0}", i + nextStartIndex)).SetValue<string>(processedPayments[i].PaymentRequestIdentifier);
                    worksheet.Cell(String.Format("B{0}", i + nextStartIndex)).SetValue<string>(Formatter.DateTimeToBgFormatWithoutSeconds(processedPayments[i].TransactionDate));
                    worksheet.Cell(String.Format("C{0}", i + nextStartIndex)).SetValue<string>(processedPayments[i].ServiceProviderName);
                    worksheet.Cell(String.Format("D{0}", i + nextStartIndex)).SetValue<string>(processedPayments[i].PaymentReason);
                    worksheet.Cell(String.Format("E{0}", i + nextStartIndex)).SetValue<string>(processedPayments[i].ObligationType);
                    worksheet.Cell(String.Format("F{0}", i + nextStartIndex)).SetValue<string>(Formatter.DecimalToTwoDecimalPlacesFormat(processedPayments[i].PaymentAmountRequest) + " лв.");
                    worksheet.Cell(String.Format("G{0}", i + nextStartIndex)).SetValue<string>(Formatter.EnumToDescriptionString(processedPayments[i].PaymentRequestStatusId));
                }
            }
            else
            {
                worksheet.Range(worksheet.Cell(nextStartIndex, 1), worksheet.Cell(nextStartIndex, 6)).Merge();
                worksheet.Cell(string.Format("A{0}", nextStartIndex)).Value = "Няма намерени резултати";
            }

            worksheet.Rows().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            worksheet.Rows().Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;

            worksheet.Column("A").AdjustToContents();
            worksheet.Column("B").AdjustToContents();
            worksheet.Column("C").AdjustToContents();
            worksheet.Column("D").AdjustToContents();
            worksheet.Column("E").AdjustToContents();
            worksheet.Column("F").AdjustToContents();
            worksheet.Column("G").AdjustToContents();

            MemoryStream excelStream = new MemoryStream();
            workbook.SaveAs(excelStream);
            excelStream.Position = 0;

            return File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Задължения.xlsx");
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult Order(Guid id, bool showDetailsForm = false)
        {
            var paymentOrderVO = GetPaymentOrderVO(id);

            OrderVM model = new OrderVM(paymentOrderVO);

            //when (query param == true) then ShowDetailsForm is set to true, otherwise ShowDetailsForm is set in constructor
            if (showDetailsForm)
            {
                model.ShowDetailsForm = showDetailsForm;
            }

            return PartialView(MVC.Shared.Views._Order, model);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult Details(Guid id)
        {
            var paymentOrderVO = GetPaymentOrderVO(id);

            OrderVM model = new OrderVM(paymentOrderVO);

            return PartialView(model);
        }

        [HttpGet]
        [WebUserAuthorize]
        public virtual ActionResult CancelRequest(Guid id)
        {
            var paymentRequest = this.commonRepository.FindPaymentRequestByGuidAndUin(id, this.CurrentUser.Uin);

            if (paymentRequest.PaymentRequestStatusId == Model.Enums.PaymentRequestStatus.Pending)
            {
                paymentRequest.PaymentRequestStatusId = Model.Enums.PaymentRequestStatus.Canceled;
                paymentRequest.PaymentRequestStatusChangeTime = DateTime.Now;
                paymentRequest.ObligationStatusId = Model.Enums.ObligationStatusEnum.Canceled;

                this.unitOfWork.Save();

                User user = this.systemRepository.GetUserById(this.CurrentUser.UserId.Value);
                if (user != null && user.StatusNotifications && !String.IsNullOrWhiteSpace(user.Email))
                {
                    StatusChangedPaymentRequestContextDO contextDO = new StatusChangedPaymentRequestContextDO(
                        paymentRequest.PaymentRequestIdentifier,
                        paymentRequest.ServiceProviderName,
                        paymentRequest.PaymentReason,
                        paymentRequest.PaymentAmount,
                        paymentRequest.PaymentRequestStatusId.GetDescription());

                    Email email = new Email(contextDO, user.Email);

                    this.systemRepository.AddEntity<Email>(email);
                    this.unitOfWork.Save();
                }
                if (user != null && user.StatusObligationNotifications && !String.IsNullOrWhiteSpace(user.Email))
                {
                    StatusChangedObligationContextDO contextOblDO = new StatusChangedObligationContextDO(
                          paymentRequest.PaymentRequestIdentifier,
                          paymentRequest.ServiceProviderName,
                          paymentRequest.PaymentReason,
                          paymentRequest.PaymentAmount,
                          paymentRequest.ObligationStatusId.GetDescription());

                    Email email = new Email(contextOblDO, user.Email);

                    this.systemRepository.AddEntity<Email>(email);
                    this.unitOfWork.Save();
                }
                if (!String.IsNullOrWhiteSpace(paymentRequest.AdministrativeServiceNotificationURL))
                {
                    EserviceNotification statusNotification = new EserviceNotification(paymentRequest);

                    this.systemRepository.AddEntity<EserviceNotification>(statusNotification);
                    this.unitOfWork.Save();
                }
            }

            return RedirectToAction(MVC.Payment.ActionNames.List, MVC.Payment.Name);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult PrintOrder(Guid id)
        {
            var paymentOrderVO = GetPaymentOrderVO(id);

            OrderVM model = new OrderVM(paymentOrderVO);

            string htmlContent = RenderHelper.RenderHtmlByMvcView(MVC.Shared.Views._OrderPrint, model);

            return View(MVC.Shared.Views._PrintHtml, (object)htmlContent);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult PrintDetails(Guid id)
        {
            var paymentOrderVO = GetPaymentOrderVO(id);

            OrderVM model = new OrderVM(paymentOrderVO);

            string htmlContent = RenderHelper.RenderHtmlByMvcView(MVC.Payment.Views.DetailsPrint, model);

            return View(MVC.Shared.Views._PrintHtml, (object)htmlContent);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual FileResult DownloadPdfOrder(Guid id)
        {
            var paymentOrderVO = GetPaymentOrderVO(id);

            OrderVM model = new OrderVM(paymentOrderVO);

            string htmlContent = RenderHelper.RenderHtmlByMvcView(MVC.Shared.Views._OrderPrint, model);

            byte[] data = RenderHelper.RenderPdf(MVC.Shared.Views._PrintPdf, htmlContent);

            string fileName = "PaymentOrder" + MimeTypeFileExtension.GetFileExtenstionByMimeType(MimeTypeFileExtension.MIME_APPLICATION_PDF);

            return File(data, MimeTypeFileExtension.MIME_APPLICATION_PDF, fileName);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual FileResult DownloadPdfDetails(Guid id)
        {
            var paymentOrderVO = GetPaymentOrderVO(id);

            OrderVM model = new OrderVM(paymentOrderVO);

            string htmlContent = RenderHelper.RenderHtmlByMvcView(MVC.Payment.Views.DetailsPrint, model);

            byte[] data = RenderHelper.RenderPdf(MVC.Shared.Views._PrintPdf, htmlContent);

            string fileName = "PaymentDetails" + MimeTypeFileExtension.GetFileExtenstionByMimeType(MimeTypeFileExtension.MIME_APPLICATION_PDF);

            return File(data, MimeTypeFileExtension.MIME_APPLICATION_PDF, fileName);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult TaxAgreementDsk(Guid id)
        {
            PaymentRequest request;
            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            }
            else
            {
                request = this.webRepository.GetPaymentRequestByAccessCode(this.CurrentUser.AccessCode);
            }

            TaxAgreementDskVM model = new TaxAgreementDskVM();
            model.Gid = request.Gid;
            model.PaymentAmount = request.PaymentAmount;
            model.IsInternalPayment = true;

            return PartialView(MVC.Shared.Views._TaxAgreementDsk, model);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult TaxAgreementCvpos(Guid id)
        {
            PaymentRequest request;
            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            }
            else
            {
                request = this.webRepository.GetPaymentRequestByAccessCode(this.CurrentUser.AccessCode);
            }

            TaxAgreementCvposVM model = new TaxAgreementCvposVM();
            model.Gids = new Guid[] { request.Gid };
            model.PaymentAmount = request.PaymentAmount;
            model.IsInternalPayment = true;

            return PartialView(MVC.Shared.Views._TaxAgreementBoricaCvpos, model);
        }


        [HttpGet]
        public virtual ActionResult TaxAgreementCvposMultiple(Guid[] gids)
        {
            TaxAgreementCvposVM model = new TaxAgreementCvposVM();
            model.Gids = gids;

            List<PaymentRequest> paymentRequests = this.unitOfWork.DbContext.Set<PaymentRequest>()
                .Where(pr => gids.Contains(pr.Gid)).ToList();
            
            foreach (var paymentRequest in paymentRequests)
            {
                model.PaymentAmount += paymentRequest.PaymentAmount;
            }
            model.IsInternalPayment = true;

            return PartialView(MVC.Shared.Views._TaxAgreementBoricaCvpos, model);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult TaxAgreementBorica(Guid id)
        {
            PaymentRequest request;
            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            }
            else
            {
                request = this.webRepository.GetPaymentRequestByAccessCode(this.CurrentUser.AccessCode);
            }

            TaxAgreementBoricaVM model = new TaxAgreementBoricaVM();
            model.Gid = request.Gid;
            model.PaymentAmount = request.PaymentAmount;
            model.IsInternalPayment = true;

            return PartialView(MVC.Shared.Views._TaxAgreementBorica, model);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult TaxAgreementFiBank(Guid id)
        {
            PaymentRequest request;
            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            }
            else
            {
                request = this.webRepository.GetPaymentRequestByAccessCode(this.CurrentUser.AccessCode);
            }

            TaxAgreementFiBankVM model = new TaxAgreementFiBankVM();
            model.Gid = request.Gid;
            model.PaymentAmount = request.PaymentAmount;
            model.IsInternalPayment = true;

            return PartialView(MVC.Shared.Views._TaxAgreementFiBank, model);
        }

        [HttpGet]
        [WebUserAuthorize(AllowAuthorizationByAccessCode = true)]
        public virtual ActionResult TaxAgreementEpay(Guid id)
        {
            PaymentRequest request;
            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            }
            else
            {
                request = this.webRepository.GetPaymentRequestByAccessCode(this.CurrentUser.AccessCode);
            }

            TaxAgreementEpayVM model = new TaxAgreementEpayVM();
            model.Gid = request.Gid;
            model.PaymentAmount = request.PaymentAmount;
            model.IsInternalPayment = true;

            return PartialView(MVC.Shared.Views._TaxAgreementEpay, model);
        }

        [HttpGet]
        [WebUserAuthorize]
        public virtual ActionResult Share(Guid id)
        {
            PaymentRequest request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            User user = this.systemRepository.GetUserById(this.CurrentUser.UserId.Value);

            if (String.IsNullOrWhiteSpace(request.PaymentRequestAccessCode))
            {
                request.PaymentRequestAccessCode = this.commonRepository.GeneratePaymentRequestAccessCode();
                request.ObligationStatusId = ObligationStatusEnum.Ordered;

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    if (user.StatusObligationNotifications)
                    {
                        Email email = CreateObligationEmail(request, user);
                        this.systemRepository.AddEntity<Email>(email);

                        this.unitOfWork.Save();
                    }

                    if (user.AccessCodeNotifications)
                    {
                        AccessCodeApplicantActivatedContextDO contextObl = new AccessCodeApplicantActivatedContextDO(
                            request.PaymentRequestIdentifier,
                            request.ServiceProviderName,
                            request.PaymentReason,
                            request.PaymentAmount,
                            request.ObligationStatusId.GetDescription(),
                            request.PaymentRequestAccessCode);

                        Email emailObl = new Email(contextObl, user.Email);
                        this.systemRepository.AddEntity<Email>(emailObl);
                    }
                }

                this.unitOfWork.Save();
            }

            if (request.ObligationStatusId == ObligationStatusEnum.Asked)
            {
                request.ObligationStatusId = ObligationStatusEnum.Ordered;

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    if (user.StatusObligationNotifications)
                    {
                        Email email = CreateObligationEmail(request, user);
                        this.systemRepository.AddEntity<Email>(email);

                        this.unitOfWork.Save();
                    }
                }

                this.unitOfWork.Save();
            }


            ShareVM model = new ShareVM();

            model.Gid = id;
            model.AccessCode = request.PaymentRequestAccessCode;
            model.Email = user.Email;

            if (!String.IsNullOrWhiteSpace(request.PaymentRequestAccessCode))
            {
                model.Link = Formatter.UriCombine(AppSettings.EPaymentsCommon_WebAddress, MVC.Home.Name, MVC.Home.ActionNames.AccessByCode).ToString() + String.Format("?code={0}", request.PaymentRequestAccessCode);
            }

            return View(model);
        }

        [HttpPost]
        [WebUserAuthorize]
        public virtual ActionResult Share(ShareVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PaymentRequest request = this.webRepository.GetPaymentRequestByGidAndUin(model.Gid, this.CurrentUser.Uin);

            SharePaymentContextDO contextDO = new SharePaymentContextDO(
                request.ApplicantName,
                request.PaymentRequestAccessCode,
                request.PaymentRequestIdentifier,
                request.ServiceProviderName,
                request.PaymentReason,
                request.PaymentAmount);

            Email email = new Email(contextDO, model.Email);
            this.systemRepository.AddEntity<Email>(email);
            this.unitOfWork.Save();

            TempData[TempDataKeys.SharePaymentEmailSend] = true;

            return RedirectToAction(MVC.Payment.ActionNames.Share, MVC.Payment.Name, new { id = request.Gid });
        }

        [HttpGet]
        [WebUserAuthorize]
        public virtual ActionResult GenerateAccessCode(Guid id)
        {
            PaymentRequest request = this.webRepository.GetPaymentRequestByGidAndUin(id, this.CurrentUser.Uin);
            request.PaymentRequestAccessCode = this.commonRepository.GeneratePaymentRequestAccessCode();

            User user = this.systemRepository.GetUserById(this.CurrentUser.UserId.Value);
            if (!String.IsNullOrWhiteSpace(user.Email) && user.AccessCodeNotifications)
            {
                AccessCodeActivatedContextDO contextDO = new AccessCodeActivatedContextDO(
                    request.ApplicantName,
                    request.PaymentRequestAccessCode,
                    request.PaymentRequestIdentifier,
                    request.ServiceProviderName,
                    request.PaymentReason,
                    request.PaymentAmount);

                Email email = new Email(contextDO, user.Email);

                this.systemRepository.AddEntity<Email>(email);
            }

            this.unitOfWork.Save();

            return RedirectToAction(MVC.Payment.ActionNames.Share, MVC.Payment.Name, new { id = id });
        }

        [NonAction]
        private ActionResult RedirectToListAction(ListSearchDO searchDO)
        {
            return RedirectToAction(MVC.Payment.ActionNames.List, MVC.Payment.Name,
                new
                {
                    @pPage = searchDO.PPage,
                    @pSortBy = searchDO.PSortBy,
                    @pSortDesc = searchDO.PSortDesc,

                    @prId = searchDO.PrId,
                    @prDateFrom = searchDO.PrDateFrom,
                    @prDateTo = searchDO.PrDateTo,
                    @prAmountFrom = searchDO.PrAmountFrom,
                    @prAmountTo = searchDO.PrAmountTo,
                    @prProvider = searchDO.PrProvider,
                    @prReason = searchDO.PrReason,
                    @prStatus = searchDO.PrStatus,

                    @prPage = searchDO.PrPage,
                    @prSortBy = searchDO.PrSortBy,
                    @prSortDesc = searchDO.PrSortDesc,

                    @focus = searchDO.Focus,
                });
        }

        [NonAction]
        private PaymentOrderVO GetPaymentOrderVO(Guid id)
        {
            if (!this.CurrentUser.IsAuthorizedByAccessCode)
            {
                return this.webRepository.GetPaymentOrderByGidAndUin(id, this.CurrentUser.Uin);
            }
            else
            {
                return this.webRepository.GetPaymentOrderByAccessCode(this.CurrentUser.AccessCode);
            }
        }

        [NonAction]
        private Email CreateObligationEmail(PaymentRequest request, User user)
        {
            StatusChangedObligationContextDO contextObl = new StatusChangedObligationContextDO(request.PaymentRequestIdentifier,
                request.ServiceProviderName,
                request.PaymentReason,
                request.PaymentAmount,
                request.ObligationStatusId.GetDescription());

            Email email = new Email(contextObl, user.Email);

            return email;
        }
    }
}