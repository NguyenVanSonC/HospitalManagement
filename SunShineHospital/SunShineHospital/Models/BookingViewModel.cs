using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    [Serializable]
    public class BookingViewModel
    {
        public int DoctorId { set; get; }
        public DoctorViewModel Doctor { set; get; }
        public string Time { set; get; }
        public string Day { set; get; }
    }
}