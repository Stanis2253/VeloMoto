using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeloMotoApi.Model;

namespace VeloMotoAPI.Model
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Required]
        public int PruchaseInvoiceId { get; set; }

        [ForeignKey("PruchaseInvoiceId")]
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
