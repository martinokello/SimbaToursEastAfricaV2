using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public virtual List<Item> InvoicedItems{ get; set; } = new List<Item>();
        public decimal NetCost { get; set; }
        public decimal PercentTaxAppliable { get; set; }
        public decimal GrossCost { get; set; }
        [ForeignKey("TourClientId")]
        public virtual TourClient TourClient { get; set; }
        public int TourClientId { get; set; }
    }
}
