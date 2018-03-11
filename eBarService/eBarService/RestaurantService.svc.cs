using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using eBarService.Interfaces;
using eBarService.Messages;
using eBarService.Models;
using eBarService.ServiceInterfaces;
using Newtonsoft.Json;

namespace eBarService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantOperations _restaurantOperations;

        public RestaurantService(IRestaurantOperations restaurantOperations)
        {
            _restaurantOperations = restaurantOperations;
        }
        public string GetResturantsByLocation(string latitude, string longitude, string range, string location)
        {
            var restaurants = _restaurantOperations.GetResturantsByLocation(latitude, longitude, Convert.ToInt32(range), location);
            return JsonConvert.SerializeObject(restaurants);
        }

        public string GetRestaurantById(int restaurantId)
        {
            var restaurant = _restaurantOperations.GetRestaurantById(restaurantId);
            return JsonConvert.SerializeObject(restaurant);
        }

        public string GetRestaurantsByCity(string city)
        {
            var restaurant = _restaurantOperations.GetRestaurantsByLocation(city);
            return JsonConvert.SerializeObject(restaurant);
        }

        public string GetRestaurantsByType(int typeId)
        {
            var restaurants = _restaurantOperations.GetRestaurantsByType(typeId);
            return JsonConvert.SerializeObject(restaurants);
        }

        public string GetAllRestaurants()
        {
            try
            {
                var restaurants = _restaurantOperations.GetAllRestaurants();
                return JsonConvert.SerializeObject(restaurants);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(null);
            }
        }

        public string GetRestaurantTypes()
        {
            var types = _restaurantOperations.GetAllRestaurantTypes();
            return JsonConvert.SerializeObject(types);
        }

        public string GetRestaurantLocation(int restaurantId)
        {
            var location = _restaurantOperations.GetRestaurantLocation(restaurantId);
            return JsonConvert.SerializeObject(location);
        }

        public string GetRestaurantTables(int restaurantId)
        {
            var tables = _restaurantOperations.GetRestaurantTables(restaurantId);
            return JsonConvert.SerializeObject(tables);
        }

        public string SaveRestaurant(Restaurants restaurant)
        {
            ResponseDataModel response = new ResponseDataModel();
            try
            {
                response.ResultMessage = _restaurantOperations.AddRestaurant(restaurant);
                response.ResultFlag = true;
                response.ResultCode = ResultCode.RestaurantSaved.ToString();
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed.ToString();
            }

            return JsonConvert.SerializeObject(response);
        }
    }
}
