using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class MedicineViewModel
    {
        public int ID { set; get; }

        public string Alias { set; get; }

        public string Company { set; get; }

        public string Name { set; get; }

        public string Image { set; get; }

        public string Description { set; get; }

        public decimal Price { set; get; }
        public int? Warranty { set; get; }
        public int Quantity { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }
    }
}