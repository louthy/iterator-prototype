using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

class SingletonManagedSource<A>(A Head) : IteratorSource<A>
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override IteratorSource Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = Head;
        frame.Source = EmptyIteratorManagedSource<A>.Instance;
        return true;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsManagedSource<A>(value, this); 
}

class SingletonUnmanagedSource<A>(A Head) : IteratorSource<A>
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override IteratorSource Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => this;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = Head;
        frame.Source = EmptyIteratorUnmanagedSource<A>.Instance;
        return true;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsUnmanagedSource<A>(value, this); 
}