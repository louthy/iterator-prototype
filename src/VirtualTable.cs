using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

// Allows abstracting over the operations of an IterableK
public abstract record VirtualTable<A>
{
    public abstract bool Step(object src, ref Space128 state, out Iterator<A> tail);
}

public record VirtualTable<T, TS, A> : VirtualTable<A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public override bool Step(object src, ref Space128 space, out Iterator<A> tail)
    {
        ref var state = ref Unsafe.As<Space128, TS>(ref space);
        if (T.Step(ref state, out A h))
        {
            var t1 = new Iterator<T, TS, A>(in h, src, in state);
            ref var t2 = ref Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref t1);
            tail = t2;
            return true;
        }
        else
        {
            tail = default!;
            return false;
        }        
    }
}

public static class VirtualTableCache<T, TS, A>
    where T : IterableK<T, TS>
    where TS : struct
{
    public static readonly VirtualTable<A> Cache = new VirtualTable<T, TS, A>();
}