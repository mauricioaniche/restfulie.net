using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaType
    {
        string[] Synonyms { get; }
        IDriver Driver { get; set; }
        IResourceMarshaller BuildMarshaller();
        IResourceUnmarshaller BuildUnmarshaller();
    }

    public abstract class MediaType : IMediaType
    {
        #region IMediaType Members

        public abstract string[] Synonyms { get; }

        public abstract IResourceMarshaller BuildMarshaller();

        public abstract IResourceUnmarshaller BuildUnmarshaller();

        public IDriver Driver { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            return GetType() == obj.GetType();
        }
    }
}