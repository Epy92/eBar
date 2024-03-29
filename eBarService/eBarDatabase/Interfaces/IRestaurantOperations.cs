﻿using System.Collections.Generic;
using ViewModels;

namespace eBarDatabase
{
    public interface IRestaurantOperations
    {
        List<RestaurantLocations> GetRestaurantLocation(int restaurantId);
        List<RestaurantTypes> GetAllRestaurantTypes();
        List<Restaurants> GetAllRestaurants();
        List<Restaurants> GetResturantsByLocation(string latitude, string longitude, int rangeKm, string location);
        Restaurants GetRestaurantById(int restaurantId);
        List<RestaurantTypes> GetRestaurantsByType(int typeId);
        List<RestaurantTables> GetRestaurantTables(int restaurantId);
        //List<Restaurants> GetRestaurantsByLocation(string location);
        List<RestaurantModel> GetRestaurantsForPr();
        List<RestaurantModel> GetRestaurantsByKeyword(string keyword);
        List<RestaurantModel> GetRestaurantsByCountyAndCity(string county, string location);
        List<RestaurantModel> GetRestaurantsByTypes(string typeIDs); 
        List<RestaurantModel> GetRestaurantsByGeoCoordinate(string lat, string longitude, int rangeKm, List<RestaurantModel> l_rest);
        string SaveRestaurantDetails(RestaurantDetails restaurantDetails);
        string AddRestaurant(ref Restaurants restaurant);
        string SaveRestaurantAdministrator(RestaurantAdministrators restaurantAdmin);
        string SaveRestaurantLocation(RestaurantLocations restaurantLocation);
        string SaveRestaurantType(RestaurantTypes restaurantTypes);
        List<CountiesAndCities> GetCitiesByCounty(string county);
    }
}