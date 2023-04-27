using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeloMotoApi.Model;

namespace VeloMotoAPI.Model
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
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
