using SimbaToursEastAfrica.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class HotelViewModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public bool HasMealsIncluded { get; set; }
        public HotelPricing HotelPricing { get; set; }
        public int HotelPricingId { get; set; }
    }
}
