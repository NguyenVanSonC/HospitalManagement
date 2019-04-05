using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;

namespace SunShineHospital.Api
{
    [RoutePrefix("api/medicinecategories")]
    public class MedicineCategoryController : ApiController
    {
        #region MyRegion
        private readonly IMedicineCategoryService _medicineCategoryService;

        public MedicineCategoryController(IMedicineCategoryService medicineCategoryService)
        {
            this._medicineCategoryService = medicineCategoryService;
        }
        #endregion

        [Route("")]
        [ResponseType(typeof(IEnumerable<MedicineCategoryViewModel>))]
        public IHttpActionResult GetAll()
        {
            var listMedicineCategoryModel = _medicineCategoryService.GetAll();
            if (listMedicineCategoryModel.Count() > 0)
            {
                var listMedicineCategoryViewModel =
                    Mapper.Map<IEnumerable<MedicineCategory>, IEnumerable<MedicineCategoryViewModel>>(listMedicineCategoryModel);

                return Ok(listMedicineCategoryViewModel);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
