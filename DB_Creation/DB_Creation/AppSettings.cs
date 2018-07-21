using System.Configuration;

namespace DB_Creation
{
    static class AppSettings
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.AppSettings["ConnectionString"]; }
        }

        public static string DatabaseScriptsDirectory
        {
            get { return ConfigurationManager.AppSettings["DatabaseScriptsDirectory"]; }
        }

        public static string TestDataScriptsDirectory
        {
            get { return ConfigurationManager.AppSettings["TestDataScriptsDirectory"]; }
        }

        public static string DatabaseName
        {
            get { return ConfigurationManager.AppSettings["DatabaseName"]; }
        }
    }
}
