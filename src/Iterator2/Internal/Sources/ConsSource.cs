using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

sealed class ConsManagedSource<A>(A Head, IteratorSource Tail) : IteratorManagedSource<A>
    where A : class
{
    public override IteratorSource Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => Tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Objs.Push(Head);
        frame.Source = Tail;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsManagedSource<A>(value, this);
}

sealed class ConsUnmanagedSource<A>(A Head, IteratorSource Tail) : IteratorUnmanagedSource<A>
    where A : unmanaged
{
    public override IteratorSource Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => Tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Values.Push(Head);
        frame.Source = Tail;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsUnmanagedSource<A>(value, this);
}