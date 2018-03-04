using System;
using eBarService.DatabaseOperations;
using eBarService.Models;
using Newtonsoft.Json;

namespace eBarService
{
    public class ServiceEbar : IServiceEbar
    {
        public string Register(UserTbl userRegister)
        {
            ResponseDataModel response = new ResponseDataModel();

            try
            {
                response.ResultMessage = new UserOperations().RegisterUser(userRegister);
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
           return JsonConvert.SerializeObject(new UserOperations().IsUserValid(userLogin.Username ?? userLogin.Email, userLogin.UserPassword));
        }
    }
}
