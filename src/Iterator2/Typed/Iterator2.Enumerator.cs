using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public struct IteratorEnumerator2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    readonly Iterator2<T, IS, A> reset;
    Iterator2<T, IS, A> iter;
    A current;
    
    public IteratorEnumerator2(in Iterator2<T, IS, A> iter)
    {
        this.reset = iter;
        this.iter = iter;
        this.current = default!;
    }
    
    public bool MoveNext()
    {
        ref var fs = ref Unsafe.AsRef(in iter.fields);
        if (fs.action is null)
        {
            ref var s = ref Unsafe.AsRef(in fs.space);
            return T.Next(in fs.ta, ref s, out current);
        }
        else
        {
            ref var a = ref Unsafe.AsRef(in fs.action);
            ref var s = ref Unsafe.AsRef(in fs.space);
            return fs.action.TryGetValue(in fs.ta, ref a, ref s, out current);
        }
    }

    public A Current => 
        current;

    public void Reset()
    {
        iter = reset;
        current = default!;
    }
}