using System.Collections.Generic;
using DBModels;
using eBarWS.Models;

namespace eBarWS.Interfaces
{
    public interface IRestaurantOperations
    {
        List<RestaurantLocations> GetRestaurantLocation(int restaurantId);
        List<RestaurantTypes> GetAllRestaurantTypes();
        List<Restaurants> GetAllRestaurants();
        List<Restaurants> GetResturantsByLocation(string latitude, string longitude, int rangeKm, string location);
        Restaurants GetRestaurantById(int restaurantId);
        List<RestaurantAdministrators> GetRestaurantAdministrators(int restaurantId);
        List<RestaurantTypes> GetRestaurantsByType(int typeId);
        List<RestaurantTables> GetRestaurantTables(int restaurantId);
        List<Restaurants> GetRestaurantsByLocation(string location);
        string AddRestaurant(Restaurants restaurant);
        string DeleteRestaurant(int restaurantId);
        List<RestaurantModel> GetRestaurantsForPr();
    }
}