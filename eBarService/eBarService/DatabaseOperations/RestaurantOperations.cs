using System.Collections.Generic;
using System.Linq;
using eBarService.Interfaces;

namespace eBarService.DatabaseOperations
{
    public class RestaurantOperations : IRestaurantOperations
    {
        private eBarEntities _databaseEntities;

        public RestaurantOperations()
        {
            _databaseEntities = new eBarEntities();        
        }
        public List<RestaurantLocations> GetResturantLocation(int restaurantId)
        {
            return _databaseEntities.RestaurantLocations.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        public List<RestaurantTypes> GetAllRestaurantTypes()
        {
            return _databaseEntities.RestaurantTypes.ToList();
        }

        public List<Restaurants> GetAllRestaurantes()
        {
            return _databaseEntities.Restaurants.ToList();
        }

        public List<Restaurants> GetResturantesByLocation(string latitude, string longitude)
        {
            throw new System.NotImplementedException();
        }

        public Restaurants GetRestaurantById(int restaurantId)
        {
            return _databaseEntities.Restaurants.FirstOrDefault(x => x.RestaurantId == restaurantId);
        }

        public List<RestaurantAdministrators> GetRestaurantAdministrators(int restaurantId)
        {
            return _databaseEntities.RestaurantAdministrators.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        public List<RestaurantTypes> GetRestaurantTypesByType(int typeId)
        {
            return _databaseEntities.RestaurantTypes.Where(x => x.TypeId == typeId).ToList();
        }

        public List<RestaurantTables> GetRestaurantTables(int restaurantId)
        {
            return _databaseEntities.RestaurantTables.Where(x => x.RestaurantId == restaurantId).ToList();
        }
    }
}