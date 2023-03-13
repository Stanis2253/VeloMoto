using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Price
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public decimal Value { get; set; }

        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime = DateTime.Now; }

            set { dateTime = value; }
        }
    }
}
