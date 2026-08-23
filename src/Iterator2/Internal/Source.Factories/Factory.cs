using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype.Internal.Source.Factories;

static class Factory<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<A> Instance;

    static Factory()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var ty = typeof(IterableUnmanagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var f  = ty.GetFields().First(f => f.Name == "Instance");
            Instance = (IteratorSource<A>?)f.GetValue(null) ??
                       throw new InvalidOperationException(
                           "IteratorUnmanagedSource<,,> should have a static field named Instance that is of type IteratorSource<A>");
        }
        else
        {
            var ty = typeof(IterableManagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var f  = ty.GetFields().First(f => f.Name == "Instance");
            Instance = (IteratorSource<A>?)f.GetValue(null) ??
                       throw new InvalidOperationException(
                           "IteratorManagedSource<,,> should have a static field named Instance that is of type IteratorSource<A>");
        }
    }
}

static class Factory<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<B> Instance;
    
    static Factory()
    {
        switch (Ty<A>.IsUnmanaged, Ty<B>.IsUnmanaged)
        {
            case (true, true):
            {
                var ty = typeof(IterableUnmanagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(B));
                var f  = ty.GetFields().First(f => f.Name == "Instance");
                Instance = (IteratorSource<B>?)f.GetValue(null) ?? throw ShouldntHappenException;
                break;
            }
            
            case (false, false):
            {
                var ty = typeof(IterableManagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(B));
                var f  = ty.GetFields().First(f => f.Name == "Instance");
                Instance = (IteratorSource<B>?)f.GetValue(null) ?? throw ShouldntHappenException;
                break;
            }

            case (true, false):
            {
                var ty = typeof(IterableUnmanagedToManagedSource<,,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A), typeof(B));
                var f  = ty.GetFields().First(f => f.Name == "Instance");
                Instance = (IteratorSource<B>?)f.GetValue(null) ?? throw ShouldntHappenException;
                break;
            }

            case (false, true):
            {
                var ty = typeof(IterableManagedToUnmanagedSource<,,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A), typeof(B));
                var f  = ty.GetFields().First(f => f.Name == "Instance");
                Instance = (IteratorSource<B>?)f.GetValue(null) ?? throw ShouldntHappenException;
                break;
            }
        }
    }

    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the IterableSource instance");
}