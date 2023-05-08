using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int SalesInvoiceId { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice SalesInvoice { get; set; }

        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual StatusOrder StatusOrder { get; set; }
    }
}
