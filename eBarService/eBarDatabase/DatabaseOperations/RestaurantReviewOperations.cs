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
            return _databaseEntities.RestaurantReview.Where(x => x.RestaurantId == restaurantId).ToList();
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
                    saveMessage = RestaurantReviewMessages.OkMessage;
                }
            }
            catch (Exception ex)
            {
                saveMessage = RestaurantEventMessage.NokMessage;
                _logger.Log("SaveRestaurantReview_Exception", ex.Message);
            }
            return saveMessage;
        }
    }
}
