namespace IteratorPrototype.Iterator3;

public readonly struct IterYield;
public readonly struct IterAwait;
public readonly struct IterPure;
public readonly record struct IterMap<A, B>(Func<A, B> f);
