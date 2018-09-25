using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }
        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
        public int DriverId { get; set; }
        [ForeignKey("ItinaryId")]
        public virtual Itinary Itinary { get; set; }
        public int ItinaryId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }
        public string Operation { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        [ForeignKey("SchedulesId")]
        public virtual SchedulesPricing SchedulesPricing { get;set; }
        public int SchedulesPricingId { get; set; }
    }
}
