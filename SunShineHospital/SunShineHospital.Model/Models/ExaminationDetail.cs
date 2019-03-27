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
    [Table("ExaminationDetails")]
    public class ExaminationDetail : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int ExaminationID { set; get; }

        public int DoctorID { set; get; }

        public int TestID { set; get; }

        public int Quantitty { set; get; }

        [ForeignKey("ExaminationID")]
        public virtual Examination Examination { set; get; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { set; get; }

        [ForeignKey("TestID")]
        public virtual Test Test { set; get; }
    }
    
}
