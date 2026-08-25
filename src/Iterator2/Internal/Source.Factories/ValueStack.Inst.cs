#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;

namespace IteratorPrototype.Internal.Source.Factories;

abstract class ValueStack<A>
{
    protected static ValueStack<A> Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Pop(ref OpFrame frame, out A top) =>
        Instance.PopImpl(ref frame, out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Pop(ref StackFrame stack, out A top) =>
        Instance.PopImpl(ref stack.frame, out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Push(ref OpFrame frame, in A top) =>
        Instance.PushImpl(ref frame, in top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Push(ref StackFrame stack, in A top) =>
        Instance.PushImpl(ref stack.frame, in top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static ValueStack()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedValueStack<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            var type = typeof(ManagedValueStack<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }

    protected abstract bool PopImpl(ref OpFrame frame, out A top);
    protected abstract bool PushImpl(ref OpFrame frame, in A top);

    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the ValueStack instance");    
}

class ManagedValueStack<A> : ValueStack<A>
    where A : class
{
    static ManagedValueStack() =>
        Instance = new ManagedValueStack<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    protected override bool PopImpl(ref OpFrame frame, out A top) =>
        frame.objs.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    protected override bool PushImpl(ref OpFrame frame, in A top) =>
        frame.objs.Push(top);
}

class UnmanagedValueStack<A> : ValueStack<A>
    where A : unmanaged
{
    static UnmanagedValueStack() =>
        Instance = new UnmanagedValueStack<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    protected override bool PopImpl(ref OpFrame frame, out A top) =>
        frame.values.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    protected override bool PushImpl(ref OpFrame frame, in A top) =>
        frame.values.Push(top);
}