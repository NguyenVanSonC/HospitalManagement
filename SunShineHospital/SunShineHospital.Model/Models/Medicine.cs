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
    [Table("Medicines")]
    public class Medicine : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        [MaxLength(256)]
        public string Company { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public decimal Price { set; get; }
        public int? Warranty { set; get; }
        public int Quantity { set; get; }
    }
}
