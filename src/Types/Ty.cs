using System.Reflection;

namespace IteratorPrototype.Types;

public static class Ty<A>
{
    public static readonly bool IsManaged;
    public static readonly bool IsUnmanaged;
    public static readonly bool IsValue;
    public static readonly bool IsDisposable;

    static Ty()
    {
        IsUnmanaged = IsTypeUnmanaged(typeof(A));
        IsValue = typeof(A).IsValueType;
        IsManaged = !IsValue && !IsUnmanaged;
        IsDisposable = typeof(IDisposable).IsAssignableFrom(typeof(A));
    }

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