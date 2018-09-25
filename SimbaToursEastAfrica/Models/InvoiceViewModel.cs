using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public List<ItemViewModel> InvoicedItems{ get; set; } = new List<ItemViewModel>();
        public decimal NetCost { get; set; }
        public decimal PercentTaxAppliable { get; set; }
        public decimal GrossCost { get; set; }
        public int TourClientId { get; set; }
    }
}
