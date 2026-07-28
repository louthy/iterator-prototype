using System.Runtime.CompilerServices;
using IteratorTest.Traits;
using LanguageExt.Traits;

namespace IteratorTest;

// Allows abstracting over the operations of an IterableK
public abstract record VirtualTable<A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public abstract bool Step(object src, in Space128 state, out Iterator<A> tail);
}

public record VirtualTable<T, TS, A> : VirtualTable<A>
    where T : IterableK<T, TS>
    where TS : struct
{

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public override bool Step(object src, in Space128 space, out Iterator<A> tail)
    {
        ref var          ta    = ref Unsafe.As<object, K<T, A>>(ref src);
        ref readonly var state = ref Unsafe.As<Space128, TS>(ref Unsafe.AsRef(in space));
        if (Step(ta, in state, out var nt))
        {
            tail = Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref nt);
            return true;
        }
        else
        {
            tail = default!;
            return false;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    bool Step(K<T, A> ta, in TS state, out Iterator<T, TS, A> tail)
    {
        tail = new Iterator<T, TS, A>(ta, in state, out IteratorTag tg);
        return tg == IteratorTag.IterableK;
    }
}

public static class VirtualTableCache<T, TS, A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public static readonly VirtualTable<A> Cache = new VirtualTable<T, TS, A>();
}