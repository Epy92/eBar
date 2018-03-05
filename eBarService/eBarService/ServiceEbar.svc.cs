using System;
using eBarService.DatabaseOperations;
using eBarService.Interfaces;
using eBarService.Models;
using Newtonsoft.Json;

namespace eBarService
{
    public class ServiceEbar : IServiceEbar
    {
        private IUserOperations _userOperations;
        private IProductOperations _productOperations;
        private IRestaurantOperations _restaurantOperations;
        public ServiceEbar(IUserOperations userOperations, IProductOperations productOperations, IRestaurantOperations restaurantOperations)
        {
            _restaurantOperations = restaurantOperations;
            _productOperations = productOperations;
            _userOperations = userOperations;
        }

        public string Register(UserTbl userRegister)
        {
            ResponseDataModel response = new ResponseDataModel();

            try
            {
                response.ResultMessage = _userOperations.RegisterUser(userRegister);
                response.ResultFlag = true;
                response.ResultCode = ResultCode.OperationSuccess;
            }
            catch (Exception ex)
            {
                response.ResultCode = ResultCode.OperationFailed;
            }

            return JsonConvert.SerializeObject(response);
        }

        public string UserLogin(UserTbl userLogin)
        {
           return JsonConvert.SerializeObject(_userOperations.IsUserValid(userLogin.Username ?? userLogin.Email, userLogin.UserPassword));
        }

        public string GetResturantsByLocation(string latitude, string longitude, string range, string location)
        {
            var restaurants = _restaurantOperations.GetResturantsByLocation(latitude, longitude, Convert.ToInt32(range), location);
            return JsonConvert.SerializeObject(restaurants);
        }

        public string GetRestaurantById(int restaurantId)
        {
            var restaurant = _restaurantOperations.GetRestaurantById(restaurantId);
            return JsonConvert.SerializeObject(restaurant);
        }

        public string GetRestaurantsByCity(string city)
        {
            var restaurant = _restaurantOperations.GetRestaurantsByLocation(city);
            return JsonConvert.SerializeObject(restaurant);
        }

        public string GetRestaurantsByType(int typeId)
        {
            var restaurants = _restaurantOperations.GetRestaurantsByType(typeId);
            return JsonConvert.SerializeObject(restaurants);
        }

        public string GetRestaurantTypes()
        {
            var types = _restaurantOperations.GetAllRestaurantTypes();
            return JsonConvert.SerializeObject(types);
        }

        public string GetRestaurantLocation(int restaurantId)
        {
            var location = _restaurantOperations.GetRestaurantLocation(restaurantId);
            return JsonConvert.SerializeObject(location);
        }

        public string GetRestaurantTables(int restaurantId)
        {
            var tables = _restaurantOperations.GetRestaurantTables(restaurantId);
            return JsonConvert.SerializeObject(tables);
        }
    }
}
