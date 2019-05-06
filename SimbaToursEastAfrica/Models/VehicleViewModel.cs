using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class VehicleViewModel
    {
        [Key]
        public int VehicleId { get; set; }
        public string VehicleRegistration { get; set; }
        public int MaxNumberOfPassengers { get; set; }
        public int ActualNumberOfPassengersAllocated { get; set; }
        public VehicleType VehicleType { get; set; }
        public TourClientViewModel TourClient { get; set; }
        public int TourClientId { get; set; }
    }

    public enum VehicleType{ Taxi=1, MiniBus=2, TourBus=3, FourWheelDriveCar=4, PickUpTrack=5}
}
