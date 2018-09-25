using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class LaguagePricing
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LaguagePricingId { get; set; }
        public string LaguagePricingName { get; set; }
        public string LaguageDescription { get; set; }
        public decimal UnitLaguagePrice { get; set; }
        public decimal UnitMedicalPrice { get; set; }
        public decimal UnitTravelDocumentPrice { get; set; }
        public decimal unitMealPrice { get; set; }
    }
}
