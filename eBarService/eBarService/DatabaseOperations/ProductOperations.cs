using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBarService.Interfaces;
using eBarService.Messages;

namespace eBarService.DatabaseOperations
{
    public class ProductOperations : IProductOperations
    {
        private readonly eBarEntities _databaseEntities;
        public ProductOperations()
        {
            _databaseEntities = new eBarEntities();
        }

        public List<RestaurantProducts> GetAllRestaurantProducts(int restaurantId)
        {
            return _databaseEntities.RestaurantProducts.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        public List<RestaurantProducts> GetProductsByCategoryId(int categoryId)
        {
            return _databaseEntities.RestaurantProducts.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<RestaurantProducts> GetProductsByPrice(int minPrice, int maxPrice)
        {
            return _databaseEntities.RestaurantProducts.Where(x => x.ProductPrice > minPrice && x.ProductPrice < maxPrice).ToList();
        }

        public string AddProduct(RestaurantProducts product)
        {
            string message;
            try
            {
                _databaseEntities.RestaurantProducts.Add(product);
                _databaseEntities.SaveChanges();
                message = ProductMessages.ProductSaved;
            }
            catch (Exception ex)
            {
                message = ProductMessages.ProductUnsaved;
            }
            return message;
        }
    }
}