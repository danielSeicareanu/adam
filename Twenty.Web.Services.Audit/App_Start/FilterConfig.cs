using System.Web;
using System.Web.Mvc;

namespace Twenty.Web.Services.Audit
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}