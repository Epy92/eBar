using eBarPortal.Models;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using ViewModels;

namespace eBarPortal.APIHelpers
{
    public class UserSessionApiHelper
    {
        public const string UserSessionApiController = "UserSession";
        private HttpClient _userSessionApiClient;
        public UserSessionApiHelper()
        {
            _userSessionApiClient = new HttpClient()
            {
                BaseAddress = new Uri(AppSettings.WepApiURL)
            };
        }
        public async Task<SessionResponse> Login(string email, string password)
        {
            SessionResponse resp = new SessionResponse();

            UserModel user = new UserModel();
            user.Email = email;
            user.UserPassword = password;

            var uri = new Uri(AppSettings.WepApiURL + UserSessionApiController + "/UserLogin");

           // _userSessionApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var task = _userSessionApiClient.PostAsync(uri, content);
            task.Wait();
            var response = task.Result;
            response.EnsureSuccessStatusCode();
            string responseAsString = await response.Content.ReadAsAsync<string>();


            if (!string.IsNullOrEmpty(responseAsString))
            {
                resp = JsonConvert.DeserializeObject<SessionResponse>(responseAsString);
                return resp;
            }
            return null;
        }

        private object Content(object escapedString, string v)
        {
            throw new NotImplementedException();
        }
    }
}
