using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class HotelBooking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HotelBookingId { get; set; }
        public string HotelName { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("TourClientId")]
        public virtual TourClient TourClient { get; set; }
        public int TourClientId { get; set; }
        public decimal AccomodationCost { get; set; }
        public bool HasMealsIncluded { get; set; }
        [ForeignKey("HotelPricingId")]
        public virtual HotelPricing HotelPricing { get; set; }
        public int HotelPricingId { get; set; }
    }
}
