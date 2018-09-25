using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Laguage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LaguageId { get; set; }

        [ForeignKey("TourClientId")]
        public virtual TourClient TourClient { get; set; }
        public int TourClientId { get; set; }
        [ForeignKey("LaguagePricingId")]
        public virtual LaguagePricing LaguagePricing { get; set; }
        public int LaguagePricingId { get; set; }
    }
}
