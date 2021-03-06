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
    public partial class AccountController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AccountController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult EAuthLogin()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuthResponse);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AuthPassLogin()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AuthPassLogin);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Eid()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Eid);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult NoiAuth()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NoiAuth);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EDeliveryAuth()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EDeliveryAuth);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EAuthAdminRedirect()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuthAdminRedirect);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EAuthAdminRedirectTest()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuthAdminRedirectTest);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountController Actions { get { return MVC.Account; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Account";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Account";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Login = "Login";
            public readonly string Logout = "Logout";
            public readonly string LogoutEserviceAdminUser = "LogoutEserviceAdminUser";
            public readonly string EAuth = "EAuth";
            public readonly string EAuthResponse = "EAuthResponse";
            public readonly string AuthPassLogin = "AuthPassLogin";
            public readonly string EidSubmitForm = "EidSubmitForm";
            public readonly string Eid = "Eid";
            public readonly string NoiAuth = "NoiAuth";
            public readonly string EDeliveryAuth = "EDeliveryAuth";
            public readonly string EAuthAdminRedirect = "EAuthAdminRedirect";
            public readonly string EAuthAdminRedirectTest = "EAuthAdminRedirectTest";
            public readonly string EAuthMetadata = "Metadata";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Login = "Login";
            public const string Logout = "Logout";
            public const string LogoutEserviceAdminUser = "LogoutEserviceAdminUser";
            public const string EAuth = "EAuth";
            public const string EAuthLogin = "EAuthLogin";
            public const string AuthPassLogin = "AuthPassLogin";
            public const string EidSubmitForm = "EidSubmitForm";
            public const string Eid = "Eid";
            public const string NoiAuth = "NoiAuth";
            public const string EDeliveryAuth = "EDeliveryAuth";
            public const string EAuthAdminRedirect = "EAuthAdminRedirect";
            public const string EAuthAdminRedirectTest = "EAuthAdminRedirectTest";
        }


        static readonly ActionParamsClass_EAuthLogin s_params_EAuthLogin = new ActionParamsClass_EAuthLogin();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EAuthLogin EAuthLoginParams { get { return s_params_EAuthLogin; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EAuthLogin
        {
            public readonly string samlResponse = "samlResponse";
            public readonly string relayState = "relayState";
        }
        static readonly ActionParamsClass_AuthPassLogin s_params_AuthPassLogin = new ActionParamsClass_AuthPassLogin();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AuthPassLogin AuthPassLoginParams { get { return s_params_AuthPassLogin; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AuthPassLogin
        {
            public readonly string requestDO = "requestDO";
        }
        static readonly ActionParamsClass_Eid s_params_Eid = new ActionParamsClass_Eid();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Eid EidParams { get { return s_params_Eid; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Eid
        {
            public readonly string Target = "Target";
            public readonly string URL = "URL";
            public readonly string SAMLArtifact = "SAMLArtifact";
        }
        static readonly ActionParamsClass_NoiAuth s_params_NoiAuth = new ActionParamsClass_NoiAuth();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NoiAuth NoiAuthParams { get { return s_params_NoiAuth; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NoiAuth
        {
            public readonly string jwt = "jwt";
        }
        static readonly ActionParamsClass_EDeliveryAuth s_params_EDeliveryAuth = new ActionParamsClass_EDeliveryAuth();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EDeliveryAuth EDeliveryAuthParams { get { return s_params_EDeliveryAuth; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EDeliveryAuth
        {
            public readonly string token = "token";
        }
        static readonly ActionParamsClass_EAuthAdminRedirect s_params_EAuthAdminRedirect = new ActionParamsClass_EAuthAdminRedirect();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EAuthAdminRedirect EAuthAdminRedirectParams { get { return s_params_EAuthAdminRedirect; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EAuthAdminRedirect
        {
            public readonly string samlResponse = "samlResponse";
            public readonly string relayState = "relayState";
        }
        static readonly ActionParamsClass_EAuthAdminRedirectTest s_params_EAuthAdminRedirectTest = new ActionParamsClass_EAuthAdminRedirectTest();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EAuthAdminRedirectTest EAuthAdminRedirectTestParams { get { return s_params_EAuthAdminRedirectTest; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EAuthAdminRedirectTest
        {
            public readonly string samlResponse = "samlResponse";
            public readonly string relayState = "relayState";
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_AccountController : EPayments.Web.Controllers.AccountController
    {
        public T4MVC_AccountController() : base(Dummy.Instance) { }

        [NonAction]
        partial void LoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Threading.Tasks.Task<System.Web.Mvc.ActionResult> Login()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Login);
            LoginOverride(callInfo);
            return System.Threading.Tasks.Task.FromResult(callInfo as ActionResult);
        }

        [NonAction]
        partial void LogoutOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Logout()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Logout);
            LogoutOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void LogoutEserviceAdminUserOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult LogoutEserviceAdminUser()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LogoutEserviceAdminUser);
            LogoutEserviceAdminUserOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EAuthOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult EAuth()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuth);
            EAuthOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EAuthLoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string samlResponse);

        [NonAction]
        public override System.Web.Mvc.ActionResult EAuthResponse(string samlResponse)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuthResponse);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "samlResponse", samlResponse);
            EAuthLoginOverride(callInfo, samlResponse);
            return callInfo;
        }

        [NonAction]
        partial void AuthPassLoginOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, EPayments.Common.DataObjects.AuthRequestDO requestDO);

        [NonAction]
        public override System.Web.Mvc.ActionResult AuthPassLogin(EPayments.Common.DataObjects.AuthRequestDO requestDO)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AuthPassLogin);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "requestDO", requestDO);
            AuthPassLoginOverride(callInfo, requestDO);
            return callInfo;
        }

        [NonAction]
        partial void EidSubmitFormOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult EidSubmitForm()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EidSubmitForm);
            EidSubmitFormOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void EidOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string Target, string URL, string SAMLArtifact);

        [NonAction]
        public override System.Web.Mvc.ActionResult Eid(string Target, string URL, string SAMLArtifact)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Eid);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Target", Target);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "URL", URL);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "SAMLArtifact", SAMLArtifact);
            EidOverride(callInfo, Target, URL, SAMLArtifact);
            return callInfo;
        }

        [NonAction]
        partial void NoiAuthOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string jwt);

        [NonAction]
        public override System.Web.Mvc.ActionResult NoiAuth(string jwt)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NoiAuth);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "jwt", jwt);
            NoiAuthOverride(callInfo, jwt);
            return callInfo;
        }

        [NonAction]
        partial void EDeliveryAuthOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string token);

        [NonAction]
        public override System.Web.Mvc.ActionResult EDeliveryAuth(string token)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EDeliveryAuth);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "token", token);
            EDeliveryAuthOverride(callInfo, token);
            return callInfo;
        }

        [NonAction]
        partial void EAuthAdminRedirectOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string samlResponse, string relayState);

        [NonAction]
        public override System.Web.Mvc.ActionResult EAuthAdminRedirect(string samlResponse, string relayState)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuthAdminRedirect);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "samlResponse", samlResponse);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "relayState", relayState);
            EAuthAdminRedirectOverride(callInfo, samlResponse, relayState);
            return callInfo;
        }

        [NonAction]
        partial void EAuthAdminRedirectTestOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string samlResponse, string relayState);

        [NonAction]
        public override System.Web.Mvc.ActionResult EAuthAdminRedirectTest(string samlResponse, string relayState)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EAuthAdminRedirectTest);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "samlResponse", samlResponse);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "relayState", relayState);
            EAuthAdminRedirectTestOverride(callInfo, samlResponse, relayState);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
