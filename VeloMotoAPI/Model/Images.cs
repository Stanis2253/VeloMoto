using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeloMotoApi.Model;

namespace VeloMotoAPI.Model
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Required]

        public string IndexImg { get; set; }
    }
}
