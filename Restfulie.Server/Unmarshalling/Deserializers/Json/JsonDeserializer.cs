using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Restfulie.Server.Unmarshalling.Deserializers.Json
{
    public class JsonDeserializer : IResourceDeserializer
    {
        #region IResourceDeserializer Members
        
        public object Deserialize(string content, Type objectType)
        {
            var serializer = new JsonSerializer();
            var jsonTextReader = new JsonTextReader(new StringReader(content));

            var method = typeof (JsonSerializer).GetMethods().First(m => m.Name == "Deserialize" && m.IsGenericMethod);
            var generic = method.MakeGenericMethod(objectType);

            return generic.Invoke(serializer, new object[] { jsonTextReader });
        }

        #endregion
    }
}