using System.Collections.Generic;

namespace eBarService.Interfaces
{
    public interface IRestaurantOperations
    {
        List<RestaurantLocations> GetResturantLocation(int restaurantId);
        List<RestaurantTypes> GetAllRestaurantTypes();
        List<Restaurants> GetAllRestaurantes();
        List<Restaurants> GetResturantesByLocation(string latitude, string longitude);
        Restaurants GetRestaurantById(int restaurantId);
        List<RestaurantAdministrators> GetRestaurantAdministrators(int restaurantId);
        List<RestaurantTypes> GetRestaurantTypesByType(int typeId);
        List<RestaurantTables> GetRestaurantTables(int restaurantId);
    }
}