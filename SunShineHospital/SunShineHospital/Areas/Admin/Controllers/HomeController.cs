using Microsoft.AspNet.Identity;
using SunShineHospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SunShineHospital.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Doctor")]
    public class HomeController : Controller
    {
        #region MyRegion
        private readonly IAppoinmentService _appoinmentService;
        private readonly IDoctorService _doctorService;
        public HomeController(IAppoinmentService appoinmentService, IDoctorService doctorService)
        {
            this._appoinmentService = appoinmentService;
            this._doctorService = doctorService;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Header()
        {
            var userId = User.Identity.GetUserId();
            var doctorModel = _doctorService.GetDoctorIdByUserId(userId);
            var doctorId = doctorModel != null? doctorModel.ID : 0;
            return PartialView(doctorId);
        }
    }
}