using System;
using System.Collections.Generic;
using ViewModels;

namespace eBarDatabase
{
    public interface IRestaurantEventOperations
    {
        List<RestaurantEvent> GetRestaurantEvents(int userId);

        List<RestaurantEventModel> GetRestaurantEventsForTimeline(DateTime lastEventDate);
        List<RestaurantEvent> GetMostRecent10Restaurants();
        string SaveRestaurantEvent(RestaurantEvent restaurantEvent);
        string UpdateRestaurantEvent(RestaurantEvent restaurantEvent);
    }
}
