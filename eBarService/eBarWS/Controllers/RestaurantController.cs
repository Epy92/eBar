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
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Web;
using System.IO;

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

        //[EBarAuth]
        //public string GetRestaurantsByCity(string city)
        //{
        //    try
        //    {
        //        var restaurant = _restaurantOperations.GetRestaurantsByLocation(city);
        //        return JsonConvert.SerializeObject(restaurant);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Log("GetResturantsByCity_Exception: ", ex.Message);
        //        return JsonConvert.SerializeObject(null);
        //    }
        //}

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

        public string GetRestaurantsByParameters(string keyword, string location, string county, string typeIDs, string latitude, string longitude, int rangeKm, int nrOfRecordsToSkip)
        {
            List<RestaurantModel> restaurants = null;
            bool allParametersAreNull = AreAllParametersNull(keyword, location, county,typeIDs, latitude, longitude,rangeKm);
            try
            {
                //meaning the user searched without any filter
                if (allParametersAreNull)
                {
                    restaurants = _restaurantOperations.GetRestaurantsForPr();
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    restaurants = _restaurantOperations.GetRestaurantsByKeyword(keyword);
                }

                if (!string.IsNullOrEmpty(typeIDs))
                {
                    if (restaurants.Any())
                    {
                        var types = typeIDs.Split(',');
                        restaurants = restaurants.Where(x => types.Contains(x.RestaurantTypeId.ToString())).ToList();
                    }
                    else
                    {
                        restaurants = _restaurantOperations.GetRestaurantsByTypes(typeIDs);
                    }
                }

                if (!string.IsNullOrEmpty(county))
                {
                    if (restaurants.Any())
                    {
                        restaurants = restaurants.Where(x => x.RestaurantCounty == county).ToList();
                        if (!string.IsNullOrEmpty(location)) {
                            restaurants = restaurants.Where(x => x.RestaurantCity == location).ToList();
                        }
                    }
                    else
                    {
                        restaurants = _restaurantOperations.GetRestaurantsByCountyAndCity(county, location);
                    }
                }
                else {
                    if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(latitude) && rangeKm > 0)
                    {
                        restaurants = _restaurantOperations.GetRestaurantsByGeoCoordinate(null, null, 0, restaurants);
                    }
                }

                return JsonConvert.SerializeObject(restaurants.Skip(nrOfRecordsToSkip).Take(10).ToList());
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsByParameters_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        private bool AreAllParametersNull(string keyword, string location, string county, string typeIDs, string latitude, string longitude, int rangeKm)
        {
            bool result = true;

            result = result && string.IsNullOrEmpty(keyword);
            result = result && string.IsNullOrEmpty(county);
            result = result && string.IsNullOrEmpty(typeIDs);
            result = result && string.IsNullOrEmpty(latitude);
            result = result && string.IsNullOrEmpty(longitude);
            result = result && rangeKm > 0;

            return result;
        }

        public string GetCitiesByCounty(string county)
        {
            try
            {
                var cities = _restaurantOperations.GetCitiesByCounty(county);
                return JsonConvert.SerializeObject(cities);
            }
            catch (Exception ex)
            {
                _logger.Log("GetCitiesByCounty_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        [HttpPost]
        public async Task<string> SaveRestaurantDetails()
        {
            String fileBase64 = null;
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // get file and convert to base64
                foreach (MultipartFileData file in provider.FileData)
                {
                    var path = file.LocalFileName;
                    Byte[] bytes = File.ReadAllBytes(path);
                    fileBase64 = Convert.ToBase64String(bytes);

                    File.Delete(path);
                }

                Restaurants restaurant = SaveRestaurantOperations(fileBase64, provider);

                return JsonConvert.SerializeObject(restaurant);
            }
            catch (Exception ex)
            {
                _logger.Log("SaveRestaurantDetails_Exception", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }

        private Restaurants SaveRestaurantOperations(string fileBase64, MultipartFormDataStreamProvider provider)
        {
            //get restaurant details model
            RestaurantDetailsModel model = JsonConvert.DeserializeObject<RestaurantDetailsModel>(provider.FormData[0]);
            model.RestaurantThumbnail = fileBase64;

            Restaurants restaurant = new Restaurants() { RestaurantName = model.RestaurantName };
            _restaurantOperations.AddRestaurant(ref restaurant);
            RestaurantAdministrators restAdmin = new RestaurantAdministrators()
            {
                RestaurantId = restaurant.RestaurantId,
                UserID = model.UserId
            };
            _restaurantOperations.SaveRestaurantAdministrator(restAdmin);

            RestaurantLocations restLoc = new RestaurantLocations()
            {
                RestaurantCity = model.RestaurantCity,
                RestaurantCounty = model.RestaurantCounty,
                RestaurantAddress = model.RestaurantAddress,
                RestaurantId = restaurant.RestaurantId
            };
            _restaurantOperations.SaveRestaurantLocation(restLoc);

            RestaurantDetails restDetails = Mapper.Map<RestaurantDetailsModel, RestaurantDetails>(model);
            restDetails.RestaurantId = restaurant.RestaurantId;
            restDetails.RestaurantDirectoryGuid = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "Restaurant_" + restaurant.RestaurantId);
            _restaurantOperations.SaveRestaurantDetails(restDetails);

            RestaurantTypes restType = new RestaurantTypes()
            {
                TypeName = RestTypesEnum.RestaurantTypes[model.RestaurantTypeId],
                RestaurantId = restaurant.RestaurantId
            };
            _restaurantOperations.SaveRestaurantType(restType);
            return restaurant;
        }
    }
}
