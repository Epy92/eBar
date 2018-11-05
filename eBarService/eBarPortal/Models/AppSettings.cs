using Microsoft.Extensions.Configuration;

namespace eBarPortal.Models
{
    public static class AppSettings
    {
        public static string WepApiURL;

        public static void ReadAppSettings(IConfiguration config)
        {
            WepApiURL = config.GetSection("AppSettings")["WepApiURL"];
        }
    }
}
