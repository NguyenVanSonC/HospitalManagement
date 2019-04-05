using SunShineHospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SunShineHospital.Model.Models;
using System.Web.Http.Description;
using AutoMapper;
using SunShineHospital.Models;
using System.Threading.Tasks;

namespace SunShineHospital.Api
{
    [RoutePrefix("api/appointments")]
    public class AppointmentController : ApiController
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

        [Route("")]
        [ResponseType(typeof(IEnumerable<AppoinmentViewModel>))]
        public IHttpActionResult GetAll()
        {
            var listAppointmentModel =  _appoinmentService.GetAll();
            if (listAppointmentModel.Count()>0)
            {
                var listAppoitmentViewModel =
                    Mapper.Map<IEnumerable<Appoinment>, IEnumerable<AppoinmentViewModel>>(listAppointmentModel);

                return Ok(listAppoitmentViewModel);
            }
            else
            {
                return NotFound();
            }
            
        }

        [Route("{id:int}")]
        [ResponseType(typeof(AppoinmentViewModel))]
        public IHttpActionResult Get(int id)
        {
            var appointmentModel =  _appoinmentService.GetById(id);
            if (appointmentModel != null)
            {
                var appoitmentViewModel = Mapper.Map<Appoinment, AppoinmentViewModel>(appointmentModel);
                return Ok(appoitmentViewModel);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
