using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SunShineHospital
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "ThankPatient",
                url: "thank-booking.html",
                defaults: new { controller = "Booking", action = "ThankPatient", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Checkout",
                url: "dat-lich.html",
                defaults: new { controller = "Booking", action = "CreateBooking", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky.html",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Booking",
                url: "booking-doctor.html",
                defaults: new { controller = "Booking", action = "BookingDoctor", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Register Doctor",
                url: "register-doctor.html",
                defaults: new { controller = "Doctor", action = "Register", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "View Doctor",
                url: "view-all-doctor.html",
                defaults: new { controller = "Doctor", action = "GetAllDoctor", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Doctor Department",
                url: "{alias}.dp-{id}.html",
                defaults: new { controller = "Doctor", action = "Department", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Doctor Detail",
                url: "{alias}.doc-{id}.html",
                defaults: new { controller = "Doctor", action = "Detail", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SunShineHospital.Controllers" }
            );
        }
    }
}
