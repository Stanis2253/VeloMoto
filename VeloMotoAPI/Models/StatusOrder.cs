using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class StatusOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; }
    }
}
