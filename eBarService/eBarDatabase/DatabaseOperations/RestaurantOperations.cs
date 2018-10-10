﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using ViewModels;

namespace eBarDatabase
{
    public class RestaurantOperations : IRestaurantOperations, IDisposable
    {
        private DBModels _databaseEntities;
        private IDatabaseLogger _logger;

        public RestaurantOperations()
        {
            _databaseEntities = new DBModels();
            _logger = new DatabaseLogger();
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
        }

        public List<RestaurantTypes> GetRestaurantsByType(int typeId)
        {
            return _databaseEntities.RestaurantTypes.Where(x => x.TypeId == typeId).ToList();
        }

        public List<RestaurantTables> GetRestaurantTables(int restaurantId)
        {
            return _databaseEntities.RestaurantTables.Where(x => x.RestaurantId == restaurantId).ToList();
        }

        //public List<Restaurants> GetRestaurantsByLocation(string location)
        //{
        //    return _databaseEntities.RestaurantLocations.Where(x => x.RestaurantCity.ToUpper() == location.ToUpper()).Select(x => x.Restaurants).ToList();
        //}

        public string AddRestaurant(ref Restaurants restaurant)
        {
            string message = RestaurantMessages.RestaurantSaved;
            try
            {
                _databaseEntities.Restaurants.Add(restaurant);
                _databaseEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log("AddRestaurant_RestaurantOperations_Exception", ex.Message);
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
                using (var context = new DBModels())
                {
                    if(context.Database.Connection.State == System.Data.ConnectionState.Closed || context.Database.Connection.State == System.Data.ConnectionState.Broken)
                    {
                        context.Database.Connection.Open();
                    }
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantCounty = restLoc.RestaurantCounty,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsForPr_Exception", ex.Message);
            }
            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsByKeyword(string keyword)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   where rest.RestaurantName.ToUpper().Contains(keyword) || details.RestaurantDescription.ToUpper().Contains(keyword)
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantCounty = restLoc.RestaurantCounty,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsObjListByKeyword_RestaurantOperations_Exception", ex.Message);
            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsByTypes(string typeIDs)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId && x.TypeId.ToString() == typeIDs).DefaultIfEmpty()
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantCounty = restLoc.RestaurantCounty,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsObjListByType_RestaurantOperations_Exception", ex.Message);
            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsByCountyAndCity(string county, string location)
        {
            List<RestaurantModel> restaurants = null;
            try
            {
                using (var context = new DBModels())
                {
                    restaurants = (from rest in context.Restaurants
                                   from restLoc in context.RestaurantLocations.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from details in context.RestaurantDetails.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   from types in context.RestaurantTypes.Where(x => x.RestaurantId == rest.RestaurantId).DefaultIfEmpty()
                                   where (string.IsNullOrEmpty(location)) ? (restLoc.RestaurantCounty == county) : (restLoc.RestaurantCounty == county && restLoc.RestaurantCity == location)
                                   select new RestaurantModel()
                                   {
                                       RestaurantName = rest.RestaurantName,
                                       RestaurantCity = restLoc.RestaurantCity,
                                       RestaurantCounty = restLoc.RestaurantCounty,
                                       RestaurantDescription = details.RestaurantDescription,
                                       RestaurantType = types.TypeName,
                                       ThumbnailBase64String = details.RestaurantThumbnail,
                                       RestaurantAddress = restLoc.RestaurantAddress,
                                       RestaurantTypeId = types.TypeId
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsObjListByLocation_RestaurantOperations_Exception", ex.Message);
            }

            return restaurants;
        }

        public List<RestaurantModel> GetRestaurantsByGeoCoordinate(string latitude, string longitude, int rangeKm, List<RestaurantModel> restaurants)
        {
            List<RestaurantModel> restaurantList = new List<RestaurantModel>();
            try
            {
                var geoCoordinate = new GeoCoordinate(Convert.ToDouble(latitude), Convert.ToDouble(longitude));

                // if no restaurants get all and then filter by coordinates
                if(restaurants.Count == 0)
                {
                    restaurants = GetRestaurantsForPr();
                }
                else{
                    foreach (var restaurant in restaurants)
                    {
                        var restaurantLocations = _databaseEntities.RestaurantLocations.FirstOrDefault(x => x.RestaurantId.ToString() == restaurant.RestaurantId);

                        if (restaurantLocations != null)
                        {
                            var restaurantGeoCoordinate = new GeoCoordinate(Convert.ToDouble(restaurantLocations.Latitude), Convert.ToDouble(restaurantLocations.Longitude));

                            double distanceInMeteres = geoCoordinate.GetDistanceTo(restaurantGeoCoordinate);

                            if (distanceInMeteres / 1000 < rangeKm)
                            {
                                restaurantList.Add(restaurant);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetRestaurantsObjListByGeoCoordinate_RestaurantOperations_Exception", ex.Message);
            }
            return restaurantList;
        }

        public string SaveRestaurantDetails(RestaurantDetails restaurantDetails)
        {
            string message;
            try
            {
                using (var context = new DBModels())
                {
                    var restDetail = context.RestaurantDetails.FirstOrDefault(x => x.RestaurantId == restaurantDetails.RestaurantId);
                    if (restDetail == null)
                    {
                        message = RestaurantMessages.RestaurantDetailsNotSaved;
                    }
                    else
                    {
                        restDetail.RestaurantDescription = restaurantDetails.RestaurantDescription;
                        restDetail.RestaurantThumbnail = restaurantDetails.RestaurantThumbnail;
                        restDetail.RestaurantProgram = restaurantDetails.RestaurantProgram;
                        restDetail.RestaurantContact = restaurantDetails.RestaurantContact;
                        context.Entry(restDetail).State = EntityState.Modified;
                        context.SaveChanges();
                        message = RestaurantMessages.RestaurantDetailsSaved; ;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log("SaveRestaurantDetails_RestaurantOperations_Exception", ex.Message);
                return message = RestaurantMessages.RestaurantDetailsNotSaved;
            }
            return message;
        }

        public void Dispose()
        {
            _databaseEntities.Dispose();
            _databaseEntities = null;
        }

        public List<CountiesAndCities> GetCitiesByCounty(string county)
        {
            List<CountiesAndCities> cities = new List<CountiesAndCities>();
            try
            {
                using (var context = new DBModels())
                {
                    cities = context.CountiesAndCities.Where(x => x.Counties == county).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Log("GetCitiesByCounty_Exception", ex.Message);
            }
            return cities;
        }

        public string SaveRestaurantAdministrator(RestaurantAdministrators restaurantAdmin)
        {
            string message = RestaurantMessages.RestaurantAdministratorSaved;
            try
            {
                _databaseEntities.RestaurantAdministrators.Add(restaurantAdmin);
                _databaseEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log("SaveRestaurantAdministrator_RestaurantOperations_Exception", ex.Message);
                message = RestaurantMessages.RestaurantAdministratorNotSaved;
            }
            return message;
        }

        public string SaveRestaurantLocation(RestaurantLocations restaurantLocation)
        {
            string message = RestaurantMessages.RestaurantLocationSaved;
            try
            {
                _databaseEntities.RestaurantLocations.Add(restaurantLocation);
                _databaseEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log("SaveRestaurantLocation_RestaurantOperations_Exception", ex.Message);
                message = RestaurantMessages.RestaurantLocationNotSaved;
            }
            return message;
        }

        public string SaveRestaurantType(RestaurantTypes restaurantTypes)
        {
            string message = RestaurantMessages.RestaurantLocationSaved;
            try
            {
                _databaseEntities.RestaurantTypes.Add(restaurantTypes);
                _databaseEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log("SaveRestaurantLocation_RestaurantOperations_Exception", ex.Message);
                message = RestaurantMessages.RestaurantLocationNotSaved;
            }
            return message;
        }
    }
}