using System;
using System.Collections.Generic;
using System.Linq;
using DBModels;
using eBarWS.Interfaces;
using eBarWS.Messages;

namespace eBarWS.DatabaseOperations
{
    public class CategoryOperations : ICategoryOperations
    {
        private DBModels.DBModels _databaseEntities;
        public CategoryOperations()
        {
               _databaseEntities = new DBModels.DBModels();
        }

        //public List<RestaurantProductsCategories> GetRestaurantCategories(int restaurantId)
        //{
        //    return _databaseEntities.RestaurantProductsCategories.Where(x => x.RestaurantId == restaurantId).ToList();
        //}

        //public RestaurantProductsCategories GetRestaurantCategoryById(int categoryId)
        //{
        //    return _databaseEntities.RestaurantProductsCategories.FirstOrDefault(x => x.CategoryId == categoryId);
        //}

//        public string AddCategory(RestaurantProductsCategories category)
//        {
//            string message = null;
//            try
//            {
//                _databaseEntities.RestaurantProductsCategories.Add(category);
//                _databaseEntities.SaveChanges();
//                message = CategoryMessages.Saved;
//            }
//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
//            catch (Exception ex)
//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
//            {
//                message = CategoryMessages.Unsaved;
//            }
//            return message;
//        }
    }
}