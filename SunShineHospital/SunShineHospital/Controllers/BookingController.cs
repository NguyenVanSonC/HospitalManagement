using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SunShineHospital.Infrastructure.Extensions;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SunShineHospital.Web.App_Start;

namespace SunShineHospital.Controllers
{
    public class BookingController : Controller
    {
        private IDoctorService _doctorService;
        private IPatientService _patientService;
        private IAppoinmentService _appoinmentService;
        private ApplicationUserManager _userManager;

        public BookingController(IDoctorService doctorService, IPatientService patientService, 
            IAppoinmentService appoinmentService, ApplicationUserManager userManager)
        {
            this._doctorService = doctorService;
            this._patientService = patientService;
            this._appoinmentService = appoinmentService;
            this._userManager = userManager;
        }

        public ActionResult ThankPatient()
        {
            return View();
        }

        public JsonResult GetCalendarDoctor(int doctorId)
        {
            var listCalendarDoctor = _appoinmentService.GetByDoctorId(doctorId);
            if (listCalendarDoctor != null && listCalendarDoctor.Any())
            {
                return Json(new
                {
                    data = listCalendarDoctor,
                    status = true
                }, JsonRequestBehavior.AllowGet);
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
        public JsonResult BookedDoctor(BookingViewModel model, string appoinmentViewModel)
        {
            var appoinment = new JavaScriptSerializer().Deserialize<AppoinmentViewModel>(appoinmentViewModel);
            var appoinmentModel = new Appoinment();
            appoinmentModel.UpdateAppoinment(appoinment);

            if (Request.IsAuthenticated)
            {
                //Get patient by userid
                var userId = User.Identity.GetUserId();
                appoinmentModel.PatientID = _patientService.GetPatientIdByUserId(userId);
                if (_appoinmentService.CheckAppoinmentByPatientId(appoinmentModel.PatientID))
                {
                    var appoinmentDulicate = _appoinmentService.GetAppoinmentByPatientId(appoinmentModel.PatientID);
                    return Json(new
                    {
                        status = false,
                        appoinmentID = appoinmentDulicate.ID,
                        error = "dulicate-appoinment"
                    });
                }
                appoinmentModel.CreatedBy = User.Identity.GetUserName();
            }
            else
            {
                appoinmentModel.PatientID = null;
            }

            appoinmentModel.Time = model.Time;
            appoinmentModel.Day = model.Day;
            appoinmentModel.CreatedDate = DateTime.Now;
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
                    if (!string.IsNullOrEmpty(model.Time) && day != model.Day)
                    {
                        model.Time = null;
                    }
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
                if (string.IsNullOrEmpty(model.Day))
                {
                    return Json(new
                    {
                        error = "day",
                        status = false
                    });
                }
                else
                {
                    return Json(new
                    {
                        error = "time",
                        status = false
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = true
                });
            }
        }

        [HttpPost]
        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
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

        [HttpGet]
        public JsonResult CancelAppoinment(int appoinmentID)
        {
            if (_appoinmentService.Delete(appoinmentID) != null)
            {
                _appoinmentService.Save();
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}