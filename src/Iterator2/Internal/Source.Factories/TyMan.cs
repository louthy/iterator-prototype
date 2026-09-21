using System.Reflection;

namespace IteratorPrototype.Internal.Source.Factories;

public static class TyMan<A>
{
    public static readonly bool IsUnmanaged = IsTypeUnmanaged(typeof(A));

    static bool IsTypeUnmanaged(Type type)
    {
        while (true)
        {
            // Unmanaged types must be value types
            if (!type.IsValueType) return false;

            // Primitive types (int, float, bool, etc.) are unmanaged
            if (type.IsPrimitive) return true;

            // Enums are unmanaged if their underlying type is unmanaged
            if (type.IsEnum)
            {
                type = Enum.GetUnderlyingType(type);
                continue;
            }

            // For structs, all fields must also be unmanaged
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (!IsTypeUnmanaged(field.FieldType)) return false;
            }

            return true;
        }
    }
}