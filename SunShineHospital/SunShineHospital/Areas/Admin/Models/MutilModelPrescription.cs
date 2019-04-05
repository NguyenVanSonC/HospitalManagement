using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunShineHospital.Models;

namespace SunShineHospital.Areas.Admin.Models
{
    public class MutilModelPrescription
    {
        public CreatePrescription CreatePrescription { set; get; }
        public AppoinmentViewModel AppoinmentViewModel { set; get; }
    }
}