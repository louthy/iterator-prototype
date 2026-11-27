#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
using IteratorPrototype.Types;

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
            throw new Exception($"We have a type {Ty<A>.Pretty} that apparently isn't managed, unmanaged, or a value-type!");
        }
    }

    public static unsafe IterOp iterable<T, IS>() 
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        Instance.IterableImpl<T, IS>();

    public static unsafe IterOp iterator => 
        Instance.IteratorImpl;

    public static unsafe IterOp iteratorIndexed => 
        Instance.IteratorIndexedImpl;

    public static unsafe IterOp lazyIterator => 
        Instance.LazyIteratorImpl;
 
    public static unsafe IterOp ilist => 
        Instance.IListImpl;

    public abstract unsafe IterOp IterableImpl<T, IS>()
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged;

    public abstract unsafe IterOp IteratorImpl { get; }

    public abstract unsafe IterOp IteratorIndexedImpl { get; }

    public abstract unsafe IterOp LazyIteratorImpl { get; }

    public abstract unsafe IterOp IListImpl { get; }
}

class ManagedPull<A> : PullGen<A>
    where A : class
{
    static ManagedPull() =>
        Instance = new ManagedPull<A>();

    public override unsafe IterOp IterableImpl<T, IS>() =>
        &Pull.iterableManaged<T, IS, A>;

    public override unsafe IterOp IteratorImpl =>
        &Pull.iteratorManaged<A>;

    public override unsafe IterOp IteratorIndexedImpl =>
        &Pull.iteratorIndexedManaged<A>;

    public override unsafe IterOp LazyIteratorImpl =>
        &Pull.lazyIteratorManaged<A>;

    public override unsafe IterOp IListImpl =>
        &Pull.ilistManaged<A>;
}

class UnmanagedPull<A> : PullGen<A>
    where A : unmanaged
{
    static UnmanagedPull() =>
        Instance = new UnmanagedPull<A>();
    
    public override unsafe IterOp IterableImpl<T, IS>() =>
        &Pull.iterableUnmanaged<T, IS, A>;

    public override unsafe IterOp IteratorImpl =>
        &Pull.iteratorUnmanaged<A>;

    public override unsafe IterOp IteratorIndexedImpl =>
        &Pull.iteratorIndexedUnmanaged<A>;

    public override unsafe IterOp LazyIteratorImpl =>
        &Pull.lazyIteratorUnmanaged<A>;

    public override unsafe IterOp IListImpl =>
        &Pull.ilistUnmanaged<A>;
}

class StructPull<A> : PullGen<A>
    where A : struct
{
    static StructPull() =>
        Instance = new StructPull<A>();

    public override unsafe IterOp IterableImpl<T, IS>() =>
        &Pull.iterableStruct<T, IS, A>;

    public override unsafe IterOp IteratorImpl =>
        &Pull.iteratorStruct<A>;

    public override unsafe IterOp IteratorIndexedImpl =>
        &Pull.iteratorIndexedStruct<A>;

    public override unsafe IterOp LazyIteratorImpl =>
        &Pull.lazyIteratorStruct<A>;

    public override unsafe IterOp IListImpl =>
        &Pull.ilistStruct<A>;
}
