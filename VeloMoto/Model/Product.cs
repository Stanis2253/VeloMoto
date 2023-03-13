using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required] 
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ShortDesc { get; set; }
        [Required]
        public bool IsActual { get; set; }

        public int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }
        public List<Category> Category { get; set; } = new();
    }
}
