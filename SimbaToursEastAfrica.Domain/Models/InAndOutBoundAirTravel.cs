using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class InAndOutBoundAirTravel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InAndOutBoundAirTravelId { get; set; }
        public string FlightNumber { get; set; }
        [ForeignKey("DestinationId")]
        public Destination FromAndToDestination { get; set; }
        public int DestinationId { get; set; }
        public decimal FlightCost { get; set; }
        [ForeignKey("LaguageId")]
        public Laguage CustomerLaguage { get; set; }
        public int LaguageId { get; set; }
        public bool HasMealsIncluded { get; set; }
    }
}
