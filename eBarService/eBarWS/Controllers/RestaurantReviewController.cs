using eBarDatabase;
using eBarWS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Http;
using AutoMapper;
using ViewModels;

namespace eBarWS.Controllers
{
    public class RestaurantReviewController : ApiController
    {
        private readonly IRestaurantReviewOperations _restaurantReviewOperations;
        private readonly ILogger _logger;

        public RestaurantReviewController(ILogger logger, IRestaurantReviewOperations restaurantReviewOperations)
        {
            _logger = logger;
            _restaurantReviewOperations = restaurantReviewOperations;
        }
        // returneaza toate recenziile unui restaurant
        public string GetRestaurantReview(int restaurantId)
        {
            try
            {
                
                var restaurantReview = _restaurantReviewOperations.GetRestaurantReviews(restaurantId);
                //RestaurantReviewModel restDetails = Mapper.Map<RestaurantReview, RestaurantReviewModel> (restaurantReview);

                return JsonConvert.SerializeObject(restaurantReview);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantReview_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }
        public string CreateRestaurantReview(RestaurantReview restaurantReview)
        {
            try
            {
                var restaurantEvents = _restaurantReviewOperations.CreateRestaurantReview(restaurantReview);
                return JsonConvert.SerializeObject(restaurantEvents);
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantEvents_Exception: ", ex.Message);
                return JsonConvert.SerializeObject(null);
            }
        }
        public string UpdateRestaurantReview(RestaurantReview restaurantReview)
        {
            try
            {
                var restaurantEvents = _restaurantReviewOperations.UpdateRestaurantReview(restaurantReview);
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