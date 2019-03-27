using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class ExaminationViewModel
    {
        public int ID { set; get; }

        public string PaymentMethod { set; get; }


        public string PaymentStatus { set; get; }

        public int PatientID { set; get; }

        public virtual PatientViewModel Patient { set; get; }

        public IEnumerable<ExaminationDetailViewModel> ExaminationDetails { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }
    }
}