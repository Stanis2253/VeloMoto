using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int SalesInvoiceId { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice SalesInvoice { get; set; }
        public int Amount { get; set; }
    }
}
