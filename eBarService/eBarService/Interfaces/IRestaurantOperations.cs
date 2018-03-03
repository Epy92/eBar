using System.Collections.Generic;

namespace eBarService.Interfaces
{
    public interface IRestaurantOperations
    {
        List<RestaurantLocations> GetResturantLocation(int restaurantId);
        List<RestaurantTypes> GetAllRestaurantTypes();
        List<Restaurants> GetAllRestaurantes();
        List<Restaurants> GetResturantsByLocation(string latitude, string longitude, int rangeKm, string location);
        Restaurants GetRestaurantById(int restaurantId);
        List<RestaurantAdministrators> GetRestaurantAdministrators(int restaurantId);
        List<RestaurantTypes> GetRestaurantTypesByType(int typeId);
        List<RestaurantTables> GetRestaurantTables(int restaurantId);
        List<Restaurants> GetRestaurantsByLocation(string location);
        string AddRestaurant(Restaurants restaurant);
        string DeleteRestaurant(int restaurantId);
    }
}