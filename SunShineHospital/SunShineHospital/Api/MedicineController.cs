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
    [RoutePrefix("api/medicines")]
    public class MedicineController : ApiController
    {
        #region MyRegion
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            this._medicineService = medicineService;
        }
        #endregion

        [Route("category/{categoryId:int}")]
        [ResponseType(typeof(IEnumerable<MedicineViewModel>))]
        public IHttpActionResult GetByCategoryId(int categoryId)
        {
            var listMedicineModel = _medicineService.GetByCategoryId(categoryId);
            if (listMedicineModel.Count() > 0)
            {
                var listMedicineViewModel =
                    Mapper.Map<IEnumerable<Medicine>, IEnumerable<MedicineViewModel>>(listMedicineModel);
                return Ok(listMedicineViewModel);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
