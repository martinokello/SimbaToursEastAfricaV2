using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class ItinaryViewModel
    {
        public int ItinaryId { get; set; }
        public List<ScheduleViewModel> Schedules { get; set; }
    }
}
