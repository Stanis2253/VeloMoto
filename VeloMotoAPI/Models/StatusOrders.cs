using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class StatusOrders
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; }
    }
}
