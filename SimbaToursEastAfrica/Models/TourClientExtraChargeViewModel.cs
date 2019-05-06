using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class TourClientExtraChargeViewModel
    {
        public int TourClientExtraChargesId { get; set; }
        
        public int TourClientId { get; set; }

        public TourClientViewModel TourClient { get; set; }

        public decimal ExtraCharges { get; set; }

        public string Description { get; set; }
    }
}
