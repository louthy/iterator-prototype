using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public struct IteratorEnumerator<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    readonly Iterator<T, IS, A> reset;
    Iterator<T, IS, A> iter;
    A current;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator(in Iterator<T, IS, A> iter)
    {
        this.reset = iter;
        this.iter = iter;
        this.current = default!;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool MoveNext()
    {
        ref var fs = ref Unsafe.AsRef(in iter.fields);
        ref var ta = ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in fs.ta));
        if (fs.action is null)
        {
            ref var s = ref Unsafe.AsRef(in fs.space);
            return T.Next(in fs.ta, ref s, out current);
        }
        else
        {
            ref var a     = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in fs.action));
            ref var s     = ref Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fs.space));
            var     stack = new IteratorStack(ref ta, ref a, ref s);
            return fs.action.TryGetValue(ref stack, out current);
        }
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