using System.ComponentModel.DataAnnotations;
using VeloMotoApi.Model;

namespace VeloMotoAPI.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Product> Product { get; set; } = new();
    }
}
