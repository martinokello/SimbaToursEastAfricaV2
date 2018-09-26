using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class TransportPricing
    {
        [Key]
        public int TransportPricingId { get; set; }
        public string TransportPricingName { get; set; }
        public string Description { get; set; }
        public decimal FourByFourPricing { get; set; }
        public decimal MiniBusPricing { get; set; }
        public decimal TaxiPricing { get; set; }
        public decimal PickupTruckPricing { get; set; }
        public decimal TourBusPricing { get; set; }
    }
}
