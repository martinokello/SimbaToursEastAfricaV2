using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Meal
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MealId { get;set; }
        [ForeignKey("TourClientId")]
        public virtual TourClient TourClient { get; set; }
        public int TourClientId { get; set; }
        [ForeignKey("MealPricingId")]
        public virtual MealPricing MealPricing { get; set; }
        public int MealPricingId { get; set; }
    }
}
