using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

abstract class VarsGen<A>
{
    public static VarsGen<A> Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static VarsGen()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedVars<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            var type = typeof(ManagedVars<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }

    public abstract bool PopImpl(ref Vars frame, out A top);
    public abstract bool PopImpl(ref Vars frame);
    public abstract bool PrependImpl(ref Vars frame, in A top);
    public abstract bool PushImpl(ref Vars frame, in A top);
    public abstract bool PeekImpl(ref Vars frame, out A top);    
}

class ManagedVars<A> : VarsGen<A>
    where A : class
{
    static ManagedVars() =>
        Instance = new ManagedVars<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack, out A top) =>
        stack.objs.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack) =>
        stack.objs.Pop();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars stack, in A top) =>
        stack.objs.Push(top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref Vars stack, in A top) =>
        stack.objs.Prepend(top);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars stack, out A top) =>
        stack.objs.Peek(out top);
}

class UnmanagedVars<A> : VarsGen<A>
    where A : unmanaged
{
    static UnmanagedVars() =>
        Instance = new UnmanagedVars<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack, out A top) =>
        stack.values.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack) =>
        stack.values.Pop<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars stack, in A top) =>
        stack.values.Push(top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref Vars stack, in A top) =>
        stack.values.Prepend(top);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars stack, out A top) =>
        stack.values.Peek(out top);
}