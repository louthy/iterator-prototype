using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

// Cons structure used to carry the result of consuming the next element
[SkipLocalsInit]
public readonly record struct Cons<TA, IS, A>(in A Head, in Iterator<TA, IS, A> Tail)
    where TA : class, IterableImmutable<TA, IS, A>
    where IS : struct;
