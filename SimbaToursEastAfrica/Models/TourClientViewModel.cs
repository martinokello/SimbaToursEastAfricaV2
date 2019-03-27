using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class TourClientViewModel
    {
        public int TourClientId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string Nationality { get; set; }
        public bool HasRequiredVisaStatus { get; set; }
        public int NumberOfIndividuals { get; set; }
        public LaguageViewModel CombinedLaguage { get; set; }
        public MealViewModel CombinedMeals { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; }
        public decimal CostPerIndividual { get; set; }
        public decimal GrossTotalCosts { get; set; }
        public int LaguageId { get; set; }
        public int MealId { get; set; }
        public HotelViewModel Hotel { get; set; }
        public string EmailAddress { get; set; }
        public bool HasFullyPaid { get; set; } = false;
        public decimal PaidInstallments { get; set; }
        public decimal CurrentPayment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        
    }
}
