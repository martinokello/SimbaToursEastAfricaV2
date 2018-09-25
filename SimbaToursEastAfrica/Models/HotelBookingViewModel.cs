using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class HotelBookingViewModel
    {
        [Key]
        public int HotelBookingId { get; set; }
        public string HotelName { get; set; }
        public LocationViewModel Location { get; set; }
        public int LocationId { get; set; }
        public TourClientViewModel TourClient { get; set; }
        public decimal AccomodationCost { get; set; }

        public bool HasMealsIncluded { get; set; }
    }
}
