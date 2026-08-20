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
        this.reset = iter;
        this.iter = iter;
        this.current = default!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool MoveNext()
    {
        ref var fs = ref Unsafe.AsRef(in iter.fields);
        ref var ta = ref Unsafe.AsRef(in fs.ta);
        ref var a  = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in fs.action));
        ref var s  = ref Unsafe.AsRef(in fs.space);
        return fs.action.TryGetValue(ref ta, ref a, ref s, out current);
    }

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