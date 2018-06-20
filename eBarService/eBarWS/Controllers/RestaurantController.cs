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
using System.Web.Routing;

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

        public string GetRestaurantsByParameters(string keyword, string location, int typeId, string lat, string longitude, int rangeKm)
        {
            List<RestaurantModel> l_rest = null;
            try
            {
                //if user wants restaurants by location
                if (!string.IsNullOrEmpty(location))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByLocation(location);
                        //filter by type if any
                        if (typeId != 0)
                        {
                            l_rest = l_rest.Where(x => x.RestaurantTypeId == typeId).ToList();
                        }
                        //filter by keyword if any
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            l_rest = l_rest.Where(x => x.RestaurantName.ToUpper().Contains(keyword) || x.RestaurantDescription.ToUpper().Contains(keyword)).ToList();
                        }

                        //return JsonConvert.SerializeObject(l_rest);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsByLocation_Exception: ", ex.Message);
                        return JsonConvert.SerializeObject(null);
                    }
                }
                //if user wants restaurants by range
                else if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(longitude) && rangeKm > 0)
                {
                    //get by type if any
                    if (typeId > 0)
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByType(typeId);
                        //filter by keyword if any
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            l_rest = l_rest.Where(x => x.RestaurantName.ToUpper().Contains(keyword) || x.RestaurantDescription.ToUpper().Contains(keyword)).ToList();
                        }
                    }
                    //get by keyword if any (in this case the type is for sure 0 and no need for filter)
                    else if (typeId == 0 && !string.IsNullOrEmpty(keyword))
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByKeyword(keyword);
                    }

                    //if there are no restaurants means type = 0 and keyword is null and no branch was executed (if or elseif)
                    // then get all restaurants and filter by range and coordinates
                    if (l_rest == null)
                    {
                        l_rest = _restaurantOperations.GetRestaurantsForPr();
                    }

                    l_rest = _restaurantOperations.GetRestaurantsObjListByGeoCoordinate(lat, longitude, rangeKm, l_rest);
                }

                #region IOAN_VERSION_REFACTORED

                ////////////// IOAN///////////////////////
                // Get restaurants by location
                if (!string.IsNullOrEmpty(location) && (typeId == 0) && (keyword == string.Empty))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByLocation(location);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsByLocation_Exception: ", ex.Message);
                        l_rest = null;
                    }
                }

                //get restaurants by name
                else if ((location == string.Empty) && (typeId == 0) && !string.IsNullOrEmpty(keyword))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByKeyword(keyword);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsByName_Exception: ", ex.Message);
                        l_rest = null;
                    }
                }
                // get restaurants by type
                else if ((location == string.Empty) && typeId != 0 && (keyword == string.Empty))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByType(typeId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsObjListByType_Exception: ", ex.Message);
                        l_rest = null;
                    }

                }
                //get restaurants by location and name
                else if (!string.IsNullOrEmpty(location) && (typeId == 0) && !string.IsNullOrEmpty(keyword))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByLocationAndName(location, keyword);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsObjListByLocationAndName_Exception: ", ex.Message);
                        l_rest = null;
                    }
                }
                // get restaurants by location and type
                else if (location != string.Empty && typeId != 0 && (keyword == string.Empty))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByLocationAndType(location, typeId);
                        return JsonConvert.SerializeObject(l_rest);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsObjListByLocationAndType_Exception: ", ex.Message);
                        l_rest = null;
                    }
                }

                // get restaurants by name and type
                else if ((location == string.Empty) && typeId != 0 && keyword != string.Empty)
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByNameAndType(keyword, typeId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsObjListByNameAndType_Exception: ", ex.Message);
                        l_rest = null;
                    }
                }

                if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(longitude) && (rangeKm > 0))
                {
                    try
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByGeoCoordinate(lat, longitude, rangeKm, l_rest);
                    }
                    catch (Exception ex)
                    {
                        _logger.Log("GetRestaurantsObjListByGeoCoordinate_Exception: ", ex.Message);
                        l_rest = null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsByParameters_Exception: ", ex.Message);
                l_rest = null;
            }
#endregion

            return JsonConvert.SerializeObject(l_rest);
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
