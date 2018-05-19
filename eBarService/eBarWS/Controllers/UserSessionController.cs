using System;
using System.Web;
using System.Web.Http;
using DBModels;
using eBarWS.Interfaces;
using eBarWS.Messages;
using eBarWS.Models;
using Newtonsoft.Json;
using eBarWS.Utils;

namespace eBarWS.Controllers
{
    public class UserSessionController : ApiController
    {
        private IUserOperations _userOperations = null;
        private ILogger _logger = null;

        public UserSessionController(IUserOperations userOperations, ILogger logger)
        {
            _userOperations = userOperations;
            _logger = logger;
        }

        [HttpPost]
        public string Register(UserTbl userRegister)
        {
            ResponseDataModel response = new ResponseDataModel();
            try
            {
                response.ResultMessage = _userOperations.RegisterUser(userRegister);
                response.ResultFlag = response.ResultMessage != UserMessages.DuplicateUser;
                response.ResultCode = response.ResultMessage != UserMessages.DuplicateUser ? ResultCode.RegisterSuccess.ToString() : ResultCode.UserInvalid.ToString();
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed.ToString();
                _logger.Log("RegisterUser_Exception: ", ex.Message);
            }

            return JsonConvert.SerializeObject(response);
        }

        [HttpPost]
        public string UserLogin(UserTbl userLogin)
        {
            SessionResponse response = new SessionResponse();
            try
            {
               // UserTbl userLogin = JsonConvert.DeserializeObject<UserTbl>(userJson);
                var user = _userOperations.GetUser(userLogin.Username ?? userLogin.Email, userLogin.UserPassword);
                

                if (user == null)
                {
                    response.ResultFlag = false;
                    response.ResultMessage = UserMessages.MissingUser;
                    response.ResultCode = ResultCode.MissingUser.ToString();
                }
                else
                {
                    response.ResultFlag = true;
                    response.ResultMessage = UserMessages.LoginSuccess;
                    response.ResultCode = ResultCode.LoginSuccess.ToString();
                    response.UserDetails = new UserModel()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Username = user.Username,
                        UserPreferredLanguage = user.UserPreferredLanguage,
                        UserTypeId = user.UserTypeId
                    };
                    response.SessionKey = Guid.NewGuid().ToString();
                    
                    HttpContext.Current.Session.Add("Session_ID", response.SessionKey);
                }               
            }
            catch (Exception ex)
            {
                response.ResultFlag = false;
                response.ResultCode = ResultCode.OperationFailed.ToString();
                _logger.Log("UserLogin_Exception: ", ex.Message);
            }
            return JsonConvert.SerializeObject(response);
        }

        [EBarAuth]
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

                switch (message)
                {
                    case UserMessages.MissingUser:
                        response.ResultCode = ResultCode.MissingUser.ToString();
                        break;
                    case UserMessages.ResetCodeAlreadyGenerated:
                        response.ResultCode = ResultCode.ResetCodeAlreadyGenerated.ToString();
                        break;
                    case UserMessages.UnexpectedError:
                        response.ResultCode = ResultCode.GenerateResetCodeFailed.ToString();
                        break;
                    default:
                        if (string.IsNullOrEmpty(message))
                        {
                            response.ResultCode = ResultCode.ResetCodeGenerated.ToString();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                response.ResultMessage = UserMessages.GenerateResetCodeFailed;
                response.ResultFlag = false;
                response.ResultCode = ResultCode.GenerateResetCodeFailed.ToString();
                _logger.Log("GenerateResetCode_Exception: ", ex.Message);
            }
            return JsonConvert.SerializeObject(response);
        }

        [EBarAuth]
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