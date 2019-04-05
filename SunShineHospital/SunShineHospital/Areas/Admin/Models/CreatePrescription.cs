using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Areas.Admin.Models
{
    public class CreatePrescription
    {
        [Required]
        public int AppointmentId { set; get; }
        [Required]
        [MaxLength(200)]
        public string Diagnose { set; get; }
    }
}