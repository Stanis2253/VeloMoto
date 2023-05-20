using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class SalesInvoice
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public DateTime Date { get; set; }
        [Required]
        public int StatusId { get; set; }


        [ForeignKey("StatusId")]
        public virtual StatusOrders StatusOrder { get; set; }
    }
}
