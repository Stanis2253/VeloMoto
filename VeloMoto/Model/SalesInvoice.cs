using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SalesInvoice
    {
        [Key]
        public int Id { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public virtual StatusOrder StatusOrder { get; set; }
        public DateTime Date { get; set; }
    }
}
