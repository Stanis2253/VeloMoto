using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int PruchaseInvoiceId { get; set; }

        [ForeignKey("PruchaseInvoiceId")]
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
