using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VeloMotoAPI.Models
{
    public class Prices
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }
        [Required]
        public decimal Value { get; set; }

        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime = DateTime.Now; }

            set { dateTime = value; }
        }
    }
}
