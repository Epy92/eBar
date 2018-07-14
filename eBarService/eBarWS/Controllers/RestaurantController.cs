using System;
using System.Web.Http;
using Newtonsoft.Json;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using eBarWS.Interfaces;
using eBarDatabase;
using eBarWS.Utils;
using ViewModels;

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

        public string GetRestaurantsByParameters(string keyword, string location, string county, string typeIDs, string lat, string longitude, int rangeKm, int nrOfRecordsToSkip)
        {
            List<RestaurantModel> l_rest = null;
            try
            {
                //if user wants restaurants by locality
                if (!string.IsNullOrEmpty(county))
                {
                    try
                    {
                        // se face cautare dupa judet si dupa localitate
                        l_rest = _restaurantOperations.GetRestaurantsObjListByLocation(county, location);

                        //filter by type if any
                        if (!string.IsNullOrEmpty(typeIDs))
                        {
                            l_rest = l_rest.Where(x => typeIDs.Split(';').ToList().Contains(x.RestaurantTypeId.ToString())).ToList();

                            //where(x=> typeIds.Split(";").toList().containts(x=>x.typeId))
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
                    if (!string.IsNullOrEmpty(typeIDs))
                    {
                        l_rest = _restaurantOperations.GetRestaurantsObjListByType(typeIDs);
                        //filter by keyword if any
                        if (!string.IsNullOrEmpty(keyword))
                        {
                            l_rest = l_rest.Where(x => x.RestaurantName.ToUpper().Contains(keyword) || x.RestaurantDescription.ToUpper().Contains(keyword)).ToList();
                        }
                    }
                    //get by keyword if any (in this case the type is for sure 0 and no need for filter)
                    else if (string.IsNullOrEmpty(typeIDs) && !string.IsNullOrEmpty(keyword))
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

                if (l_rest.Count > 10)
                {
                    if (nrOfRecordsToSkip > 0)
                    {
                        l_rest = l_rest.Skip(nrOfRecordsToSkip).Take(10).ToList();
                    }
                    else
                    {
                        l_rest = l_rest.Take(10).ToList();
                    }
                }
                return JsonConvert.SerializeObject(l_rest);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsByParameters_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        public string GetRestarantEvents(int restaurantId)
        {
            //todo implement: get data from database, now is hardcoded

            List<RestaurantEvent> events = new List<RestaurantEvent>();
            RestaurantEvent resEvent = new RestaurantEvent()
            {
                    RestaurantId = 1,
                    EventId = 1,
                    EventDescription = "Happy hour: In fiecare zi intre orele 12-14 avem meniul zile la doar 15 lei",
                    EventTitle = "Happy hour Restaurant OK",
            };
            events.Add(resEvent);
            RestaurantEvent resEvent2 = new RestaurantEvent()
            {
                RestaurantId = 2,
                EventDescription = "Oferta familly: In fiecare weekend pizza familly la 20 lei",
                EventTitle = "Pizza fammilly - 20 lei",
                EventStartDate = DateTime.Now,
                EventEndDate = DateTime.Now.AddMonths(1),
                EventId = 2
            };
            events.Add(resEvent2);

            return JsonConvert.SerializeObject(events);
        }
    }
}
