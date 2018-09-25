using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Destination
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DestinationId { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationTo { get; set; }

        public DateTime DepartTime { get; set; }
        public DateTime ArriveTime { get; set; }
    }
}
