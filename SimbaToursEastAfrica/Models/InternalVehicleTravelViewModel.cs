using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class InternalVehicleTravelViewModel
    {
        public int InternalVehicleTravelId { get; set; }
        public VehicleViewModel VehicleAllocated { get; set; }
        public decimal VehicleCosts { get; set; }
        public int VehicleId { get; set; }
    }
}
