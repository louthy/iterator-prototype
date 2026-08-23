using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype.Internal.Source.Factories;

static class IterableFactory<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<A> Instance;

    static IterableFactory() =>
        Instance = MakeInstance(null);

    public static IteratorSource<A> MakeInstance(IteratorSource? parent)
    {
        if (Ty<A>.IsUnmanaged)
        {
            var ty = typeof(IterableUnmanagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var c  = ty.GetConstructors()
                       .First(c => c.GetParameters().Length           == 1 &&
                                   c.GetParameters()[0].ParameterType == typeof(IteratorSource));
            var i = c.Invoke([parent]);
            return (IteratorSource<A>?)i ?? throw ShouldntHappenException;
        }
        else
        {
            var ty = typeof(IterableManagedSource<,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A));
            var c  = ty.GetConstructors()
                       .First(c => c.GetParameters().Length           == 1 &&
                                   c.GetParameters()[0].ParameterType == typeof(IteratorSource));
            var i = c.Invoke([parent]);
            return (IteratorSource<A>?)i ?? throw ShouldntHappenException;
        }
    }

    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the IterableSource instance");
}

static class IterableFactory<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<B> Instance;
    
    static IterableFactory() =>
        Instance = MakeInstance(null);

    static IteratorSource<B> MakeInstance(IteratorSource? parent)
    {
        switch (Ty<A>.IsUnmanaged, Ty<B>.IsUnmanaged)
        {
            case (true, true):
            {
                var ty = typeof(IterableUnmanagedToUnmanagedSource<,,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A), typeof(B));
                var c  = ty.GetConstructors()
                           .First(c => c.GetParameters().Length           == 1 &&
                                       c.GetParameters()[0].ParameterType == typeof(IteratorSource));
                var i = c.Invoke(null, [parent]);
                return (IteratorSource<B>?)i ?? throw ShouldntHappenException;
            }
            
            case (false, false):
            {
                var ty = typeof(IterableManagedToManagedSource<,,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A), typeof(B));
                var c  = ty.GetConstructors()
                           .First(c => c.GetParameters().Length           == 1 &&
                                       c.GetParameters()[0].ParameterType == typeof(IteratorSource));
                var i = c.Invoke(null, [parent]);
                return (IteratorSource<B>?)i ?? throw ShouldntHappenException;
            }

            case (true, false):
            {
                var ty = typeof(IterableUnmanagedToManagedSource<,,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A), typeof(B));
                var c  = ty.GetConstructors()
                           .First(c => c.GetParameters().Length           == 1 &&
                                       c.GetParameters()[0].ParameterType == typeof(IteratorSource));
                var i = c.Invoke(null, [parent]);
                return (IteratorSource<B>?)i ?? throw ShouldntHappenException;
            }

            case (false, true):
            {
                var ty = typeof(IterableManagedToUnmanagedSource<,,,>).MakeGenericType(typeof(T), typeof(IS), typeof(A), typeof(B));
                var c  = ty.GetConstructors()
                           .First(c => c.GetParameters().Length           == 1 &&
                                       c.GetParameters()[0].ParameterType == typeof(IteratorSource));
                var i = c.Invoke(null, [parent]);
                return (IteratorSource<B>?)i ?? throw ShouldntHappenException;
            }
        }        
    }
    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the IterableSource instance");
}