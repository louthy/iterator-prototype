namespace IteratorPrototype.Internal.VM;

abstract class IteratorVM
{
    public abstract bool Run(ref StackFrame frame);
    public abstract IteratorVM Parent { get; }
}

abstract class IteratorVM<A> : IteratorVM
{
    public abstract bool Run(ref StackFrame frame, out A head);
    public abstract IteratorVM<A> Prepend(A value);
}

abstract class IteratorManagedVM<A> : IteratorVM<A>
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

abstract class IteratorUnmanagedVM<A> : IteratorVM<A>
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