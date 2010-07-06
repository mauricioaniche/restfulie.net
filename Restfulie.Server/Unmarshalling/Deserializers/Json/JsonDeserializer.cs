using System;
using System.Web.Script.Serialization;
using System.Reflection;

namespace Restfulie.Server.Unmarshalling.Deserializers.Json
{
    public class JsonDeserializer : IResourceDeserializer
    {
        public object Deserialize(string content, Type objectType)
        {
            var serializer = new JavaScriptSerializer();

            // How to use reflection to call generic method
            // http://stackoverflow.com/questions/232535/how-to-use-reflection-to-call-generic-method
            MethodInfo method = typeof(JavaScriptSerializer).GetMethod("Deserialize");
            MethodInfo generic = method.MakeGenericMethod(objectType);

            return generic.Invoke(serializer, new object[] { content });
        }
    }
}
