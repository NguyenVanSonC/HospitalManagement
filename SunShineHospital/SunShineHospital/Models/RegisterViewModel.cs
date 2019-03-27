using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunShineHospital.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên.")]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập tên đăng nhập.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng.")]
        public string Email { set; get; }

        public string Address { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập giới tính.")]
        public bool Gender { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập ngày sinh.")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { set; get; }
    }
}