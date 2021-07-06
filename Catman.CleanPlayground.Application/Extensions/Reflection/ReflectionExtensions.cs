namespace Catman.CleanPlayground.Application.Extensions.Reflection
{
    using System;
    using System.Linq;

    internal static class ReflectionExtensions
    {
        public static bool ImplementsGenericInterface(this Type type, Type interfaceGenericTypeDefinition)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type
                .GetInterfaces()
                .Any(@interface => @interface.IsGenericType &&
                                   @interface.GetGenericTypeDefinition() == interfaceGenericTypeDefinition);
        }

        public static Type GetImplementedGenericInterface(this Type type, Type interfaceGenericTypeDefinition)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type
                .GetInterfaces()
                .First(@interface => interfaceGenericTypeDefinition == @interface.GetGenericTypeDefinition());
        }
    }
}
