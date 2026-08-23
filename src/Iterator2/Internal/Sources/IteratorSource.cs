namespace IteratorPrototype.Internal.Sources;

abstract record IteratorSource(IteratorSource? Parent, bool IsUnmanaged)
{
    public abstract bool Run(ref StackFrame frame);
    
    public IteratorSource SetParent(IteratorSource parent) =>
        this with { Parent = parent };
}

abstract record  IteratorSource<A>(IteratorSource? Parent, bool IsUnmanaged) : IteratorSource(Parent, IsUnmanaged)
{
    public abstract bool Run(ref StackFrame frame, out A head);
    public abstract IteratorSource<A> Prepend(A value);
}

abstract record IteratorManagedSource<A>(IteratorSource? Parent) : IteratorSource<A>(Parent, false)
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

abstract record IteratorUnmanagedSource<A>(IteratorSource? Parent) : IteratorSource<A>(Parent, true)
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