using EPayments.Common.Filters;
using System.Web;
using System.Web.Mvc;

namespace EPayments.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NLogTraceFilter());
            filters.Add(new Log4NetExceptionFilter());
        }
    }
}
