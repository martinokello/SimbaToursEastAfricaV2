
using SimbaToursEastAfrica.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class LaguageViewModel
    {
        public int LaguageId { get; set; }
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
        public TourClientViewModel TourClient { get; set; }
        public int TourClientId { get; set; }

        public LaguagePricing LaguagePricing { get; set; }
        public int LaguagePricingId{get;set;}

    }
}
