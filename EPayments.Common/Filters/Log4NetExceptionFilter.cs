using EPayments.Common.Helpers;
using log4net;
using System.Web.Mvc;

namespace EPayments.Common.Filters
{
    public class Log4NetExceptionFilter : HandleErrorAttribute
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Log4NetExceptionFilter));

        public override void OnException(ExceptionContext filterContext)
        {
            logger.Error(Formatter.ExceptionToDetailedInfo(filterContext.Exception));

            base.OnException(filterContext);
        }
    }
}
