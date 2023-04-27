using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeloMotoAPI.Model
{
    public class PurchaseInvoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public virtual Provider Provider { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
