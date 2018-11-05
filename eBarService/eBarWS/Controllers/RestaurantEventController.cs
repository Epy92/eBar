using AutoMapper;
using eBarDatabase;
using eBarWS.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ViewModels;

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

        public string GetRestaurantEventsForTimeline(long lastEventDate)
        {
            try
            {
                double ticks = double.Parse(lastEventDate.ToString());
                TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                DateTime evDate = new DateTime(1970, 1, 1) + time;
                var restaurantEvents = _restaurantEventOperations.GetRestaurantEventsForTimeline(evDate);

                //var rest = Mapper.Map<List<RestaurantEvent>, List<RestaurantEventModel>>(restaurantEvents);
                return JsonConvert.SerializeObject(restaurantEvents);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantEvents_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
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