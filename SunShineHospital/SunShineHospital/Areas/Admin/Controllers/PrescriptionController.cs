using SunShineHospital.Areas.Admin.Models;
using SunShineHospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using SunShineHospital.Model.Models;
using TeduShop.Common;
using SunShineHospital.Models;
using Microsoft.AspNet.Identity;

namespace SunShineHospital.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Prescription")]
    [Authorize(Roles = "Admin,Doctor")]
    public class PrescriptionController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPrescriptionDetailService _prescriptionDetailService;
        private readonly IMedicineService _medicineService;
        private readonly IDoctorService _doctorService;
        private readonly IAppoinmentService _appoinmentService;

        public PrescriptionController(IPrescriptionService prescriptionService, IPrescriptionDetailService prescriptionDetailService, 
            IMedicineService medicineService, IDoctorService doctorService, IAppoinmentService appoinmentService)
        {
            this._prescriptionDetailService = prescriptionDetailService;
            this._prescriptionService = prescriptionService;
            this._medicineService = medicineService;
            this._doctorService = doctorService;
            this._appoinmentService = appoinmentService;
        }

        public ActionResult CreatePresciption([Bind(Prefix = "CreatePrescription")]CreatePrescription createPrescription)
        {
            if (ModelState.IsValid && Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var doctorModel = _doctorService.GetDoctorIdByUserId(userId);
                var userName = User.Identity.GetUserName();
                var prescriptionModel = new Prescription()
                {
                    AppointmentID = createPrescription.AppointmentId,
                    Diagnose = createPrescription.Diagnose,
                    CreatedDate = DateTime.Now,
                    CreatedBy = userName,
                    Status = true
                };
                var cart = (List<MedicineCartViewModel>)Session[CommonConstants.SessionPrescription];
                var listMedicineDetail = new List<PrescriptionDetail>();
                foreach (var medicine in cart)
                {
                    if (medicine.AppointmentId == createPrescription.AppointmentId)
                    {
                        var prescriptionDetail = new PrescriptionDetail()
                        {
                            MedicineID = medicine.MedicineId,
                            Quantitty = medicine.Quantity,
                            Calendar = medicine.Calendar,
                            TotalDay = medicine.TotalDay,
                            BeforeMeal = medicine.BeforeMeal,
                            Status = true
                        };
                        listMedicineDetail.Add(prescriptionDetail);
                    }                   
                }

                bool check = _prescriptionService.CreatePrescription(prescriptionModel, listMedicineDetail);
                if (check)
                {
                    var appoinmentModel = _appoinmentService.GetById(prescriptionModel.AppointmentID);
                    appoinmentModel.Status = false;
                    _appoinmentService.Update(appoinmentModel);
                    _prescriptionService.Save();
                    cart.RemoveAll(x => x.AppointmentId == createPrescription.AppointmentId);
                    Session[CommonConstants.SessionPrescription] = cart;
                }
                return RedirectToAction("GetAppointments", "Appointment", new { doctorId = doctorModel.ID });
            }
            else
            {
                return RedirectToAction("GetAppointmentById", "Appointment", new { id = createPrescription.AppointmentId });
            }
        }

        [Route("Update")]
        [HttpGet]
        public JsonResult Update(string medicineViewModel)
        {
            if (!String.IsNullOrEmpty(medicineViewModel))
            {
                var medicine = new JavaScriptSerializer().Deserialize<MedicineCartViewModel>(medicineViewModel);
                var cart = (List<MedicineCartViewModel>) Session[CommonConstants.SessionPrescription];
                cart = cart ?? new List<MedicineCartViewModel>();
                bool IsSameAppointment(MedicineCartViewModel m1, MedicineCartViewModel m2) =>
                    m1.MedicineId == m2.MedicineId && m1.AppointmentId == m2.AppointmentId;
                if (cart.Any(x => IsSameAppointment(x, medicine)))
                {
                    var itemIndex = cart.FindIndex(x => IsSameAppointment(x, medicine));
                    var item = cart.ElementAt(itemIndex);
                    medicine.Medicine = item.Medicine;
                    cart.RemoveAt(itemIndex);
                    cart.Insert(itemIndex, medicine);
                    Session[CommonConstants.SessionPrescription] = cart;
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
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [Route("Delete")]
        [HttpGet]
        public JsonResult Delete(int medicineId, int appointmentId)
        {
            var cart = (List<MedicineCartViewModel>)Session[CommonConstants.SessionPrescription];
            cart = cart ?? new List<MedicineCartViewModel>();
            bool IsSameAppointment(MedicineCartViewModel m1) =>
                m1.MedicineId == medicineId && m1.AppointmentId == appointmentId;

            var medicineModel = cart.Where(x => IsSameAppointment(x)).Single();
            if (cart.Count > 0)
            {
                cart.Remove(medicineModel);
                Session[CommonConstants.SessionPrescription] = cart;
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

        [Route("DeleteAll")]
        [HttpGet]
        public JsonResult DeleteAll(int appointmentId)
        {
            var cart = (List<MedicineCartViewModel>) Session[CommonConstants.SessionPrescription];
            cart = cart ?? new List<MedicineCartViewModel>();
            if (cart.Count > 0)
            {
                cart.RemoveAll(x => x.AppointmentId == appointmentId);
                Session[CommonConstants.SessionPrescription] = cart;
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

        [Route("GetAll")]
        [HttpGet]
        public JsonResult GetAll(int appointmentId)
        {
            var cart = (List<MedicineCartViewModel>)Session[CommonConstants.SessionPrescription];
            cart = cart ?? new List<MedicineCartViewModel>();
            var listMedicine = cart.Where(m => m.AppointmentId == appointmentId).ToList();
            if (listMedicine.Count > 0)
            {
                return Json(new
                {
                    data = listMedicine,
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

        [Route("Create")]
        [HttpPost]
        public JsonResult Create(string medicineViewModel)
        {
            if (!String.IsNullOrEmpty(medicineViewModel))
            {
                var medicine = new JavaScriptSerializer().Deserialize<MedicineCartViewModel>(medicineViewModel);
                var cart = (List<MedicineCartViewModel>)Session[CommonConstants.SessionPrescription];
                cart = cart ?? new List<MedicineCartViewModel>();

                bool IsSameAppointment(MedicineCartViewModel m1, MedicineCartViewModel m2) =>
                    m1.MedicineId == m2.MedicineId && m1.AppointmentId == m2.AppointmentId;
                if (cart.Any(x => IsSameAppointment(x, medicine)))
                {
                    return Json(new
                    {
                        status = false,
                        message = "duplicate-medicine"
                    });
                }
                else
                {
                    var medicineModel = _medicineService.GetById(medicine.MedicineId);
                    medicine.Medicine = Mapper.Map<Medicine, MedicineViewModel>(medicineModel);
                    cart.Add(medicine);
                    Session[CommonConstants.SessionPrescription] = cart;
                    return Json(new
                    {
                        status = true
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}