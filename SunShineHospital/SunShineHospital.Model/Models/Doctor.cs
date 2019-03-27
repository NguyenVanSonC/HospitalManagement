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
    [Table("Doctors")]
    public class Doctor : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        public int DepartmentID { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        public decimal? Salary { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }
        public string Content { set; get; }
        public string Education { set; get; }
        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }

        [ForeignKey("DepartmentID")]
        public virtual Department Department { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { set; get; }

        public virtual IEnumerable<Test> Tests { set; get; }
    }
}
