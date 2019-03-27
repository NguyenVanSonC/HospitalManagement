using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;

namespace SunShineHospital.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        public PartialViewResult MenuDepartment()
        {
            var model = _departmentService.GetAll();
            var listDepartmentViewModel = Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(model);
            return PartialView(listDepartmentViewModel);
        }
    }
}