using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Collections;

[SkipLocalsInit]
readonly struct OpStack
{
    const int MaxCapacity = 4;
    const int HeaderSize = sizeof(int);
    
    public readonly int Top;
    public readonly OpFrame Frame0;
    public readonly OpFrame Frame1;
    public readonly OpFrame Frame2;
    public readonly OpFrame Frame3;

    public ref OpFrame this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref OpStack dest)
    {
       
        Frame0.CopyTo(ref Unsafe.AsRef(in dest.Frame0));
        Frame1.CopyTo(ref Unsafe.AsRef(in dest.Frame1));
        Frame2.CopyTo(ref Unsafe.AsRef(in dest.Frame2));
        Frame3.CopyTo(ref Unsafe.AsRef(in dest.Frame3));
        ref var dtop = ref Unsafe.AsRef(in dest.Top);
        dtop = Top;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Pop()
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == 0) throw new StackUnderflowException("OpStack underflow");
        
        top--;
        ref var frame = ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), top);
        frame.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpStack PopSafe()
    {
        OpStack stack = default;
        CopyTo(ref stack);
        stack.Pop();
        return stack;
    }

    public ref OpFrame AtTop
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), Top - 1);
    }

    public ref Op AtPC
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref AtTop.AtPC;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void ResetPC()
    {
        ref var top = ref AtTop;
        top.ResetPC();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool NextPC(out Op op)
    {
        if (Top == 0)
        {
            op = null!;
            return false;
        }
        ref var top = ref AtTop;
        return top.NextPC(out op);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Add(Op op)
    {
        if (Top == 0) throw new StackUnderflowException();
        AtTop.Add(op);   
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpStack AddSafe(Op op)
    {
        if (Top == 0) throw new StackUnderflowException();
        OpStack stack = default;
        CopyTo(ref stack);
        stack.Add(op);   
        return stack;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Push(in object ta)
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == MaxCapacity) throw new InvalidOperationException("OpStack is full");
        ref var frame = ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), top);
        Unsafe.AsRef(in frame.Self) = ta;
        top++;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Push(in OpFrame frame)
    {
        ref var top   = ref Unsafe.AsRef(in Top);
        if(top == MaxCapacity) throw new InvalidOperationException("OpStack is full");
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), top);
        entry = frame;
        top++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpStack PushSafe(OpFrame value)
    {
        OpStack stack = default;
        CopyTo(ref stack);
        stack.Push(value);
        return stack;
    }
}
