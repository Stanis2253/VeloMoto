using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeloMotoAPI.Model;

namespace VeloMotoApi.Model
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
        [Required]

        public int ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }
        public List<Category> Category { get; set; } = new();
    }
}
