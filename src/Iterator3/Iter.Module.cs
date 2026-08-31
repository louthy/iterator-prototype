using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static class Iter
{
    /// <summary>
    /// Await
    /// </summary>
    public static IterAwait await = default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static IterPure pure = default;
    
    /// <summary>
    /// Co-routine scope
    /// </summary>
    public static IterScope scope = default;
    
    /// <summary>
    /// Pure
    /// </summary>
    public static IterTake take(int amount) => 
        new (amount);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> from<A>(params ReadOnlySpan<A> ta)
    {
        var array = Arr.create(ta);
        var frame = Iter<A>.Default(out var iter);
        return Push.iterable<Arr, ArrState, A>(ref frame, array)
                   ? iter
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterable<T, IS, A>(ref frame, in ta)
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
    public static Iter<A> lift<A>(in Iter<A> ta)
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterator(ref frame, in ta)
                   ? iter
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<B> apply<A, B>(in Iter<Func<A, B>> tf, in Iter<A> ta)
    {
        throw new NotImplementedException();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second)> product<A, B>(in Iter<A> ta, in Iter<B> tb)
    {
        var frame = ta.Next<A, (A, B)>(out var ta1);
        return Push.iterator(ref frame, in tb) &&
               Push.apply<A, B, (A, B)>(ref frame, static (x, y) => (x, y))
                   ? ta1
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third)> product<A, B, C>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc)
    {
        var frame = ta.Next<A, (A, B, C)>(out var ta1);
        return Push.iterator(ref frame, in ta) &&
               Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.apply<A, B, C, (A, B, C)>(ref frame, static (x, y, z) => (x, y, z))
                   ? ta1
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IterBimap<A, B, C> bimap<A, B, C>(Func<A, B, C> f) =>
        new(f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IterMap<A, B> map<A, B>(Func<A, B> f) =>
        new (f);
}
