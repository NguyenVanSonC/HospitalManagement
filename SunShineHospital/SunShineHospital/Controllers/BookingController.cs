using AutoMapper;
using Microsoft.AspNet.Identity;
using SunShineHospital.Infrastructure.Extensions;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SunShineHospital.Controllers
{
    public class BookingController : Controller
    {
        private IDoctorService _doctorService;
        private IPatientService _patientService;
        private IAppoinmentService _appoinmentService;

        public BookingController(IDoctorService doctorService, IPatientService patientService, IAppoinmentService appoinmentService)
        {
            this._doctorService = doctorService;
            this._patientService = patientService;
            this._appoinmentService = appoinmentService;
        }

        [HttpPost]
        public JsonResult BookedDoctor(string appoinmentViewModel)
        {
            var appoinment = new JavaScriptSerializer().Deserialize<AppoinmentViewModel>(appoinmentViewModel);
            var appoinmentModel = new Appoinment();
            appoinmentModel.UpdateAppoinment(appoinment);

            if (Request.IsAuthenticated)
            {
                //Get patient by userid
                var userId = User.Identity.GetUserId();
                appoinmentModel.PatientID = _patientService.GetPatientIdByUserId(userId);
                appoinmentModel.CreatedBy = User.Identity.GetUserName();
            }

            _appoinmentService.Add(appoinmentModel);
            _appoinmentService.Save();

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult CreateBooking(BookingViewModel model)
        {
            var doctor = _doctorService.GetById(model.DoctorId);
            model.Doctor = Mapper.Map<Doctor, DoctorViewModel>(doctor);
            return View(model);
        }

        [HttpPost]
        public JsonResult BookingDoctor(BookingViewModel model, int doctorId, string day, string time)
        {
            if (model.DoctorId == doctorId || model.DoctorId == 0)
            {
                var doctor = _doctorService.GetById(doctorId);
                model.Doctor = Mapper.Map<Doctor, DoctorViewModel>(doctor);
                model.DoctorId = doctorId;
                if (!string.IsNullOrEmpty(time))
                {
                    model.Time = time;
                }

                if (!string.IsNullOrEmpty(day))
                {
                    model.Day = day;
                }
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public JsonResult CheckTimeBooking(BookingViewModel model)
        {
            if (string.IsNullOrEmpty(model.Day) || string.IsNullOrEmpty(model.Time))
            {
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                return Json(new
                {
                    status = true
                });
            }
        }
    }
}