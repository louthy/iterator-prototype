using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<T, IS, A>(A Head, IteratorAction<T, IS, A> Then) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public bool TryGetValue(in object ta, ref IteratorAction self, ref Space128 space, out A head)
    {
        head = Head;
        self = Then;
        return true;
    }

    public bool TryGetValue(in K<T, A> ta, ref IteratorAction<T, IS, A> self, ref IS space, out A head)
    {
        head = Head;
        self = Then;
        return true;
    }

    public IteratorAction<B> Map<B>(Func<A, B> f) =>
        new MapAction<T, IS, A, B>(this, f);

    public IteratorAction<T, IS, A> Cons(A value) =>
        new ConsAction<T, IS, A>(value, this);
}
