using System.Collections.Generic;
using ViewModels;

namespace eBarDatabase.Interfaces
{
    public interface IRestaurantEventOperations
    {
        List<RestaurantEvent> GetRestaurantEvents(int userId);
        string SaveRestaurantEvent(RestaurantEvent restaurantEvent);
        string UpdateRestaurantEvent(RestaurantEvent restaurantEvent);
    }
}
