using System;
using System.Reflection;

namespace BlogsApp.Api.Extensions
{
    /// <summary>
    /// Type extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Is simple type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Is simple type</returns>
        public static bool IsSimple(this Type type)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // If type is nullable, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }

            return
                type.GetTypeInfo().IsPrimitive ||
                type.GetTypeInfo().IsEnum ||
                type == typeof (string) ||
                type == typeof (decimal) ||
                type == typeof (DateTime);
        }
    }
}
