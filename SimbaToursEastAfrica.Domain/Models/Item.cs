using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Item
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [ForeignKey("ItemType")]
        public virtual ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public decimal ItemCost { get; set; }
        [ForeignKey("LaguageId")]
        public virtual Laguage Laguage { get; set; }
        public Nullable<int> LaguageId { get; set; }
        [ForeignKey("MealId")]
        public virtual Meal Meal { get; set; }
        public Nullable<int> MealId { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [ForeignKey("laguagePricingId")]
        public virtual LaguagePricing laguagePricing { get; set; }
        public Nullable<int> laguagePricingId { get; set; }
        [ForeignKey("mealPricingId")]
        public virtual MealPricing mealPricing { get; set; }
        public Nullable<int> mealPricingId { get; set; }
    }

    public enum ItemType { Laguage=1, Meal = 2, TravelDocuments=3, MedicalTreatment=4}

}
