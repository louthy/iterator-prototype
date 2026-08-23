using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.VM;

class SingletonManagedVM<A>(A Head) : IteratorVM<A>
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = Head;
        frame.VM = EmptyIteratorManagedVM<A>.Instance;
        return true;
    }

    public override IteratorVM<A> Prepend(A value) =>
        new ConsManagedVM<A>(value, this); 
}

class SingletonUnmanagedVM<A>(A Head) : IteratorVM<A>
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = Head;
        frame.VM = EmptyIteratorUnmanagedVM<A>.Instance;
        return true;
    }

    public override IteratorVM<A> Prepend(A value) =>
        new ConsUnmanagedVM<A>(value, this); 
}