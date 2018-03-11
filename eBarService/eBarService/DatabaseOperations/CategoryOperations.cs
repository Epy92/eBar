﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBarService.Interfaces;
using eBarService.Messages;

namespace eBarService.DatabaseOperations
{
    public class CategoryOperations : ICategoryOperations
    {
        private eBarEntities _databaseEntities;
        public CategoryOperations()
        {
               _databaseEntities = new eBarEntities();
        }

        public List<RestaurantProductsCategories> GetRestaurantCategories(int restaurantId)
        {
            return _databaseEntities.RestaurantProductsCategories.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        public RestaurantProductsCategories GetRestaurantCategoryById(int categoryId)
        {
            return _databaseEntities.RestaurantProductsCategories.FirstOrDefault(x => x.CategoryId == categoryId);
        }

        public string AddCategory(RestaurantProductsCategories category)
        {
            string message = null;
            try
            {
                _databaseEntities.RestaurantProductsCategories.Add(category);
                _databaseEntities.SaveChanges();
                message = CategoryMessages.Saved;
            }
            catch (Exception ex)
            {
                message = CategoryMessages.Unsaved;
            }
            return message;
        }
    }
}