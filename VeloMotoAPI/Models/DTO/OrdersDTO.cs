using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models.DTO
{
    public class OrdersDTO
    {
        public int Id { get; set; }
        public int SalesInvoiceId { get; set; }
        public string UserId { get; set; }
    }
}
