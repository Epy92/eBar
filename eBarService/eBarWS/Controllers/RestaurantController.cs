using System;
using System.Web.Http;
using DBModels;
using eBarWS.Interfaces;
using eBarWS.Models;
using eBarWS.Utils;
using Newtonsoft.Json;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace eBarWS.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IRestaurantOperations _restaurantOperations;
        public RestaurantController(ILogger logger, IRestaurantOperations restaurantOperations)
        {
            _logger = logger;
            _restaurantOperations = restaurantOperations;
        }

        [EBarAuth]
        public string GetResturantsByLocation(string latitude, string longitude, string range, string location)
        {
            try
            {
                var restaurants = _restaurantOperations.GetResturantsByLocation(latitude, longitude, Convert.ToInt32(range), location);
                return JsonConvert.SerializeObject(restaurants);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantsByLocation_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetRestaurantById(int restaurantId)
        {
            try
            {
                var restaurant = _restaurantOperations.GetRestaurantById(restaurantId);
                var rest = Mapper.Map<Restaurants, RestaurantModel>(restaurant);
                return JsonConvert.SerializeObject(rest);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantsById_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetRestaurantsByCity(string city)
        {
            try
            {
                var restaurant = _restaurantOperations.GetRestaurantsByLocation(city);
                return JsonConvert.SerializeObject(restaurant);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantsByCity_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetRestaurantsByType(int typeId)
        {
            try
            {
                var restaurants = _restaurantOperations.GetRestaurantsByType(typeId);
                return JsonConvert.SerializeObject(restaurants);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantsByType_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetAllRestaurants()
        {
            try
            {
                var restaurants = _restaurantOperations.GetAllRestaurants();
                return JsonConvert.SerializeObject(restaurants);
            }
            catch (Exception ex)
            {
                _logger.Log("GetAllRestaurants_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetRestaurantTypes()
        {
            try
            {
                var types = _restaurantOperations.GetAllRestaurantTypes();
                return JsonConvert.SerializeObject(types);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantTypes_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetRestaurantLocation(int restaurantId)
        {
            try
            {
                var location = _restaurantOperations.GetRestaurantLocation(restaurantId);
                return JsonConvert.SerializeObject(location);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantLocation_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [EBarAuth]
        public string GetRestaurantTables(int restaurantId)
        {
            try
            {
                var tables = _restaurantOperations.GetRestaurantTables(restaurantId);
                return JsonConvert.SerializeObject(tables);
            }
            catch (Exception ex)
            {
                _logger.Log("GetResturantTables_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [HttpPost]
        [EBarAuth]
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
                _logger.Log("SaveRestaurant_Exception: ", ex.Message);
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed.ToString();
            }

            return JsonConvert.SerializeObject(response);
        }

        public string GetRestaurantsForPr()
        {
            try
            {
                var restaurants = _restaurantOperations.GetRestaurantsForPr();
                return JsonConvert.SerializeObject(restaurants);

            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsForPr_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        public string Test()
        {
            using (var context = new DBModels.DBModels())
            {
                var product = context.RestaurantProducts.FirstOrDefault(x => x.ProductId == 2);
                var productMapped = Mapper.Map<RestaurantProducts, RestaurantProductsModel>(product);
                return JsonConvert.SerializeObject(productMapped);
            }
        }
    }
}
