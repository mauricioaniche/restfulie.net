namespace Restfulie.Server.Marshalling
{
    // used when html is the media type and we need to let asp.net mvc work!
    public class AspNetMvcMarshaller : IResourceMarshaller
    {
        public string Build(object resource)
        {
            return string.Empty;
        }
    }
}
