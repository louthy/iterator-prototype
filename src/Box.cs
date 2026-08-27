namespace IteratorPrototype;

public record Box<A>(A Value)
    where A : struct;