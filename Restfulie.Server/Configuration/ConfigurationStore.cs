namespace Restfulie.Server.Configuration
{
    public class ConfigurationStore
    {
        private static IRestfulieConfiguration config;

        static ConfigurationStore()
        {
            config = new RestfulieConfiguration();
        }

        public static void Save(IRestfulieConfiguration newConfig)
        {
            config = newConfig;
        }

        public static IRestfulieConfiguration Get()
        {
            return config;
        }
    }
}
