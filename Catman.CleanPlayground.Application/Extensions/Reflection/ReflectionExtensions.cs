namespace Catman.CleanPlayground.Application.Extensions.Reflection
{
    using System;
    using System.Linq;

    internal static class ReflectionExtensions
    {
        public static bool Inherits(this Type type, Type possibleBaseType)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            while (true)
            {
                var currentTypeDefinition = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (currentTypeDefinition == possibleBaseType)
                {
                    return true;
                }

                if (type.BaseType is null)
                {
                    return false;
                }
                type = type.BaseType;
                
            }
        }

        public static Type GetImplementedGenericInterface(this Type type, Type genericInterfaceTypeDefinition)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type
                .GetInterfaces()
                .First(implemented => genericInterfaceTypeDefinition == implemented.GetGenericTypeDefinition());
        }
    }
}
