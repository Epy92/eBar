using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ViewModels;

namespace eBarPortal.Extensions
{
    public static class SessionExtensions
    {
        public static void SetUserObject(this ISession session, string key, UserModel value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static UserModel GetUserObject<UserModel>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(UserModel) : JsonConvert.DeserializeObject<UserModel>(value);
        }
    }
}
