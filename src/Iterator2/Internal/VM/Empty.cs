using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.VM;

class EmptyIteratorManagedVM<A> : IteratorVM<A>
    where A : class
{
    public static readonly IteratorVM Instance = 
        new EmptyIteratorManagedVM<A>();
    
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
        head = default!;
        return false;
    }

    public override IteratorVM<A> Prepend(A value) =>
        new SingletonManagedVM<A>(value);
}

class EmptyIteratorUnmanagedVM<A> : IteratorVM<A>
    where A : unmanaged
{
    public static readonly IteratorVM Instance = 
        new EmptyIteratorUnmanagedVM<A>();
    
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
        head = default!;
        return false;
    }

    public override IteratorVM<A> Prepend(A value) =>
        new SingletonUnmanagedVM<A>(value);
}