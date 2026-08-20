using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<A>(A Head, IteratorAction<A> Then) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref object ta, ref IteratorAction self, ref Space128 space, out A head)
    {
        head = Head;
        self = Then;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorAction<B> Map<B>(Func<A, B> f) =>
        new MapAction<A, B>(this, f);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorAction<A> Cons(A value) =>
        new ConsAction<A>(value, this);
}
