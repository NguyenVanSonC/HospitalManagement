using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class PrescriptionViewModel
    {
        public int ID { set; get; }

        public int DoctorID { set; get; }


        public int PatientID { set; get; }


        public string PaymentMethod { set; get; }

        public string Diagnose { set; get; }

        public string PaymentStatus { set; get; }


        public virtual DoctorViewModel Doctor { set; get; }


        public virtual PatientViewModel Patient { set; get; }

        public virtual IEnumerable<PrescriptionDetailsViewModel> PrescriptionDetails { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }
    }
}