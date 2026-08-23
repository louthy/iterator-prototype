using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.VM;

sealed class ConsManagedVM<A>(A Head, IteratorVM Tail) : IteratorManagedVM<A>
    where A : class
{
    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => Tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Objs.Push(Head);
        frame.VM = Tail;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsManagedVM<A>(value, this);
}

sealed class ConsUnmanagedVM<A>(A Head, IteratorVM Tail) : IteratorUnmanagedVM<A>
    where A : unmanaged
{
    public override IteratorVM Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => Tail;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        frame.Values.Push(Head);
        frame.VM = Tail;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsUnmanagedVM<A>(value, this);
}