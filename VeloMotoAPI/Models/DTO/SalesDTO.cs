using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models.DTO
{
    public class SalesDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SalesInvoiceId { get; set; }
        public int Amount { get; set; }

        
    }
}
