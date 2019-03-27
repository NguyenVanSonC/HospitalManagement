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
    [Table("Patients")]
    public class Patient : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }
        public string Content { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { set; get; }
    }
}
