using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class PrescriptionDetailsViewModel
    {
        public int ID { set; get; }

        public int Quantitty { set; get; }

        public int TotalDay { set; get; }

        public string Calendar { set; get; }

        public bool BeforeMeal { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }
    }
}