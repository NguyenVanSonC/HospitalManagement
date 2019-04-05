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
    [Table("Rates")]
    public class Rate : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public int DoctorID { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserId { set; get; }

        [Required]
        [Range(0,5)]
        public int CountRate { set; get; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { set; get; }
    }
}
