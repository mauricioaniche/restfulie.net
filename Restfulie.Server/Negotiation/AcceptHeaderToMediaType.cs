using System;
using System.Collections.Generic;
using Restfulie.Server.MediaTypes;
using System.Linq;

namespace Restfulie.Server.Negotiation
{
    public class AcceptHeaderToMediaType : IAcceptHeaderToMediaType
    {
        private readonly IMediaTypeList mediaTypes;

        public AcceptHeaderToMediaType(IMediaTypeList mediaTypes)
        {
            this.mediaTypes = mediaTypes;
        }

        public IMediaType GetMediaType(string acceptHeader)
        {
            if (string.IsNullOrEmpty(acceptHeader)) return mediaTypes.Default;

            var types = acceptHeader.Split(',');
            var acceptedMediaType = new List<QualifiedMediaType>();

            foreach(var type in types)
            {
                if (IsDefaultFormat(type))
                {
                    acceptedMediaType.Add(new QualifiedMediaType(mediaTypes.Default, 1));
                }
                else
                {
                    string format;
                    var qualifier = 1.0;

                    if (ContainsQualifier(type))
                    {
                        var typeInfo = type.Split(';');
                        format = typeInfo[0].Trim();
                        qualifier = Convert.ToDouble(typeInfo[1].Split('=')[1]);
                    }
                    else
                    {
                        format = type;
                    }

                    var mediaType = mediaTypes.Find(format);
                    if (mediaType != null) acceptedMediaType.Add(new QualifiedMediaType(mediaType, qualifier));
                }
            }

            if(acceptedMediaType.Count == 0) throw new AcceptHeaderNotSupportedException();
            return MostQualifiedMediaType(acceptedMediaType);
        }

        private bool ContainsQualifier(string type)
        {
            return type.Contains(";");
        }

        private bool IsDefaultFormat(string type)
        {
            return type.Trim().Equals("*/*");
        }

        private IMediaType MostQualifiedMediaType(IEnumerable<QualifiedMediaType> acceptedMediaType)
        {
            var maxQualifier = acceptedMediaType.Max(m => m.Qualifier);
            return acceptedMediaType.Where(m => m.Qualifier == maxQualifier).First().MediaType;
        }

        class QualifiedMediaType
        {
            public IMediaType MediaType { get; private set; }
            public double Qualifier { get; private set; }

            public QualifiedMediaType(IMediaType mediaType, double qualifier)
            {
                MediaType = mediaType;
                Qualifier = qualifier;
            }
        }
    }
}
