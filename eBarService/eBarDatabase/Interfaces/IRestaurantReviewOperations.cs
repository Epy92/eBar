using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBarDatabase
{
    public interface IRestaurantReviewOperations
    {
        List<RestaurantReview> GetRestaurantReviews(int restaurantId);
        string CreateRestaurantReview(RestaurantReview restaurantReview);
        string UpdateRestaurantReview(RestaurantReview restaurantReview);
    }
}
