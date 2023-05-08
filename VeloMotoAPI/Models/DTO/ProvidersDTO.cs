using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models.DTO
{
    public class ProvidersDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public string NumberPhone { get; set; }

        public string? Description { get; set; }
    }
}
