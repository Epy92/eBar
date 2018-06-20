using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using DBModels;
using eBarWS.Interfaces;
using eBarWS.Messages;
using eBarWS.Models;

namespace eBarWS.DatabaseOperations
{
    public class RestaurantOperations : IRestaurantOperations, IDisposable
    {
        private DBModels.DBModels _databaseEntities;

        public RestaurantOperations()
        {
            _databaseEntities = new DBModels.DBModels();
        }

        public List<RestaurantLocations> GetRestaurantLocation(int restaurantId)
        {
            return _databaseEntities.RestaurantLocations.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        public List<RestaurantTypes> GetAllRestaurantTypes()
        {
            return _databaseEntities.RestaurantTypes.ToList();
        }

        public List<Restaurants> GetAllRestaurants()
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
                if (distanceInMeteres / 1000 < rangeKm)
                {
                    restaurantIds.Add(restLocation.RestaurantId);
                }
            }
            return _databaseEntities.Restaurants.Where(x => restaurantIds.Contains(x.RestaurantId)).ToList();
        }

        public Restaurants GetRestaurantById(int restaurantId)
        {
            var rest = _databaseEntities.Restaurants.FirstOrDefault(x => x.RestaurantId == restaurantId);
            return rest;
            //if (rest != null)
            //{
            //    RestaurantModel restModel = new RestaurantModel()
            //    {
            //        RestaurantName = rest.RestaurantName,
            //        RestaurantAddress = rest.RestaurantLocations.First().RestaurantAddress,
            //        RestaurantCity = rest.RestaurantLocations.First().RestaurantCity,
            //        RestaurantDescription = rest.RestaurantDetails.First().RestaurantDescription,
            //        RestaurantType = rest.RestaurantTypes.First().TypeName,
            //        ThumbnailBase64String = rest.RestaurantDetails.First().RestaurantThumbnail
            //    };
            //    return restModel;
            //}
            //return null;
        }

        public List<RestaurantAdministrators> GetRestaurantAdministrators(int restaurantId)
        {
            return _databaseEntities.RestaurantAdministrators.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        public List<RestaurantTypes> GetRestaurantsByType(int typeId)
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
            string message = RestaurantMessages.RestaurantSaved;
            try
            {
                _databaseEntities.Restaurants.Add(restaurant);
                _databaseEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                message = RestaurantMessages.RestaurantNotSaved;
            }
            return message;
        }

        public string DeleteRestaurant(int restaurantId)
        {
            string message = RestaurantMessages.RestaurantDeleted;
            try
            {
                var restaurant = _databaseEntities.Restaurants.FirstOrDefault(x => x.RestaurantId == restaurantId);
                if (restaurant != null) _databaseEntities.Restaurants.Remove(restaurant);
            }
            catch (Exception)
            {
                message = RestaurantMessages.RestaurantNotDeleted;
            }
            return message;
        }

        public List<RestaurantModel> GetRestaurantsForPr()
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByName(string keyword)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants.Where(x => x.RestaurantName.ToUpper().Contains(keyword.ToUpper())).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()

                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            if (restaurants == null)
            {
                try
                {
                    using (var context = new DBModels.DBModels())
                    {
                        restaurants = (from rest in context.Restaurants
                                       from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantDescription.ToUpper().Contains(keyword.ToUpper())).DefaultIfEmpty()
                                       from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()

                                       from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                       select new RestaurantModel()
                                       {
                                           RestaurantName = rest.RestaurantName,
                                           RestaurantCity = restLoc.RestaurantCity,
                                           RestaurantDescription = details.RestaurantDescription,
                                           RestaurantType = types.TypeName,
                                           ThumbnailBase64String = details.RestaurantThumbnail,
                                           RestaurantAddress = restLoc.RestaurantAddress
                                       }).ToList();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByLocationAndName(string location, string keyword)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants.Where(x => x.RestaurantName.ToUpper().Contains(keyword.ToUpper())).DefaultIfEmpty()
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantCity == location).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            if (restaurants == null)
            {
                try
                {
                    using (var context = new DBModels.DBModels())
                    {
                        restaurants = (from rest in context.Restaurants
                                       from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantCity == location).DefaultIfEmpty()
                                       from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantDescription.ToUpper().Contains(keyword)).DefaultIfEmpty()
                                       from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                       select new RestaurantModel()
                                       {
                                           RestaurantName = rest.RestaurantName,
                                           RestaurantCity = restLoc.RestaurantCity,
                                           RestaurantDescription = details.RestaurantDescription,
                                           RestaurantType = types.TypeName,
                                           ThumbnailBase64String = details.RestaurantThumbnail,
                                           RestaurantAddress = restLoc.RestaurantAddress
                                       }).ToList();
                    }
                }
                catch (Exception ex)
                {

                }

            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByType(int typeid)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId && x.TypeId == typeid).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByLocation(string location)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantCity == location).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByLocationAndType(string location, int typeid)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantCity == location).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId && x.TypeId == typeid).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByNameAndType(string keyword, int typeid)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels.DBModels())
                {
                    restaurants = (from rest in context.Restaurants.Where(x => x.RestaurantName.ToUpper().Contains(keyword.ToUpper())).DefaultIfEmpty()
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId && x.TypeId == typeid).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            if (restaurants == null)
            {
                try
                {
                    using (var context = new DBModels.DBModels())
                    {
                        restaurants = (from rest in context.Restaurants
                                       from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                       from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId && x.RestaurantDescription.ToUpper().Contains(keyword)).DefaultIfEmpty()
                                       from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId && x.TypeId == typeid).DefaultIfEmpty()
                                       select new RestaurantModel()
                                       {
                                           RestaurantName = rest.RestaurantName,
                                           RestaurantCity = restLoc.RestaurantCity,
                                           RestaurantDescription = details.RestaurantDescription,
                                           RestaurantType = types.TypeName,
                                           ThumbnailBase64String = details.RestaurantThumbnail,
                                           RestaurantAddress = restLoc.RestaurantAddress
                                       }).ToList();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsObjListByGeoCoordinate(string latitude, string longitude, int rangeKm, List<RestaurantModel> restaurants)
        {
            List <RestaurantModel> l_list = new List<RestaurantModel>();
            var geoCoordinate = new GeoCoordinate(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            

            foreach (var restaurant in restaurants)
            {
                var restaurantLocations = _databaseEntities.RestaurantLocations.Where(x => x.RestaurantId.ToString() == restaurant.RestaurantId).FirstOrDefault();

                var restaurantGeoCoordinate = new GeoCoordinate(Convert.ToDouble(restaurantLocations.Latitude), Convert.ToDouble(restaurantLocations.Longitude));

                double distanceInMeteres = geoCoordinate.GetDistanceTo(restaurantGeoCoordinate);

                if (distanceInMeteres / 1000 < rangeKm)
                {
                    l_list.Add(restaurant);
                }
            }
            return l_list;
        }


        public void Dispose()
        {
            _databaseEntities.Dispose();
            _databaseEntities = null;
        }
    }
}