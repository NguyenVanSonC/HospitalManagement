using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineHospital.Controllers
{
    public class RateController : Controller
    {
        public PartialViewResult Index(int id)
        {
            ViewBag.doctorId = id;
            return PartialView();
        }
    }
}