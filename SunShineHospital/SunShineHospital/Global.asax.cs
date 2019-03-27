using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SunShineHospital.Infrastructure.Binders;
using SunShineHospital.Models;
using TeduShop.Web.Mappings;

namespace SunShineHospital
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfiguration.Configure();
            ModelBinders.Binders.Add(typeof(BookingViewModel), new BookingModelBinder());
        }
    }
}
