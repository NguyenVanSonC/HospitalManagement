using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class AppoinmentViewModel
    {
        public int ID { set; get; }

        public int DoctorID { set; get; }

        public int PatientID { set; get; }

        public virtual DoctorViewModel Doctor { set; get; }

        public virtual PatientViewModel Patient { set; get; }

        [Required(ErrorMessage = "Điền tên bệnh nhân cần khám")]
        [MaxLength(256)]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Điền địa chỉ bệnh nhân")]
        [MaxLength(256)]
        public string Address { set; get; }

        [Required]
        public DateTime? BirthDay { set; get; }

        public bool? Gender { set; get; }

        public bool? Healthinsurance { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { set; get; }

        public string Email { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public bool Status { set; get; }
    }
}