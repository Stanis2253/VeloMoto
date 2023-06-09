using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models.DTO
{
    public class ProductsDTO
    {

        public int IdProduct { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDesc { get; set; }

        public bool IsActual { get; set; }

        public int ManufacturerId { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

    }
}
