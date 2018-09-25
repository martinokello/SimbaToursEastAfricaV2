using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class DriverViewModel
    {
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VehicleViewModel Vehicle { get; set; }
        public List<ScheduleViewModel> Schedules { get; set; } = new List<ScheduleViewModel>();
    }
}
