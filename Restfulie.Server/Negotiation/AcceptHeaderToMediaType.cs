using System;
using System.Collections.Generic;
using System.Globalization;
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
                var parsedFormat = ParseFormat(type);

                if (IsDefaultFormat(parsedFormat.Format))
                {
                    acceptedMediaType.Add(new QualifiedMediaType(mediaTypes.Default, parsedFormat.Qualifier));
                }
                else
                {
                    var mediaType = mediaTypes.Find(parsedFormat.Format);
                    if (mediaType != null) acceptedMediaType.Add(new QualifiedMediaType(mediaType, parsedFormat.Qualifier));
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
            return type.Equals("*/*");
        }

        private IMediaType MostQualifiedMediaType(IEnumerable<QualifiedMediaType> acceptedMediaType)
        {
            var maxQualifier = acceptedMediaType.Max(m => m.Qualifier);
            return acceptedMediaType.Where(m => m.Qualifier == maxQualifier).First().MediaType;
        }

        private FormatPlusQualifier ParseFormat(string type)
        {
            string format;
            var qualifier = 1.0;

            if (ContainsQualifier(type))
            {
                var typeInfo = type.Split(';');
                format = typeInfo[0].Trim();
                qualifier = Convert.ToDouble(typeInfo[1].Split('=')[1], new CultureInfo("en-US"));
            }
            else
            {
                format = type.Trim();
            }

            return new FormatPlusQualifier(format, qualifier);
        }

        class FormatPlusQualifier
        {
            public string Format { get; private set; }
            public double Qualifier { get; private set; }

            public FormatPlusQualifier(string format, double qualifier)
            {
                Format = format;
                Qualifier = qualifier;
            }
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
