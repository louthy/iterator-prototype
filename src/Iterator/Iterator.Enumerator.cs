using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public struct IteratorEnumerator<A>
{
    readonly Iterator<A> reset;
    Iterator<A> iter;
    A current;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator(in Iterator<A> iter)
    {
        this.reset = iter;
        this.iter = iter;
        this.current = default!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool MoveNext() =>
        iter.TryGetValue(out current, out iter);

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