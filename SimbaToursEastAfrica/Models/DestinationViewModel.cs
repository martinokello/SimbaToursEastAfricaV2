using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class DestinationViewModel
    {
        public int DestinationId { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationTo { get; set; }

        public DateTime DepartTime { get; set; }
        public DateTime ArriveTime { get; set; }
    }
}
