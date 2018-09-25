using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public decimal ItemCost { get; set; }
        public int ItemTypeId { get; set; }
        public LaguageViewModel Laguage { get; set; }
        public int LaguageId { get; set; }
        public int InvoiceId { get; set; }
    }

    public enum ItemType { Laguage=1, Meal = 2, TravelDocuments=3, MedicalTreatment=4}
    
}
