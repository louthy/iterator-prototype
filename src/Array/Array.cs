using IteratorTest.Traits;

namespace IteratorTest;

public record Array<A>(A[] Items) 
    : IterableBase<Array, ArrayState, ArrayStateRef, Array<A>, A>;
