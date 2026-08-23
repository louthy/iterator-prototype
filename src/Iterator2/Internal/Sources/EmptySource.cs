using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Sources;

class EmptyIteratorManagedSource<A> : IteratorSource<A>
    where A : class
{
    public static readonly IteratorSource Instance = 
        new EmptyIteratorManagedSource<A>();
    
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
        head = default!;
        return false;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new SingletonManagedSource<A>(value);
}

class EmptyIteratorUnmanagedSource<A> : IteratorSource<A>
    where A : unmanaged
{
    public static readonly IteratorSource Instance = 
        new EmptyIteratorUnmanagedSource<A>();
    
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
        head = default!;
        return false;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new SingletonUnmanagedSource<A>(value);
}