using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Domain.Models
{
    public class Location
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }
        public string Country { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public int AddressId { get; set; }
        public string LocationName { get; set; }
    }

}
