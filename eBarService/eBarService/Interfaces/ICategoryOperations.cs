using System.Collections.Generic;

namespace eBarService.Interfaces
{
    public interface ICategoryOperations
    {
        List<RestaurantProductsCategories> GetRestaurantCategories(int restaurantId);
        RestaurantProductsCategories GetRestaurantCategoryById(int categoryId);

        string AddCategory(RestaurantProductsCategories category);
    }
}