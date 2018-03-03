using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using eBarService.Interfaces;

namespace eBarService.DatabaseOperations
{
    public class RestaurantOperations : IRestaurantOperations, IDisposable
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

        public List<Restaurants> GetResturantsByLocation(string latitude, string longitude, int rangeKm, string location)
        {
            List<int> restaurantIds = new List<int>();
            var geoCoordinate = new GeoCoordinate(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            var restaurantLocations = _databaseEntities.RestaurantLocations.Where(x => x.RestaurantCity.ToUpper() == location.ToUpper()).ToList();

            foreach (var restLocation in restaurantLocations)
            {
                var restaurantGeoCoordinate = new GeoCoordinate(Convert.ToDouble(restLocation.Latitude), Convert.ToDouble(restLocation.Longitude));
                double distanceInMeteres = geoCoordinate.GetDistanceTo(restaurantGeoCoordinate);
                if (distanceInMeteres/1000 < rangeKm)
                {
                    restaurantIds.Add(restLocation.RestaurantId);
                }
            }
            return _databaseEntities.Restaurants.Where(x => restaurantIds.Contains(x.RestaurantId)).ToList();
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

        public List<Restaurants> GetRestaurantsByLocation(string location)
        {
            return _databaseEntities.RestaurantLocations.Where(x => x.RestaurantCity.ToUpper() == location.ToUpper()).Select(x => x.Restaurants).ToList();
        }

        public string AddRestaurant(Restaurants restaurant)
        {
            string message = "Restaurant saved succesfully";
            try
            {
                _databaseEntities.Restaurants.Add(restaurant);
                _databaseEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                message = "Restaurant cannot be saved.";
            }
            return message;
        }

        public string DeleteRestaurant(int restaurantId)
        {
            string message = "Restaurant deleted succesfully";
            try
            {
                var restaurant = _databaseEntities.Restaurants.FirstOrDefault(x => x.RestaurantId == restaurantId);
                if (restaurant != null) _databaseEntities.Restaurants.Remove(restaurant);
            }
            catch (Exception)
            {
                message = "Delete of restaurant failed";
            }
            return message;
        }

        public void Dispose()
        {
            _databaseEntities.Dispose();
            _databaseEntities = null;
        }
    }
}