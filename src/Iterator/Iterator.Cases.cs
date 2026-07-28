namespace IteratorTest;

// Case members must all line-up because they will share the same memory layout.
// References must be of the same type also
public readonly record struct Nil;

// Cons structure used to carry the result of consuming the next element
public readonly record struct Cons<A>(in A Head, in Iterator<A> Tail);
