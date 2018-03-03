using eBarService.DatabaseOperations;
using Newtonsoft.Json;

namespace eBarService
{
    public class ServiceEbar : IServiceEbar
    {
        public string Register(UserTbl userRegister)
        {
            return new UserOperations().RegisterUser(userRegister);   
        }

        public string UserLogin(UserTbl userLogin)
        {
           return JsonConvert.SerializeObject(new UserOperations().IsUserValid(userLogin.Username ?? userLogin.Email, userLogin.UserPassword));
        }
    }
}
