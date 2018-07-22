using System;

namespace ViewModels
{
    public class RestaurantReviewModel
    {
        public int RestaurantReviewID { get; set; }
        public int UserID { get; set; }
        public int RestaurantId { get; set; }
        public string ReviewComment { get; set; }
    }
}
