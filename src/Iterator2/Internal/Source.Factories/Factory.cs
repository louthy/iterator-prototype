using System.Reflection;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype.Internal.Source.Factories;

static class Factory<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<A> Instance;
    
    static Factory()
    {
        var isUnmanaged = IsUnmanaged(typeof(A));
        if(isUnmanaged)
        {
            var ty = typeof(IteratorUnmanagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var f  = ty.GetFields().First(f => f.Name == "Instance");
            Instance = (IteratorSource<A>?)f.GetValue(null) ?? 
                       throw new InvalidOperationException("IteratorUnmanagedSource<,,> should have a static field named Instance that is of type IteratorSource<A>");
        }
        else
        {
            var ty = typeof(IteratorManagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var f  = ty.GetFields().First(f => f.Name == "Instance");
            Instance = (IteratorSource<A>?)f.GetValue(null) ?? 
                       throw new InvalidOperationException("IteratorManagedSource<,,> should have a static field named Instance that is of type IteratorSource<A>");
        }
    }
    
    public static bool IsUnmanaged(Type type)
    {
        // Unmanaged types must be value types
        if (!type.IsValueType) return false;

        // Primitive types (int, float, bool, etc.) are unmanaged
        if (type.IsPrimitive) return true;

        // Enums are unmanaged if their underlying type is unmanaged
        if (type.IsEnum) 
            return IsUnmanaged(Enum.GetUnderlyingType(type));

        // For structs, all fields must also be unmanaged
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (!IsUnmanaged(field.FieldType)) return false;
        }

        return true;
    }
}