using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public class AcceptHeaderToMediaType : IAcceptHeaderToMediaType
    {
        private readonly IMediaTypeList mediaTypes;

        public AcceptHeaderToMediaType(IMediaTypeList mediaTypes)
        {
            this.mediaTypes = mediaTypes;
        }

        #region IAcceptHeaderToMediaType Members

        public IMediaType GetMediaType(string acceptHeader)
        {
            if (string.IsNullOrEmpty(acceptHeader))
                return mediaTypes.Default;

            var types = acceptHeader.Split(',');
            var acceptedMediaType = new List<QualifiedMediaType>();

            foreach (var formatedMediaType in types.Select(ParseFormat))
                if (IsDefaultFormat(formatedMediaType.Format))
                    acceptedMediaType.Add(new QualifiedMediaType(mediaTypes.Default, formatedMediaType.Qualifier));
                else
                {
                    var mediaType = mediaTypes.Find(formatedMediaType.Format);
                    if (mediaType != null)
                        acceptedMediaType.Add(new QualifiedMediaType(mediaType, formatedMediaType.Qualifier));
                }

            if (acceptedMediaType.Count == 0)
                throw new AcceptHeaderNotSupportedException();
            return MostQualifiedMediaType(acceptedMediaType);
        }

        #endregion

        private static bool IsDefaultFormat(string type)
        {
            return type.Equals("*/*");
        }

        private static IMediaType MostQualifiedMediaType(IEnumerable<QualifiedMediaType> acceptedMediaType)
        {
            var maxQualifier = acceptedMediaType.Max(m => m.Qualifier);
            return acceptedMediaType.First(m => m.Qualifier == maxQualifier).MediaType;
        }

        private static FormatPlusQualifier ParseFormat(string type)
        {
            var qualifier = 1.0;

            const string strRegex = @"[^;]+;\s*q\s*=\s*(?<q>[\d.]+)";
            var match = Regex.Match(type, strRegex);

            if (match.Groups["q"].Success)
                qualifier = Convert.ToDouble(match.Groups["q"].Value, new CultureInfo("en-US"));

            var typeInfo = type.Split(';');
            var format = typeInfo[0].Trim();

            return new FormatPlusQualifier(format, qualifier);
        }

        #region Nested type: FormatPlusQualifier

        private class FormatPlusQualifier
        {
            public FormatPlusQualifier(string format, double qualifier)
            {
                Format = format;
                Qualifier = qualifier;
            }

            public string Format { get; private set; }
            public double Qualifier { get; private set; }
        }

        #endregion

        #region Nested type: QualifiedMediaType

        private class QualifiedMediaType
        {
            public QualifiedMediaType(IMediaType mediaType, double qualifier)
            {
                MediaType = mediaType;
                Qualifier = qualifier;
            }

            public IMediaType MediaType { get; private set; }
            public double Qualifier { get; private set; }
        }

        #endregion
    }
}