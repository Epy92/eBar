using System.Collections.Generic;

namespace ViewModels
{
    public static class RestTypesEnum
    {
        public static Dictionary<int, string> RestaurantTypes;
        static RestTypesEnum()
        {
            RestaurantTypes = new Dictionary<int, string>();
            RestaurantTypes.Add(1, "Pizzerie");
            RestaurantTypes.Add(2, "Bar/Pub");
            RestaurantTypes.Add(3, "Traditional romanesc");
            RestaurantTypes.Add(4, "Cafenea");
            RestaurantTypes.Add(5, "Restaurant italian");
            RestaurantTypes.Add(6, "Brasserie");
            RestaurantTypes.Add(7, "Berarie");
            RestaurantTypes.Add(8, "Rotiserie");
            RestaurantTypes.Add(9, "Ceainarie");
            RestaurantTypes.Add(10, "Restaurant grecesc");
            RestaurantTypes.Add(11, "Restaurant chinezesc");
            RestaurantTypes.Add(12, "Restaurant libanez");
            RestaurantTypes.Add(13, "Restaurant turcesc");
            RestaurantTypes.Add(14, "Fast food");
        }
    }
}
