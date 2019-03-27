using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunShineHospital.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }
        public string FullName { set; get; }
        public DateTime BirthDay { set; get; }
        public string Bio { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public string Address { set; get; }
        public string PhoneNumber { set; get; }
        public bool? Gender { set; get; }
        public string Alias { set; get; }

        public string Image { set; get; }


        public string Description { set; get; }
        public string Content { set; get; }

        public string UserId { set; get; }
    }
}