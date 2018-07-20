using System;
using System.Web.Http;
using Newtonsoft.Json;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using eBarWS.Interfaces;
using eBarDatabase;
using eBarDatabase.Interfaces;
using ViewModels;

namespace eBarWS.Controllers
{
    public class RestaurantEventController : ApiController
    {
        private readonly IRestaurantEventOperations _restaurantEventOperations;
        private readonly ILogger _logger;

        protected RestaurantEventController(ILogger logger, IRestaurantEventOperations restaurantEventOperations)
        {
            _logger = logger;
            _restaurantEventOperations = restaurantEventOperations;
        }

        public string GetRestarantEvents(int restaurantId)
        {
            try
            {
                var restaurantEvents = _restaurantEventOperations.GetRestaurantEvents(restaurantId);
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
                var restaurantEvents = _restaurantEventOperations.SaveRestaurantEvent(restaurantEvent);
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