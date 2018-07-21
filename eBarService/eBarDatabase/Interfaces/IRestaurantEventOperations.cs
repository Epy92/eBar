using System.Collections.Generic;

namespace eBarDatabase
{
    public interface IRestaurantEventOperations
    {
        List<RestaurantEvent> GetRestaurantEvents(int userId);
        string SaveRestaurantEvent(RestaurantEvent restaurantEvent);
        string UpdateRestaurantEvent(RestaurantEvent restaurantEvent);
    }
}
