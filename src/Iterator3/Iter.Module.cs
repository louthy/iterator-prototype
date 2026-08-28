using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static class Iter
{
    /// <summary>
    /// Yield
    /// </summary>
    public static IterYield yield = default;
    
    /// <summary>
    /// Await
    /// </summary>
    public static IterAwait await = default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static IterPure pure = default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static IterTake take(int amount) => 
        new (amount);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterable<T, IS, A>(ref frame, ta)
                   ? iter
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> forever<A>(in A head)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.forever(ref frame, in head)
                    ? iter
                    : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> singleton<A>(in A head)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.singleton(ref frame, in head)
                   ? iter
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> pair<A>(in A item1, in A item2) =>
        singleton(in item1) | singleton(in item2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IterMap<A, B> map<A, B>(Func<A, B> f) =>
        new (f);
                    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> product<A>(in Iter<A> tx, in Iter<A> ty)
    {
        var frame = tx.Next(out var tx1);
        return Push.iter(ref frame, in ty)
                   ? tx1
                   : default;
    }
}
