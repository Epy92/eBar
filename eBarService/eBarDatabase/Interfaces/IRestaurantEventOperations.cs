using System.Collections.Generic;
using ViewModels;

namespace eBarDatabase.Interfaces
{
    public interface IRestaurantEventOperations
    {
        List<RestaurantEvent> GetRestaurantEvents(int restaurantId);
        string SaveRestaurantEvent(RestaurantEvent restaurantEvent);
        string UpdateRestaurantEvent(RestaurantEvent restaurantEvent);
    }
}
