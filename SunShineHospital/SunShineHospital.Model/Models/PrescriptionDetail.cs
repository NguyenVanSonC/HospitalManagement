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
    [Table("PrescriptionDetails")]
    public class PrescriptionDetail : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int PrescriptionID { set; get; }

        public int MedicineID { set; get; }

        public int Quantitty { set; get; }

        [ForeignKey("PrescriptionID")]
        public virtual Prescription Prescription { set; get; }

        [ForeignKey("MedicineID")]
        public virtual Medicine Medicine { set; get; }
    }
}
