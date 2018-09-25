using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Models
{
    public class MealViewModel
    {
        public int MealId { get;set; }
        public List<ItemViewModel> MealItems { get; set; } = new List<ItemViewModel>();
        public TourClientViewModel TourClient { get; set; }
        public int TourClientId { get; set; }
    }
}
