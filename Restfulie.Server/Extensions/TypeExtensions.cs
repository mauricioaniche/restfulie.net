using System;

namespace Restfulie.Server.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAResource(this Type type)
        {
            return type == typeof(IBehaveAsResource) ||
                   type.GetInterface(typeof(IBehaveAsResource).FullName) != null;
        }
    }
}
