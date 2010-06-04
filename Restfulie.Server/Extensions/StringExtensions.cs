using System;
using System.Collections.Generic;
using System.IO;

namespace Restfulie.Server.Extensions
{
    public static class StringExtensions
    {
        public static Stream AsStream(this string xml)
        {
            var byteArray = new List<byte>();
            foreach (var s in xml)
            {
                byteArray.Add(Convert.ToByte(s));
            }

            return new MemoryStream(byteArray.ToArray());
        }
    }
}
