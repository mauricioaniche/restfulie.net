using System;

namespace Restfulie.Server.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsAResource(this Type type)
        {
            return type == typeof (IBehaveAsResource) ||
                   type.GetInterface(typeof (IBehaveAsResource).FullName) != null;
        }

        public static bool IsAListOfResources(this Type type)
        {
            if (type.IsArray && type.GetElementType().IsAResource())
                return true;
            if (type.IsGenericType && type.GetGenericArguments()[0].IsAResource())
                return true;

            return false;
        }
    }
}