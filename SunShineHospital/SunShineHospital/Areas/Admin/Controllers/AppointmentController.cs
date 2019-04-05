using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SunShineHospital.Areas.Admin.Models;
using SunShineHospital.Common;
using SunShineHospital.Infrastructure.Core;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;

namespace SunShineHospital.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Appointment")]
    [Authorize(Roles = "Admin,Doctor")]
    public class AppointmentController : Controller
    {
        #region MyRegion
        private readonly IAppoinmentService _appoinmentService;
        private readonly IDoctorService _doctorService;
        public AppointmentController(IAppoinmentService appoinmentService, IDoctorService doctorService)
        {
            this._appoinmentService = appoinmentService;
            this._doctorService = doctorService;
        }
        #endregion

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetAppointmentById(int id)
        {
            AppoinmentViewModel appoinmentViewModel= null;
            HttpResponseMessage response = GlobalVariableApiHttp.WebApiClient.GetAsync("appointments/" + id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                appoinmentViewModel = response.Content.ReadAsAsync<AppoinmentViewModel>().Result;
            }

            ViewBag.AppointmentId = id;
            var createPrescription = new CreatePrescription()
            {
                AppointmentId = id
            };
            var mutilModel = new MutilModelPrescription()
            {
                AppoinmentViewModel = appoinmentViewModel,
                CreatePrescription = createPrescription
            };

            return View(mutilModel);
        }

        [Route("GetAll/{doctorId:int}")]
        [HttpGet]
        public ActionResult GetAppointments(int doctorId, string keyword = "", int page = 1, string soft = "")
        {
            int pageSize = int.Parse(ConfigHepper.GetByKey("AdminPageSize"));
            var listAppointmentModel = _appoinmentService.GetAppoinmentByDoctorId(doctorId, keyword, page, pageSize, soft, out int totalRow);
            var listAppointmentViewModel =
                Mapper.Map<IEnumerable<Appoinment>, IEnumerable<AppoinmentViewModel>>(listAppointmentModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            ViewBag.pageSize = pageSize;
            ViewBag.soft = soft;
            ViewBag.doctorId = doctorId;
            if (totalRow < pageSize)
            {
                ViewBag.count = totalRow;
            }
            else
            {
                ViewBag.count = pageSize;
            }
            var paginationSet = new PaginationSet<AppoinmentViewModel>()
            {
                Items = listAppointmentViewModel,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
    }
}