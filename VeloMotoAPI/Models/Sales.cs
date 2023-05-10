
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }
        [Required]
        public int SalesInvoiceId { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice SalesInvoice { get; set; }
        [Required]
        public int Amount { get; set; }

        public decimal Price { get; set; }
    }
}
