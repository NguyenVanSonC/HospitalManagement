using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class RateViewModel
    {
        public int ID { set; get; }

        public int DoctorID { set; get; }

        [StringLength(128)]
        public string UserId { set; get; }

        [Required]
        [Range(0, 5, ErrorMessage = "Đánh giá trong khoảng 5 sao")]
        public int CountRate { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }

        public virtual DoctorViewModel Doctor { set; get; }

        public virtual ApplicationUserViewModel User { set; get; }
    }

    public class RateData
    {
        public float AverageRate { set; get; }
        public float[] ArrayPercent { set; get; }
    }
}