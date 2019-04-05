using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using SunShineHospital.Infrastructure.Core;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;
using SunShineHospital.Common;
using SunShineHospital.Infrastructure.Extensions;
using SunShineHospital.Web.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace SunShineHospital.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorService _doctorService;
        private IDepartmentService _departmentService;
        private ApplicationUserManager _userManager;

        public DoctorController(IDoctorService doctorService, IDepartmentService departmentService, ApplicationUserManager userManager)
        {
            this._doctorService = doctorService;
            this._departmentService = departmentService;
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            var listDepartmentModel = _departmentService.GetAll();
            var listDepartmentViewModel =
                Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(listDepartmentModel);
            IEnumerable<SelectListItem> selectListDepartment = listDepartmentViewModel.OrderBy(m => m.Name).Select(m =>
                new SelectListItem()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name
                });
            ViewBag.listDepartment = selectListDepartment;
            return View();
        }

        [HttpPost]
        public ActionResult Register(DoctorViewModel doctorViewModel)
        {
            if (Request.IsAuthenticated && ModelState.IsValid)
            {
                var user = _userManager.FindById(User.Identity.GetUserId());
                var doctorModel = new Doctor();
                doctorModel.UpdateDoctor(doctorViewModel);
                doctorModel.CreatedBy = user.FullName;
                doctorModel.UserId = user.Id;
                doctorModel.Salary = 7000000;
                doctorModel.Image = "Assets/Client/img/doctor.jpg";
                doctorModel.Alias = GetAliasByName.GetAlias(user.FullName);
                doctorModel.CreatedDate = DateTime.Now;
                doctorModel.Status = true;
                doctorModel.HomeFlag = true;
                _doctorService.Add(doctorModel);
                _userManager.AddToRolesAsync(user.Id, new string[] {"Doctor"});
                _doctorService.Save();
                TempData["message"] = string.Format("Chaò mừng bạn đã trở thành bác sỹ của chúng tôi !");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }


        public ActionResult Detail(int id)
        {
            var model = _doctorService.GetById(id);
            var doctorVm = Mapper.Map<Doctor, DoctorViewModel>(model);
            return View(doctorVm);
        }
        public ActionResult Department(int id, int page = 1, string soft = "")
        {
            int pageSize = int.Parse(ConfigHepper.GetByKey("PageSize"));
            var model = _doctorService.GetListDoctorByDepartmentIdPaging(id, page, pageSize, soft, out int totalRow);
            var listDoctor = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(model);
            int totalPage = (int) Math.Ceiling((double) totalRow / pageSize);
            var department = _departmentService.GetById(id);
            ViewBag.department = Mapper.Map<Department, DepartmentViewModel>(department);

            ViewBag.pageSize = pageSize;
            ViewBag.soft = soft;
            if (totalRow < pageSize)
            {
                ViewBag.count = totalRow;
            }
            else
            {
                ViewBag.count = pageSize;
            }
            var paginationSet = new PaginationSet<DoctorViewModel>()
            {
                Items = listDoctor,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }

        public PartialViewResult GetHotDoctor()
        {
            int top = int.Parse(ConfigHepper.GetByKey("TopSize"));
            var model = _doctorService.GetHotDoctor(top);
            var listHotDoctor = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(model);
            return PartialView(listHotDoctor);
        }

        public ActionResult GetAllDoctor(string keyword = "", int page = 1, string soft = "")
        {
            int pageSize = int.Parse(ConfigHepper.GetByKey("PageSize"));
            var model = _doctorService.Search(keyword, page, pageSize, soft, out int totalRow);
            var doctorViewModel = Mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(model);
            int totalPage = (int) Math.Ceiling((double) totalRow / pageSize);
            ViewBag.pageSize = pageSize;
            ViewBag.soft = soft;
            if (totalRow< pageSize)
            {
                ViewBag.count = totalRow;
            }
            else
            {
                ViewBag.count = pageSize;
            }
            var paginationSet = new PaginationSet<DoctorViewModel>()
            {
                Items = doctorViewModel,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }
    }
}