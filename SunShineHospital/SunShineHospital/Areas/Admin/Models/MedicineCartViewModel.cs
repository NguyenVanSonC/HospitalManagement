using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SunShineHospital.Models;

namespace SunShineHospital.Areas.Admin.Models
{
    [Serializable]
    public class MedicineCartViewModel
    {
        public int MedicineId { set; get; }

        public int AppointmentId { set; get; }
        [Required]
        public MedicineViewModel Medicine{ set; get; }

        [Required]
        [Range(0, 100)]
        public int Quantity { set; get; }

        [Required]
        [Range(0, 50)]
        public int TotalDay { set; get; }

        [Required]
        [MaxLength(100)]
        public string Calendar { set; get; }

        public bool BeforeMeal { set; get; }
    }
}