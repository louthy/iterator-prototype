using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

record SingletonManagedSource<A>(A Head, IteratorSource Next) : IteratorSource<A>(Next, false)
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = Head;
        frame.Source = Next;
        return true;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsManagedSource<A>(value, this); 
}

record SingletonUnmanagedSource<A>(A Head, IteratorSource Next) : IteratorSource<A>(Next, true)
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = Head;
        frame.Source = Next;
        return true;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsUnmanagedSource<A>(value, this); 
}