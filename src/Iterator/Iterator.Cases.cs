using System.Runtime.CompilerServices;

namespace IteratorPrototype;

// Case members must all line-up because they will share the same memory layout.
// References must be of the same type also
[SkipLocalsInit]
public readonly record struct Nil
{
    public static readonly object Obj = new Nil();
}

/// <summary>
/// Cons structure used to carry the result of consuming the next element
/// </summary>
/// <param name="Head">Consumed head element</param>
/// <param name="Tail">Remaining unconsumed elements</param>
/// <typeparam name="A">Element value type</typeparam>
[SkipLocalsInit]
public readonly record struct Cons<A>(in A Head, in Iterator<A> Tail);
