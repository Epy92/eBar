using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace eBarDatabase
{
    public class RestaurantReviewOperations : IRestaurantReviewOperations
    {
        private IDatabaseLogger _logger;
        private DBModels _databaseEntities;
        public RestaurantReviewOperations()
        {
            _logger = new DatabaseLogger();
            _databaseEntities = new DBModels();
        }
        public List<RestaurantReview> GetRestaurantReviews(int restaurantId)
        {
            List<RestaurantReview> reviews = new List<RestaurantReview>();

            using (var context = new DBModels())
            {

                if (context.Database.Connection.State == System.Data.ConnectionState.Closed || context.Database.Connection.State == System.Data.ConnectionState.Broken)
                {
                    context.Database.Connection.Open();
                }
                reviews = context.RestaurantReview.Where(x => x.RestaurantId == restaurantId).ToList();
            }

            return reviews;
        }

        public string CreateRestaurantReview(RestaurantReview restaurantReview)
        {
            string saveMessage;
            try
            {
                using (var context = new DBModels())
                {
                    context.RestaurantReview.Add(restaurantReview);
                    context.SaveChanges();
                    saveMessage = RestaurantReviewMessages.OkCreatMessage;
                }
            }
            catch (Exception ex)
            {
                saveMessage = RestaurantReviewMessages.NokCreatMessage;
                _logger.Log("SaveRestaurantReview_Exception", ex.Message);
            }
            return saveMessage;
        }
        public string UpdateRestaurantReview(RestaurantReview restaurantReview)
        {
            string message;
            try
            {
                using (var context = new DBModels())
                {
                    var reviewRest = context.RestaurantReview.FirstOrDefault(x => x.RestaurantReviewID == restaurantReview.RestaurantReviewID);
                    if (reviewRest == null)
                    {
                        message = RestaurantReviewMessages.NokUpdateMessage;
                    }
                    else
                    {
                        reviewRest.ReviewComment = restaurantReview.ReviewComment;
                        context.Entry(reviewRest).State = EntityState.Modified;
                        context.SaveChanges();
                        message = RestaurantReviewMessages.OKUpdateMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log("UpdateRestaurantReview_Exception", ex.Message);
                return message = RestaurantEventMessage.NokUpdate;
            }
            return message;
        }
    }
}
