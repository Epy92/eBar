using System.Collections.Generic;

namespace eBarService.Interfaces
{
    public interface IProductOperations
    {
        List<RestaurantProducts> GetAllRestaurantProducts(int restaurantId);
        List<RestaurantProducts> GetProductsByCategoryId(int categoryId);
        List<RestaurantProducts> GetProductsByPrice(int minPrice, int maxPrice);
        string AddProduct(RestaurantProducts product);
    }
}