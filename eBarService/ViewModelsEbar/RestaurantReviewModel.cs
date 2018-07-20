using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RestaurantReviewModel
    {
        public int RestaurantReviewID { get; set; }
        public int UserID { get; set; }
        public int RestaurantId { get; set; }
        public string Review { get; set; }
        public int Grade { get; set; }
    }
}
