#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

abstract class PullGen<A>
{
    internal static PullGen<A> Instance;

    [MethodImpl(Optimisations.Default)]
    static PullGen()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedPull<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else if (Ty<A>.IsValue)
        {
            var type = typeof(StructPull<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else if (Ty<A>.IsManaged)
        {
            var type = typeof(ManagedPull<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            throw new Exception("We have a type {typeof(Ty).Name} that apparently isn't managed, unmanaged, or a value-type!");
        }
    }

    public static unsafe IterOp bimap1<X, Y>() => 
        Instance.BiMapImpl<X, Y>();

    public static unsafe IterOp iterable<T, IS>() 
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        Instance.IterableImpl<T, IS>();

    public static unsafe IterOp iterator => 
        Instance.IteratorImpl;

    public abstract unsafe IterOp BiMapImpl<X, Y>();
 
    public abstract unsafe IterOp IterableImpl<T, IS>()
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged;

    public abstract unsafe IterOp IteratorImpl { get; }
}

class ManagedPull<A> : PullGen<A>
    where A : class
{
    static ManagedPull() =>
        Instance = new ManagedPull<A>();

    public override unsafe IterOp BiMapImpl<X, Y>() =>
        &Pull.bimapManaged1<X, Y, A>;

    public override unsafe IterOp IterableImpl<T, IS>() =>
        &Pull.iterableManaged<T, IS, A>;

    public override unsafe IterOp IteratorImpl =>
        &Pull.iteratorManaged<A>;
}

class UnmanagedPull<A> : PullGen<A>
    where A : unmanaged
{
    static UnmanagedPull() =>
        Instance = new UnmanagedPull<A>();

    public override unsafe IterOp BiMapImpl<X, Y>() =>
        &Pull.bimapUnmanaged1<X, Y, A>;
    
    public override unsafe IterOp IterableImpl<T, IS>() =>
        &Pull.iterableUnmanaged<T, IS, A>;

    public override unsafe IterOp IteratorImpl =>
        &Pull.iteratorUnmanaged<A>;
}

class StructPull<A> : PullGen<A>
    where A : struct
{
    static StructPull() =>
        Instance = new StructPull<A>();

    public override unsafe IterOp BiMapImpl<X, Y>() =>
        &Pull.bimapStruct1<X, Y, A>;

    public override unsafe IterOp IterableImpl<T, IS>() =>
        &Pull.iterableStruct<T, IS, A>;

    public override unsafe IterOp IteratorImpl =>
        &Pull.iteratorStruct<A>;
}
