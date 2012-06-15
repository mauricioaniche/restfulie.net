#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Restfulie.Server.Extensions
{
    public static class StringExtensions
    {
        public static Stream AsStream(this string xml)
        {
            return new MemoryStream(xml.Select(Convert.ToByte).ToArray());
        }
    }
}