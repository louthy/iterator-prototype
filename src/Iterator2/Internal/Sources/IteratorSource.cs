namespace IteratorPrototype.Internal.Sources;

abstract record IteratorSource(IteratorSource? Next, bool IsUnmanaged)
{
    public abstract bool Run(ref StackFrame frame);
    
    public IteratorSource SetParent(IteratorSource parent) =>
        this with { Next = parent };
}

abstract record  IteratorSource<A>(IteratorSource? Next, bool IsUnmanaged) : IteratorSource(Next, IsUnmanaged)
{
    public abstract bool Run(ref StackFrame frame, out A head);
    public abstract IteratorSource<A> Prepend(A value);
}

abstract record IteratorManagedSource<A>(IteratorSource? Next) : IteratorSource<A>(Next, false)
    where A : class
{
    public override bool Run(ref StackFrame frame, out A head)
    {
        if (Run(ref frame))
        {
            ref var objs = ref frame.Objs; 
            head = objs.Peek<A>();
            objs.Pop();
            return true;
        }
        else
        {
            head = null!;
            return false;
        }
    }
}

abstract record IteratorUnmanagedSource<A>(IteratorSource? Next) : IteratorSource<A>(Next, true)
    where A : unmanaged
{
    public override bool Run(ref StackFrame frame, out A head)
    {
        if (Run(ref frame))
        {
            ref var values = ref frame.Values; 
            head = values.Peek<A>();
            values.Pop();
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }
}