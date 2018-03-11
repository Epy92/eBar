﻿using System;
using eBarService.Interfaces;
using eBarService.Messages;
using eBarService.Models;
using eBarService.ServiceInterfaces;
using Newtonsoft.Json;

namespace eBarService
{
    public class UserService : IUserService
    {
        private readonly IUserOperations _userOperations;
        public UserService(IUserOperations userOperations)
        {
            _userOperations = userOperations;
        }

        public string Register(UserTbl userRegister)
        {
            ResponseDataModel response = new ResponseDataModel();
            try
            {
                response.ResultMessage = _userOperations.RegisterUser(userRegister);
                response.ResultFlag = response.ResultMessage != UserMessages.DuplicateUser;
                response.ResultCode = ResultCode.UserInvalid.ToString();
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed.ToString();
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
                response.ResultCode = userLoginResult ? ResultCode.LoginSuccess.ToString() : ResultCode.MissingUser.ToString();
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed.ToString();
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
                response.ResultCode = ResultCode.ResetCodeGenerated.ToString();
            }
            catch (Exception ex)
            {
                response.ResultMessage = UserMessages.GenerateResetCodeFailed;
                response.ResultFlag = false;
                response.ResultCode = ResultCode.GenerateResetCodeFailed.ToString();
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
                response.ResultCode = ResultCode.PasswordChanged.ToString();
                response.ResultMessage = message;
            }
            else
            {
                response.ResultCode = message == UserMessages.MissingUser ? ResultCode.UserInvalid.ToString() : ResultCode.OperationFailed.ToString();
                response.ResultMessage = message;
            }
            return JsonConvert.SerializeObject(response);
        }
    }
}
