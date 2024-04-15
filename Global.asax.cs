using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using www.otoparcabul.com.Models;

namespace www.otoparcabul.com
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var dc = new OtoParcaBulEntities();
            Application["Cities"] = dc.Cities.OrderBy(e => e.Name).ToList();
            Application["Categories"] = dc.Categories.OrderBy(e => e.ParentCategoryID).ThenBy(e => e.Name).ToList();
            Application["VehicleTypes"] = dc.VehicleTypes.OrderBy(e => e.Name).ToList();
            Application["Brands"] = dc.Brands.Where(e => e.IsActive).OrderBy(e => e.Name).ToList();
            Application["BrandModels"] = dc.BrandModels.OrderBy(e => e.BrandID).ThenBy(e => e.Name).ToList();

            MailHelper.Init();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}