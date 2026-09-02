using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static partial class Iter
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third)> product<A, B, C>(in Iter<(A, B)> tab, in Iter<C> tc)
    {
        var frame = tab.Next<(A, B), (A, B, C)>(out var tabc);
        return Push.iterator(ref frame, in tc) &&
               Push.apply1<A, B, C, (A, B, C)>(ref frame, static (x, y, z) => (x, y, z))
                   ? tabc
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third, D Fourth)> product<A, B, C, D>(in Iter<(A, B, C)> tabc, in Iter<D> td)
    {
        var frame = tabc.Next<(A, B, C), (A, B, C, D)>(out var tabcd);
        return Push.iterator(ref frame, in td) &&
               Push.apply1<A, B, C, D, (A, B, C, D)>(ref frame, static (a, b, c, d) => (a, b, c, d))
                   ? tabcd
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth)> product<A, B, C, D, E>(in Iter<(A, B, C, D)> tabcd, in Iter<E> te)
    {
        var frame = tabcd.Next<(A, B, C, D), (A, B, C, D, E)>(out var tabcde);
        return Push.iterator(ref frame, in te) &&
               Push.apply1<A, B, C, D, E, (A, B, C, D, E)>(ref frame, static (a, b, c, d, e) => (a, b, c, d, e))
                   ? tabcde
                   : default;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> product<A, B, C, D, E, F>(in Iter<(A, B, C, D, E)> tabcde, in Iter<F> tf)
    {
        var frame = tabcde.Next<(A, B, C, D, E), (A, B, C, D, E, F)>(out var tabcdef);
        return Push.iterator(ref frame, in tf) &&
               Push.apply1<A, B, C, D, E, F, (A, B, C, D, E, F)>(ref frame, static (a, b, c, d, e, f) => (a, b, c, d, e, f))
                   ? tabcdef
                   : default;
    }    
}
