using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref Ops ops;
    public readonly ref ObjStack objs;
    public readonly ref ByteStack values;
    public readonly ref ByteStack state;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref Ops ops, ref Vars vars, ref ByteStack state)
    {
        this.ops = ref ops;
        objs = ref Unsafe.AsRef(in vars.objs);
        values = ref Unsafe.AsRef(in vars.values);
        this.state = ref state;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushState<S>(in S value)
        where S : unmanaged =>
        state.Push(in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool UnshiftState<S>(in S value)
        where S : unmanaged =>
        state.Unshift(in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopState<S>(out S value)
        where S : unmanaged =>
        state.Pop(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopState<S>()
        where S : unmanaged =>
        state.Pop<S>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PeekState<S>(out S value)
        where S : unmanaged =>
        state.Peek(out value);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push<A>(in A value) =>
        ValueStack<A>.Push(ref Unsafe.AsRef(in this), in value);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Unshift<A>(in A value) =>
        ValueStack<A>.Unshift(ref Unsafe.AsRef(in this), in value);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe bool Add(delegate*<ref StackFrame, bool> f) =>
        ops.Add(f);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushValue<A>(in A value)
        where A : unmanaged =>
        values.Push(in value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool UnshiftValue<A>(in A value)
        where A : unmanaged =>
        values.Unshift(in value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushObj<A>(in A value)
        where A : class =>
        objs.Push(in value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool UnshiftObj<A>(in A value)
        where A : class =>
        objs.Unshift(in value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>(out A value) =>
        ValueStack<A>.Pop(ref Unsafe.AsRef(in this), out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>() =>
        ValueStack<A>.Pop(ref Unsafe.AsRef(in this));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopValue<A>(out A value)
        where A : unmanaged =>
        values.Pop(out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopValue<A>()
        where A : unmanaged =>
        values.Pop<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopObj<A>(out A value)
        where A : class =>
        objs.Pop(out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopObj() =>
        objs.Pop();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Peek<A>(out A value) =>
        ValueStack<A>.Peek(ref Unsafe.AsRef(in this), out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PeekValue<A>(out A value)
        where A : unmanaged =>
        values.Peek(out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PeekObj<A>(out A value)
        where A : class =>
        objs.Peek(out value);
}
