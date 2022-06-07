// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace EPayments.Web.Controllers
{
    public partial class HomeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected HomeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Help()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Help);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AccessByCode()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessByCode);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SystemStats()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SystemStats);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult SystemStatsSearch()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SystemStatsSearch);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult DownloadFile()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.DownloadFile);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult RedirectToError()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToError);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public HomeController Actions { get { return MVC.Home; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Home";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Home";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string BanksInfo = "BanksInfo";
            public readonly string Help = "Help";
            public readonly string Departments = "Departments";
            public readonly string Feedback = "Feedback";
            public readonly string AccessByCode = "AccessByCode";
            public readonly string AccessByEserviceAdmin = "AccessByEserviceAdmin";
            public readonly string AccessibilityPolicy = "AccessibilityPolicy";
            public readonly string AccessRules = "AccessRules";
            public readonly string About = "About";
            public readonly string SystemStats = "SystemStats";
            public readonly string SystemStatsSearch = "SystemStatsSearch";
            public readonly string DownloadFile = "DownloadFile";
            public readonly string RedirectToError = "RedirectToError";
            public readonly string Error = "Error";
            public readonly string EidLogin = "EidLogin";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string BanksInfo = "BanksInfo";
            public const string Help = "Help";
            public const string Departments = "Departments";
            public const string Feedback = "Feedback";
            public const string AccessByCode = "AccessByCode";
            public const string AccessByEserviceAdmin = "AccessByEserviceAdmin";
            public const string AccessibilityPolicy = "AccessibilityPolicy";
            public const string AccessRules = "AccessRules";
            public const string About = "About";
            public const string SystemStats = "SystemStats";
            public const string SystemStatsSearch = "SystemStatsSearch";
            public const string DownloadFile = "DownloadFile";
            public const string RedirectToError = "RedirectToError";
            public const string Error = "Error";
            public const string EidLogin = "EidLogin";
        }


        static readonly ActionParamsClass_Help s_params_Help = new ActionParamsClass_Help();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Help HelpParams { get { return s_params_Help; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Help
        {
            public readonly string focus = "focus";
        }
        static readonly ActionParamsClass_Feedback s_params_Feedback = new ActionParamsClass_Feedback();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Feedback FeedbackParams { get { return s_params_Feedback; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Feedback
        {
            public readonly string model = "model";
            public readonly string captchaValid = "captchaValid";
        }
        static readonly ActionParamsClass_AccessByCode s_params_AccessByCode = new ActionParamsClass_AccessByCode();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AccessByCode AccessByCodeParams { get { return s_params_AccessByCode; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AccessByCode
        {
            public readonly string code = "code";
            public readonly string model = "model";
            public readonly string captchaValid = "captchaValid";
        }
        static readonly ActionParamsClass_AccessByEserviceAdmin s_params_AccessByEserviceAdmin = new ActionParamsClass_AccessByEserviceAdmin();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AccessByEserviceAdmin AccessByEserviceAdminParams { get { return s_params_AccessByEserviceAdmin; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AccessByEserviceAdmin
        {
            public readonly string model = "model";
            public readonly string captchaValid = "captchaValid";
        }
        static readonly ActionParamsClass_SystemStats s_params_SystemStats = new ActionParamsClass_SystemStats();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SystemStats SystemStatsParams { get { return s_params_SystemStats; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SystemStats
        {
            public readonly string searchDO = "searchDO";
        }
        static readonly ActionParamsClass_SystemStatsSearch s_params_SystemStatsSearch = new ActionParamsClass_SystemStatsSearch();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SystemStatsSearch SystemStatsSearchParams { get { return s_params_SystemStatsSearch; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SystemStatsSearch
        {
            public readonly string searchDO = "searchDO";
        }
        static readonly ActionParamsClass_DownloadFile s_params_DownloadFile = new ActionParamsClass_DownloadFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DownloadFile DownloadFileParams { get { return s_params_DownloadFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DownloadFile
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_RedirectToError s_params_RedirectToError = new ActionParamsClass_RedirectToError();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_RedirectToError RedirectToErrorParams { get { return s_params_RedirectToError; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_RedirectToError
        {
            public readonly string id = "id";
            public readonly string logId = "logId";
            public readonly string egn = "egn";
            public readonly string isIisError = "isIisError";
            public readonly string url = "url";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string About = "About";
                public readonly string AccessByCode = "AccessByCode";
                public readonly string AccessByEserviceAdmin = "AccessByEserviceAdmin";
                public readonly string AccessibilityPolicy = "AccessibilityPolicy";
                public readonly string AccessRules = "AccessRules";
                public readonly string BanksInfo = "BanksInfo";
                public readonly string Departments = "Departments";
                public readonly string EidLogin = "EidLogin";
                public readonly string Feedback = "Feedback";
                public readonly string Help = "Help";
                public readonly string Index = "Index";
                public readonly string SystemStats = "SystemStats";
            }
            public readonly string About = "~/Views/Home/About.cshtml";
            public readonly string AccessByCode = "~/Views/Home/AccessByCode.cshtml";
            public readonly string AccessByEserviceAdmin = "~/Views/Home/AccessByEserviceAdmin.cshtml";
            public readonly string AccessibilityPolicy = "~/Views/Home/AccessibilityPolicy.cshtml";
            public readonly string AccessRules = "~/Views/Home/AccessRules.cshtml";
            public readonly string BanksInfo = "~/Views/Home/BanksInfo.cshtml";
            public readonly string Departments = "~/Views/Home/Departments.cshtml";
            public readonly string EidLogin = "~/Views/Home/EidLogin.cshtml";
            public readonly string Feedback = "~/Views/Home/Feedback.cshtml";
            public readonly string Help = "~/Views/Home/Help.cshtml";
            public readonly string Index = "~/Views/Home/Index.cshtml";
            public readonly string SystemStats = "~/Views/Home/SystemStats.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_HomeController : EPayments.Web.Controllers.HomeController
    {
        public T4MVC_HomeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void BanksInfoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult BanksInfo()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BanksInfo);
            BanksInfoOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void HelpOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string focus);

        [NonAction]
        public override System.Web.Mvc.ActionResult Help(string focus)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Help);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "focus", focus);
            HelpOverride(callInfo, focus);
            return callInfo;
        }

        [NonAction]
        partial void DepartmentsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Departments()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Departments);
            DepartmentsOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void FeedbackOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Feedback()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Feedback);
            FeedbackOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void FeedbackOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, EPayments.Web.Models.Home.FeedbackVM model, bool? captchaValid);

        [NonAction]
        public override System.Web.Mvc.ActionResult Feedback(EPayments.Web.Models.Home.FeedbackVM model, bool? captchaValid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Feedback);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "captchaValid", captchaValid);
            FeedbackOverride(callInfo, model, captchaValid);
            return callInfo;
        }

        [NonAction]
        partial void AccessByCodeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string code);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessByCode(string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessByCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            AccessByCodeOverride(callInfo, code);
            return callInfo;
        }

        [NonAction]
        partial void AccessByCodeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, EPayments.Web.Models.Home.AccessByCodeVM model, bool? captchaValid);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessByCode(EPayments.Web.Models.Home.AccessByCodeVM model, bool? captchaValid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessByCode);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "captchaValid", captchaValid);
            AccessByCodeOverride(callInfo, model, captchaValid);
            return callInfo;
        }

        [NonAction]
        partial void AccessByEserviceAdminOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessByEserviceAdmin()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessByEserviceAdmin);
            AccessByEserviceAdminOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AccessByEserviceAdminOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, EPayments.Web.Models.Home.AccessByEserviceAdminVM model, bool? captchaValid);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessByEserviceAdmin(EPayments.Web.Models.Home.AccessByEserviceAdminVM model, bool? captchaValid)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessByEserviceAdmin);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "captchaValid", captchaValid);
            AccessByEserviceAdminOverride(callInfo, model, captchaValid);
            return callInfo;
        }

        [NonAction]
        partial void AccessibilityPolicyOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessibilityPolicy()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessibilityPolicy);
            AccessibilityPolicyOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AccessRulesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult AccessRules()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AccessRules);
            AccessRulesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AboutOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult About()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.About);
            AboutOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void SystemStatsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, EPayments.Web.DataObjects.SystemStatsSearchDO searchDO);

        [NonAction]
        public override System.Web.Mvc.ActionResult SystemStats(EPayments.Web.DataObjects.SystemStatsSearchDO searchDO)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SystemStats);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchDO", searchDO);
            SystemStatsOverride(callInfo, searchDO);
            return callInfo;
        }

        [NonAction]
        partial void SystemStatsSearchOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, EPayments.Web.DataObjects.SystemStatsSearchDO searchDO);

        [NonAction]
        public override System.Web.Mvc.ActionResult SystemStatsSearch(EPayments.Web.DataObjects.SystemStatsSearchDO searchDO)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SystemStatsSearch);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "searchDO", searchDO);
            SystemStatsSearchOverride(callInfo, searchDO);
            return callInfo;
        }

        [NonAction]
        partial void DownloadFileOverride(T4MVC_System_Web_Mvc_FileResult callInfo, string id);

        [NonAction]
        public override System.Web.Mvc.FileResult DownloadFile(string id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.DownloadFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DownloadFileOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void RedirectToErrorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string id, int? logId, string egn, bool? isIisError, string url);

        [NonAction]
        public override System.Web.Mvc.ActionResult RedirectToError(string id, int? logId, string egn, bool? isIisError, string url)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RedirectToError);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "logId", logId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "egn", egn);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "isIisError", isIisError);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "url", url);
            RedirectToErrorOverride(callInfo, id, logId, egn, isIisError, url);
            return callInfo;
        }

        [NonAction]
        partial void ErrorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Error()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Error);
            ErrorOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EidLoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult EidLogin()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EidLogin);
            EidLoginOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
