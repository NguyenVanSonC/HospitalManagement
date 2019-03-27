using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class PatientViewModel
    {
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }


        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }
        public string Content { set; get; }

        [StringLength(128)]

        public string UserId { set; get; }

        public virtual ApplicationUserViewModel User { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}