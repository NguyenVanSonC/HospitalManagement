using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class CommentViewModel
    {
        public int ID { set; get; }

        public int DoctorID { set; get; }

        [StringLength(128)]
        public string UserId { set; get; }

        [MaxLength(500)]
        [Required]
        public string Content { set; get; }

        public int? ParentID { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }


        public virtual DoctorViewModel Doctor { set; get; }

        public virtual ApplicationUserViewModel User { set; get; }

        public virtual IEnumerable<CommentViewModel> Comments { set; get; }
    }
}