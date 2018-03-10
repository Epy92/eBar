using System;
using System.Collections.Generic;
using System.Linq;
using eBarService.DatabaseOperations;
using eBarService.Interfaces;
using eBarService.Messages;
using eBarService.Models;
using Newtonsoft.Json;

namespace eBarService
{
    public class ServiceEbar : IServiceEbar
    {
        private readonly IUserOperations _userOperations;
        private IProductOperations _productOperations;
        private readonly IRestaurantOperations _restaurantOperations;
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
                response.ResultFlag = response.ResultMessage != UserMessages.DuplicateUser;
                response.ResultCode = ResultCode.UserInvalid;
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed;
            }

            return JsonConvert.SerializeObject(response);
        }

        public string UserLogin(UserTbl userLogin)
        {
            ResponseDataModel response = new ResponseDataModel();
            try
            {
                var userLoginResult = _userOperations.IsUserValid(userLogin.Username ?? userLogin.Email, userLogin.UserPassword);
                response.ResultFlag = userLoginResult;
                response.ResultMessage = userLoginResult ? UserMessages.LoginSuccess : UserMessages.MissingUser;
                response.ResultFlag = true;
                response.ResultCode = ResultCode.OperationSuccess;
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed;
            }
            return JsonConvert.SerializeObject(response);
        }

        public string GenerateResetCode(string usernameOrEmail)
        {
            ResponseDataModel response = new ResponseDataModel();
            try
            {
                string message = null;
                var resultFlag = false;
                _userOperations.GenerateResetCode(usernameOrEmail, out message, out resultFlag);
                response.ResultMessage = string.IsNullOrEmpty(message) ? UserMessages.ResetCodeGenerated : message;
                response.ResultFlag = resultFlag;
                response.ResultCode = ResultCode.OperationSuccess;
            }
            catch (Exception ex)
            {
                response.ResultMessage = UserMessages.GenerateResetCodeFailed;
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed;
            }
            return JsonConvert.SerializeObject(response);
        }

        public string ResetUserPassword(string username, string resetCode, string newPassword)
        {
            //var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            ResponseDataModel response = new ResponseDataModel();
            string message = _userOperations.ResetUserPassword(username, resetCode, newPassword);

            if (message == UserMessages.PasswordChanged)
            {
                response.ResultFlag = true;
                response.ResultCode = ResultCode.OperationSuccess;
                response.ResultMessage = message;
            }
            else
            {
                response.ResultCode = message == UserMessages.MissingUser ? ResultCode.UserInvalid : ResultCode.OperationFailed;
                response.ResultMessage = message;
            }
            return JsonConvert.SerializeObject(response);
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
