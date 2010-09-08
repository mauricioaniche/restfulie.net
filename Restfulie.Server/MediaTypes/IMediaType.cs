using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
	public interface IMediaType
	{
		string[] Synonyms { get; }
		IResourceMarshaller BuildMarshaller();
		IResourceUnmarshaller BuildUnmarshaller();
		IDriver Driver { get; set; }
	}

	public abstract class MediaType : IMediaType
	{
		public abstract string[] Synonyms { get; }

		public abstract IResourceMarshaller BuildMarshaller();

		public abstract IResourceUnmarshaller BuildUnmarshaller();


		public IDriver Driver { get; set; }

		public override bool Equals(object obj)
		{
			return GetType() == obj.GetType();
		}
	}
}
