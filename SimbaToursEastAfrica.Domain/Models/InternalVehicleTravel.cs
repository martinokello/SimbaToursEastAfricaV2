using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class InternalVehicleTravel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InternalVehicleTravelId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle VehicleAllocated { get; set; }
        public int VehicleId { get; set; }
        public decimal VehicleCosts { get; set; }
    }
}
