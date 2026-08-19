using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<T, IS, A, B>(B Head, IteratorAction<T, IS, A, B> Then) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public bool TryGetValue(in object ta, ref IteratorAction self, ref Space128 space, out B head)
    {
        head = Head;
        self = Then;
        return true;
    }

    public bool TryGetValue(in K<T, A> ta, ref IteratorAction<T, IS, A, B> self, ref IS space, out B head)
    {
        head = Head;
        self = Then;
        return true;
    }

    public IteratorAction<C> Map<C>(Func<B, C> f) =>
        new MapAction<T, IS, B, C>(this, f);

    public IteratorAction<B> Cons(B value) =>
        new ConsAction<T, IS, A, B>(value, this);
}

