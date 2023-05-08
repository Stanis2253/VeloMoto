using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models.DTO
{
    public class PricesDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public decimal Value { get; set; }

        public DateTime DateTime { get; set; }
    }
}
