﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Model.Abstract;

namespace SunShineHospital.Model.Models
{
    [Table("Appoinments")]
    public class Appoinment : Auditable
    {
        [Key]
        [Column(Order = 1)]
        public int DoctorID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int PatientID { set; get; }

        [MaxLength(256)]
        public string FullName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

        public bool? Gender { set; get; }

        public bool? Healthinsurance { set; get; }

        public string PhoneNumber { set; get; }

        public string Email { set; get; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { set; get; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { set; get; }

    }
}
