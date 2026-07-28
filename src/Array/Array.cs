using IteratorTest.Traits;

namespace IteratorTest;

public record Array<A>(A[] Items) 
    : IterableBase<Array, ArrayState, Array<A>, A>;
