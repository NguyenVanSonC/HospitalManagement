using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SunShineHospital.Infrastructure.Core;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;
using SunShineHospital.Common;

namespace SunShineHospital.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorService _doctorService;
        private IDepartmentService _departmentService;

        public DoctorController(IDoctorService doctorService, IDepartmentService departmentService)
        {
            this._doctorService = doctorService;
            this._departmentService = departmentService;
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