namespace IteratorPrototype.Internal.Sources;

abstract class IteratorSource
{
    public abstract bool Run(ref StackFrame frame);
    public abstract IteratorSource Parent { get; }
}

abstract class IteratorSource<A> : IteratorSource
{
    public abstract bool Run(ref StackFrame frame, out A head);
    public abstract IteratorSource<A> Prepend(A value);
}

abstract class IteratorManagedSource<A> : IteratorSource<A>
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

abstract class IteratorUnmanagedSource<A> : IteratorSource<A>
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