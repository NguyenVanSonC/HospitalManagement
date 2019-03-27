using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class PrescriptionDetailsViewModel
    {
        public int ID { set; get; }

        public int ExaminationID { set; get; }

        public int DoctorID { set; get; }

        public int TestID { set; get; }

        public int Quantitty { set; get; }


        public virtual ExaminationDetailViewModel Examination { set; get; }

        public virtual DoctorViewModel Doctor { set; get; }

        public virtual TestViewModel Test { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }
    }
}