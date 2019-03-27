using SunShineHospital.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunShineHospital.Model.Models
{
    [Table("Prescriptions")]
    public class Prescription : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int DoctorID { set; get; }


        public int PatientID { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }


        public string PaymentStatus { set; get; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { set; get; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { set; get; }

        public virtual IEnumerable<PrescriptionDetail> PrescriptionDetails { set; get; }
    }
}
