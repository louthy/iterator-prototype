#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;

namespace IteratorPrototype.Internal.Source.Factories;

abstract class ValueStack<A>
{
    protected static ValueStack<A> Instance;

    [MethodImpl(Optimisations.Default)]
    public static bool Pop(ref OpFrame frame, out A top) =>
        Instance.PopImpl(ref frame, out top);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Pop(ref StackFrame stack, out A top) =>
        Instance.PopImpl(ref stack.frame, out top);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Pop(ref OpFrame frame) =>
        Instance.PopImpl(ref frame);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Pop(ref StackFrame stack) =>
        Instance.PopImpl(ref stack.frame);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Push(ref OpFrame frame, in A top) =>
        Instance.PushImpl(ref frame, in top);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Push(ref StackFrame stack, in A top) =>
        Instance.PushImpl(ref stack.frame, in top);
    
    [MethodImpl(Optimisations.Default)]
    public static ref A Peek(ref OpFrame frame) =>
        ref Instance.PeekImpl(ref frame);
    
    [MethodImpl(Optimisations.Default)]
    public static ref A Peek(ref StackFrame stack) =>
        ref Instance.PeekImpl(ref stack.frame);
    
    [MethodImpl(Optimisations.Default)]
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
    protected abstract bool PopImpl(ref OpFrame frame);
    protected abstract bool PushImpl(ref OpFrame frame, in A top);
    protected abstract ref A PeekImpl(ref OpFrame frame);

    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the ValueStack instance");    
}

class ManagedValueStack<A> : ValueStack<A>
    where A : class
{
    static ManagedValueStack() =>
        Instance = new ManagedValueStack<A>();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref OpFrame frame, out A top) =>
        frame.objs.Pop(out top);
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref OpFrame frame) =>
        frame.objs.Pop();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PushImpl(ref OpFrame frame, in A top) =>
        frame.objs.Push(top);
        
    [MethodImpl(Optimisations.Default)]
    protected override ref A PeekImpl(ref OpFrame frame) =>
        ref frame.objs.Peek<A>();
}

class UnmanagedValueStack<A> : ValueStack<A>
    where A : unmanaged
{
    static UnmanagedValueStack() =>
        Instance = new UnmanagedValueStack<A>();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref OpFrame frame, out A top) =>
        frame.values.Pop(out top);
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref OpFrame frame) =>
        frame.values.Pop<A>();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PushImpl(ref OpFrame frame, in A top) =>
        frame.values.Push(top);
        
    [MethodImpl(Optimisations.Default)]
    protected override ref A PeekImpl(ref OpFrame frame) =>
        ref frame.values.Peek<A>();
}