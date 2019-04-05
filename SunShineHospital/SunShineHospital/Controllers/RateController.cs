using Microsoft.AspNet.Identity;
using SunShineHospital.Infrastructure.Extensions;
using SunShineHospital.Model.Models;
using SunShineHospital.Models;
using SunShineHospital.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using System.Globalization;

namespace SunShineHospital.Controllers
{
    public class RateController : Controller
    {
        private IRateService _rateService;

        public RateController(IRateService rateService)
        {
            this._rateService = rateService;
        }

        public PartialViewResult Index(int id)
        {
            ViewBag.doctorId = id;
            return PartialView();
        }

        public PartialViewResult GetDetail(int id)
        {
            var rateDataModel = GetRateData(id);
            ViewBag.doctorId = id;
            ViewBag.averageRate = rateDataModel.AverageRate;
            return PartialView(rateDataModel.ArrayPercent);
        }

        public JsonResult GetRateDetail(int doctorId)
        {
            var rateDataModel = GetRateData(doctorId);
            if (rateDataModel != null)
            {
                return Json(new
                {
                    status = true,
                    data = rateDataModel
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }           
        }

        [HttpPost]
        public JsonResult CreateRate(string rateViewModel)
        {
            var rate = new JavaScriptSerializer().Deserialize<RateViewModel>(rateViewModel);
            var rateNew = new Rate();
            rateNew.UpdateRate(rate);
            if (Request.IsAuthenticated)
            {
                rateNew.UserId = User.Identity.GetUserId();
                var checkRate = _rateService.GetRateByUserId(rateNew.UserId, rateNew.DoctorID);
                if (checkRate != null)
                {
                    checkRate.CountRate = rateNew.CountRate;
                    checkRate.UpdatedBy = User.Identity.GetUserName();
                    checkRate.UpdatedDate = DateTime.Now;
                    _rateService.Update(checkRate);
                }
                else
                {
                    rateNew.CreatedBy = User.Identity.GetUserName();
                    rateNew.CreatedDate = DateTime.Now;
                    _rateService.Add(rateNew);
                }
                _rateService.Save();
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

        private RateData GetRateData(int doctorId)
        {
            var listRate = _rateService.GetAllByDoctorId(doctorId);
            var listRateViewModel = Mapper.Map<IEnumerable<Rate>, IEnumerable<RateViewModel>>(listRate);
            var arrCountRate = new int[5];
            var arrPercent = new float[5];
            float averageRate = 0;
            foreach (var rate in listRateViewModel)
            {
                averageRate += rate.CountRate;
                for (int i = 0; i < 5; i++)
                {
                    if (rate.CountRate == i + 1)
                    {
                        arrCountRate[i] += 1;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                arrPercent[i] = (float)arrCountRate[i] / listRateViewModel.Count();
                arrPercent[i] *= 100;
            }

            float averageRateModel = (float)averageRate / (listRateViewModel.Count());
            string averageString = averageRateModel.ToString("0.0");
            averageRateModel = float.Parse(averageString, CultureInfo.InvariantCulture.NumberFormat);
            if (Double.IsNaN(averageRateModel))
            {
                averageRateModel = 0;
            }
            var rateData = new RateData()
            {
                AverageRate = averageRateModel,
                ArrayPercent = arrPercent
            };

            return rateData;
        }
    }
}