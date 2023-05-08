using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class Manufacturers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]

        public string NumberPhone { get; set; }

        public string? Email { get; set; }

        public string? Description { get; set; }
    }
}
