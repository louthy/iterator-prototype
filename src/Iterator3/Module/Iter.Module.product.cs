using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second)> product<A, B>(in Iter<A> ta, in Iter<B> tb)
    {
        var frame = ta.Next<A, (A, B)>(out var tab);
        return Push.iterator(ref frame, in tb) &&
               Push.apply<A, B, (A, B)>(ref frame, static (x, y) => (x, y))
                   ? tab
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third)> product<A, B, C>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc)
    {
        var frame = ta.Next<A, (A, B, C)>(out var tabc);
        return Push.iterator(ref frame, in ta) &&
               Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.apply<A, B, C, (A, B, C)>(ref frame, static (x, y, z) => (x, y, z))
                   ? tabc
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third, D Fourth)> product<A, B, C, D>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td)
    {
        var frame = ta.Next<A, (A, B, C, D)>(out var tabcd);
        return Push.iterator(ref frame, in ta) &&
               Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) &&
               Push.apply<A, B, C, D, (A, B, C, D)>(ref frame, static (a, b, c, d) => (a, b, c, d))
                   ? tabcd
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth)> product<A, B, C, D, E>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te)
    {
        var frame = ta.Next<A, (A, B, C, D, E)>(out var tabcde);
        return Push.iterator(ref frame, in ta) &&
               Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) &&
               Push.iterator(ref frame, in te) &&
               Push.apply<A, B, C, D, E, (A, B, C, D, E)>(ref frame, static (a, b, c, d, e) => (a, b, c, d, e))
                   ? tabcde
                   : default;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> product<A, B, C, D, E, F>(in Iter<A> ta, in Iter<B> tb, in Iter<C> tc, in Iter<D> td, in Iter<E> te, in Iter<F> tf)
    {
        var frame = ta.Next<A, (A, B, C, D, E, F)>(out var tabcdef);
        return Push.iterator(ref frame, in ta) &&
               Push.iterator(ref frame, in tb) &&
               Push.iterator(ref frame, in tc) &&
               Push.iterator(ref frame, in td) &&
               Push.iterator(ref frame, in te) &&
               Push.iterator(ref frame, in tf) &&
               Push.apply<A, B, C, D, E, F, (A, B, C, D, E, F)>(ref frame, static (a, b, c, d, e, f) => (a, b, c, d, e, f))
                   ? tabcdef
                   : default;
    }
}
