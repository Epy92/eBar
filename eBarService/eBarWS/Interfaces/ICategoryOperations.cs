using System.Collections.Generic;
using DBModels;

namespace eBarWS.Interfaces
{
    public interface ICategoryOperations
    {
        List<RestaurantProductsCategories> GetRestaurantCategories(int restaurantId);
        RestaurantProductsCategories GetRestaurantCategoryById(int categoryId);
        string AddCategory(RestaurantProductsCategories category);
    }
}