/*#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal;

abstract class ValueStack<A>
{
    protected static ValueStack<A> Instance;

    [MethodImpl(Optimisations.Default)]
    public static bool Pop(ref StackFrame stack, out A top) =>
        Instance.PopImpl(ref stack, out top);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Pop(ref StackFrame stack) =>
        Instance.PopImpl(ref stack);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Prepend(ref StackFrame stack, in A top) =>
        Instance.PrependImpl(ref stack, in top);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Push(ref StackFrame stack, in A top) =>
        Instance.PushImpl(ref stack, in top);
    
    [MethodImpl(Optimisations.Default)]
    public static bool Peek(ref StackFrame stack, out A top) =>
        Instance.PeekImpl(ref stack, out top);

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

    protected abstract bool PopImpl(ref StackFrame frame, out A top);
    protected abstract bool PopImpl(ref StackFrame frame);
    protected abstract bool PrependImpl(ref StackFrame frame, in A top);
    protected abstract bool PushImpl(ref StackFrame frame, in A top);
    protected abstract bool PeekImpl(ref StackFrame frame, out A top);
}

class ManagedValueStack<A> : ValueStack<A>
    where A : class
{
    static ManagedValueStack() =>
        Instance = new ManagedValueStack<A>();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref StackFrame frame, out A top) =>
        frame.objs.Pop(out top);
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref StackFrame frame) =>
        frame.objs.Pop();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PushImpl(ref StackFrame frame, in A top) =>
        frame.objs.Push(top);
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PrependImpl(ref StackFrame frame, in A top) =>
        frame.objs.Prepend(top);

    [MethodImpl(Optimisations.Default)]
    protected override bool PeekImpl(ref StackFrame frame, out A top) =>
        frame.objs.Peek(out top);
}

class UnmanagedValueStack<A> : ValueStack<A>
    where A : unmanaged
{
    static UnmanagedValueStack() =>
        Instance = new UnmanagedValueStack<A>();

    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref StackFrame frame, out A top) =>
        frame.values.Pop(out top);
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PopImpl(ref StackFrame frame) =>
        frame.values.Pop<A>();
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PushImpl(ref StackFrame frame, in A top) =>
        frame.values.Push(top);
    
    [MethodImpl(Optimisations.Default)]
    protected override bool PrependImpl(ref StackFrame frame, in A top) =>
        frame.values.Prepend(top);
        
    [MethodImpl(Optimisations.Default)]
    protected override bool PeekImpl(ref StackFrame frame, out A top) =>
        frame.values.Peek(out top);
}*/