using System.Reflection;

namespace IteratorPrototype.Types;

public enum TyFlavour
{
    Managed = 1,
    Unmanaged = 2,
    Struct = 3
}

public static class Ty<A>
{
    public static readonly TyFlavour Flavour;
    public static readonly bool IsManaged;
    public static readonly bool IsUnmanaged;
    public static readonly bool IsValue;
    public static readonly string Pretty;

    static Ty()
    {

        var typeA = typeof(A);
        Pretty = TyPretty.Make(typeA);
        IsUnmanaged = IsTypeUnmanaged(typeA);
        IsValue = typeA.IsValueType;
        IsManaged = !IsValue && !IsUnmanaged;
        Flavour = IsUnmanaged 
                      ? TyFlavour.Unmanaged 
                      : IsValue 
                          ? TyFlavour.Struct 
                          : TyFlavour.Managed;
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
