using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class DepartmentViewModel
    {
        public int ID { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Alias { set; get; }

        public string Description { set; get; }
        public int? ParentID { set; get; }
        public int? DisplayOrder { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<DoctorViewModel> Doctors { set; get; }
        public virtual IEnumerable<TestViewModel> Tests { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}