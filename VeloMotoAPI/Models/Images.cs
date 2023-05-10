using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }

        public string IndexImg { get; set; }
        public string Extension { get; set; }
    }
}
