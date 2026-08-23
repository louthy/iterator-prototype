using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

sealed record ConsManagedSource<A>(A Head, IteratorSource? Parent) : IteratorManagedSource<A>(Parent)
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Objs.Push(Head);
        frame.Source = Parent;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsManagedSource<A>(value, this);
}

sealed record ConsUnmanagedSource<A>(A Head, IteratorSource? Parent) : IteratorUnmanagedSource<A>(Parent)
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Values.Push(Head);
        frame.Source = Parent;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsUnmanagedSource<A>(value, this);
}