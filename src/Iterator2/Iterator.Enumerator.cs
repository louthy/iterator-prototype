using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public struct IteratorEnumerator2<A>
{
    readonly Iterator2<A> reset;
    Iterator2<A> iter;
    A current;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator2(in Iterator2<A> iter)
    {
        this.iter = iter;
        reset = iter;
        current = default!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool MoveNext() =>
        iter.MoveNext(out current);

    public A Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => current;
    }

    public void Reset()
    {
        iter = reset;
        current = default!;
    }
}