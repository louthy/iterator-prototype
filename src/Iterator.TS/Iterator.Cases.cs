using System.Runtime.CompilerServices;
using IteratorTest.Traits;

namespace IteratorTest;

// Cons structure used to carry the result of consuming the next element
[SkipLocalsInit]
public readonly record struct Cons<T, TS, A>(in A Head, in Iterator<T, TS, A> Tail)
    where T : IterableK<T, TS>
    where TS : struct;
