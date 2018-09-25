using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class TourClient
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TourClientId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string Nationality { get; set; }
        public bool HasRequiredVisaStatus { get; set; }
        public int NumberOfIndividuals { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }
        public virtual List<HotelBooking> HotelBookings { get; set; }
        public decimal GrossTotalCosts { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }

        public string EmailAddress { get; set; }
        public bool HasFullyPaid { get; set; } = false;
        public decimal PaidInstallments { get; set; }
        public decimal CurrentPayment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
