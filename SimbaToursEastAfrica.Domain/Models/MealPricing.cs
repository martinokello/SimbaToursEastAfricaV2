using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class MealPricing
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MealPricingId { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public decimal Price { get; set; }
    }
}
