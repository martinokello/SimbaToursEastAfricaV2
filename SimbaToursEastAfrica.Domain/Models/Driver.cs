using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
