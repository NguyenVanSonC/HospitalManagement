using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunShineHospital.Model.Abstract;

namespace SunShineHospital.Model.Models
{
    [Table("Examinations")]
    public class Examination : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        
        public string PaymentStatus { set; get; }

        public int PatientID { set; get; }
        [ForeignKey("PatientID")]
        public virtual Patient Patient { set; get; }

        public IEnumerable<ExaminationDetail> ExaminationDetails { set; get; }
    }
    
}
