using eBarDatabase;
using eBarWS.Interfaces;
using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace eBarWS.Controllers
{
    public class RestaurantEventController : ApiController
    {
        private readonly IRestaurantEventOperations _restaurantEventOperations;
        private readonly ILogger _logger;

        public RestaurantEventController(ILogger logger, IRestaurantEventOperations restaurantEventOperations)
        {
            _logger = logger;
            _restaurantEventOperations = restaurantEventOperations;
        }

        public string GetRestaurantEvents(int userId)
        {
            try
            {
                var restaurantEvents = _restaurantEventOperations.GetRestaurantEvents(userId);
                return JsonConvert.SerializeObject(restaurantEvents);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantEvents_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }
        public string CreateRestaurantEvent(RestaurantEvent restaurantEvent)
        {
            try
            {
                var restaurantEvents = _restaurantEventOperations.SaveRestaurantEvent(restaurantEvent);
                return JsonConvert.SerializeObject(restaurantEvents);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantEvents_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }
        public string UpdateRestaurantEvent(RestaurantEvent restaurantEvent)
        {
            try
            {
                var restaurantEvents = _restaurantEventOperations.UpdateRestaurantEvent(restaurantEvent);
                return JsonConvert.SerializeObject(restaurantEvents);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantEvents_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }
    }
}