using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class DoctorViewModel
    {
        public int ID { set; get; }

        public string Alias { set; get; }

        public int DepartmentID { set; get; }


        public string Image { set; get; }

        public decimal? Salary { set; get; }

        public string Education { set; get; }
        public string Description { set; get; }
        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        public virtual DepartmentViewModel Department { set; get; }

        public virtual ApplicationUserViewModel User { set; get; }

        public virtual IEnumerable<TestViewModel> Tests { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        [Required]
        public bool Status { set; get; }
    }
}