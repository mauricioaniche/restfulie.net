namespace Restfulie.Server.Configuration
{
    public class ConfigurationStore
    {
        private static readonly IRestfulieConfiguration Config;

        static ConfigurationStore()
        {
            Config = new RestfulieConfiguration();
        }

        public static IRestfulieConfiguration Get()
        {
            return Config;
        }
    }
}