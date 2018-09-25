using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class InAndOutBoundAirTravelViewModel
    {
        public int InAndOutBoundAirTravelId { get; set; }
        public string FlightNumber { get; set; }
        public DestinationViewModel FromAndToDestinationz { get; set; }
        public decimal FlightCost { get; set; }
        public LaguageViewModel CustomerLaguage { get; set; }
        public bool HasMealsIncluded { get; set; }
        public int LaguageId { get; set; }
    }
}
