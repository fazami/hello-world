namespace Farzin.Infrastructure.CrossCutting.AppConfig
{
    public static class Settings
    {
        public static string LogFile
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LogFile"];
            }
        }

    }
}
