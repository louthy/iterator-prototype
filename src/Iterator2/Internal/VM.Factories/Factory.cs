using System.Reflection;

namespace IteratorPrototype.Internal.VM;

static class Factory<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorVM<A> Instance;
    
    static Factory()
    {
        var isUnmanaged = IsUnmanaged(typeof(A));
        if(isUnmanaged)
        {
            var ty = typeof(IteratorUnmanagedVM<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var f  = ty.GetFields().First(f => f.Name == "Instance");
            Instance = (IteratorVM<A>?)f.GetValue(null) ?? throw new InvalidOperationException("IteratorUnmanagedVM<,,> should have a static field named Instance that is of type IteratorVM<A>");
        }
        else
        {
            var ty = typeof(IteratorManagedVM<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var f  = ty.GetFields().First(f => f.Name == "Instance");
            Instance = (IteratorVM<A>?)f.GetValue(null) ?? throw new InvalidOperationException("IteratorManagedVM<,,> should have a static field named Instance that is of type IteratorVM<A>");
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