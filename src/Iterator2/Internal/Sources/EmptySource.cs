using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

record EmptyIteratorManagedSource<A>(IteratorSource? Next) : IteratorSource<A>(Next, false)
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = default!;
        return false;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new SingletonManagedSource<A>(value, this);
}

record EmptyIteratorUnmanagedSource<A>(IteratorSource? Next) : IteratorSource<A>(Next, true)
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        return false;
    }

    public override bool Run(ref StackFrame frame, out A head)
    {
        head = default!;
        return false;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new SingletonUnmanagedSource<A>(value, this);
}