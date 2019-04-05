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

        [Required]
        [MaxLength(200)]
        public string Diagnose { set; get; }

        public int AppointmentID { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }


        public string PaymentStatus { set; get; }

        [ForeignKey("AppointmentID")]
        public virtual Appoinment Appoinment { set; get; }

        public virtual IEnumerable<PrescriptionDetail> PrescriptionDetails { set; get; }
    }
}
