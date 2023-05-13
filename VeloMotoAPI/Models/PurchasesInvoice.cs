using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMotoAPI.Models
{
    public class PurchasesInvoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Providers Provider { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual StatusOrders StatusOrder { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
