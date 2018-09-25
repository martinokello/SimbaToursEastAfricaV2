using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class ScheduleViewModel
    {
        public int ScheduleId { get; set; }
        public DriverViewModel Driver { get; set; }
        public ItinaryViewModel Itinary { get; set; }
        public int TourClientId { get; set; }
        public TourClientViewModel TourClient { get; set; }
        public LocationViewModel Location { get; set; }
        public string Operation { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public int ItinaryId { get; set; }
        public int DriverId { get; set; }
    }
}
