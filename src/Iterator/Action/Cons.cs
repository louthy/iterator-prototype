using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<A>(A x, Iterator<A> xs) : IteratorAction<A>
{
    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        head = x;
        stack.Pop();
        stack.PushMany(in Unsafe.As<MiniStack<IteratorFields<A>>, MiniStack<IteratorFields>>(ref Unsafe.AsRef(in xs.fields))); 
        return true;
    }
}
