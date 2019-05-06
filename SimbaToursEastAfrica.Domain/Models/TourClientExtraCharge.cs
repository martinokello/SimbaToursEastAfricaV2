using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class TourClientExtraCharge
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TourClientExtraChargesId { get; set; }

        [ForeignKey("TourClient")]
        public int TourClientId { get; set; }

        public TourClient TourClient { get; set; }

        public decimal ExtraCharges { get; set; }
        public string Description { get; set; }
    }
}
