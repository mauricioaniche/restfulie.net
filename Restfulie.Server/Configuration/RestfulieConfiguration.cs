using System;
using System.Collections.Generic;
using Restfulie.Server.MediaTypes;
using System.Linq;

namespace Restfulie.Server.Configuration
{
    public class RestfulieConfiguration : IRestfulieConfiguration
    {
        private readonly IList<IMediaType> store;

        public RestfulieConfiguration()
        {
            store = new List<IMediaType>
                        {
                            new XmlAndHypermedia(),
                            new AtomPlusXml(),
                            new JsonAndHypermedia(),
                            new HTML()
                        };
        }

        public void Register<T>(IDriver driver) 
            where T : IMediaType 
        {
            var media = FindOrCreate(typeof (T));
            media.Driver = driver;
        }

        public IMediaTypeList MediaTypes
        {
            get { return new DefaultMediaTypeList(store, new HTML()); }
        }

        public void RegisterVendorized(string format, IDriver driver)
        {
            var vendorizedMediaType = new Vendorized(format)
                                          {
                                              Driver = driver
                                          };

            store.Add(vendorizedMediaType);
        }

        private IMediaType FindOrCreate(Type mediaType)
        {
            var searchedMediaType = store.Where(mt => mt.GetType() == mediaType).SingleOrDefault();
            if (searchedMediaType == null)
            {
                searchedMediaType = (IMediaType)Activator.CreateInstance(mediaType);
                store.Add(searchedMediaType);
            }

            return searchedMediaType;
        }
    }
}
