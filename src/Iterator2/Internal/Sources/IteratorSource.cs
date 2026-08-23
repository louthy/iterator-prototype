namespace IteratorPrototype.Internal.Sources;

abstract record IteratorSource(IteratorSource? Next, LE.Unit Dummy)
{
    public abstract bool Run(ref StackFrame frame);
    
    public IteratorSource SetParent(IteratorSource parent) =>
        this with { Next = parent };
}

abstract record IteratorSource<A>(IteratorSource? Next) : IteratorSource(Next, default)
{
    public abstract IteratorSource<A> Prepend(A value);
}
