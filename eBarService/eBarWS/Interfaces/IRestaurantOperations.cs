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
        //List<RestaurantAdministrators> GetRestaurantAdministrators(int restaurantId);
        List<RestaurantTypes> GetRestaurantsByType(int typeId);
        List<RestaurantTables> GetRestaurantTables(int restaurantId);
        List<Restaurants> GetRestaurantsByLocation(string location);
        string AddRestaurant(Restaurants restaurant);
        //string DeleteRestaurant(int restaurantId);
        List<RestaurantModel> GetRestaurantsForPr();
        List<RestaurantModel> GetRestaurantsObjListByKeyword(string keyword);
        //List<RestaurantModel> GetRestaurantsObjListByLocationAndName(string location,string keyword);
        List<RestaurantModel> GetRestaurantsObjListByLocation(string county, string location);
        List<RestaurantModel> GetRestaurantsObjListByType(string typeIDs); 
        //List<RestaurantModel> GetRestaurantsObjListByLocationAndType(string location,int typeid);
        //List<RestaurantModel> GetRestaurantsObjListByNameAndType(string keyword,int typeid);
        List<RestaurantModel> GetRestaurantsObjListByGeoCoordinate(string lat, string longitude, int rangeKm, List<RestaurantModel> l_rest);
        List<RestaurantModel> GetRestaurantsObjListByCounty(string county);
    }
}