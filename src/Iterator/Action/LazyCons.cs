using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyConsIteratorAction<A>(A x, Func<Iterator<A>> xs) : IteratorAction<A>
{
    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        head = x;
        var iter = xs();
        stack.PushMany(in Unsafe.As<MiniStack<IteratorFields<A>>, MiniStack<IteratorFields>>(ref Unsafe.AsRef(in iter.fields))); 
        return true;
    }
}
