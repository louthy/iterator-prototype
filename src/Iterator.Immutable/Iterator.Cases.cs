using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

// Cons structure used to carry the result of consuming the next element
[SkipLocalsInit]
public readonly record struct Cons<T, IS, A>(in A Head, in Iterator<T, IS, A> Tail)
    where T : IterableImmutable<T, IS>
    where IS : struct;
