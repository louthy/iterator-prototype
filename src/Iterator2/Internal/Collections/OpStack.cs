using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Sources;

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
    public void SetSource(in IteratorSource? src) =>
        AtTop.SetSource(in src);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref IteratorSource<A>? GetSource<A>() =>
        ref AtTop.GetSource<A>();
    
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

    public ref OpFrame AtTop
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), Top - 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run()
    {
        if (Top == 0) return false;
        ref var top    = ref Unsafe.AsRef(in Top);
        ref var frame0 = ref Unsafe.AsRef(in Frame0);
        while (top > 0)
        {
            ref var frame  = ref Unsafe.Add(ref frame0, top - 1);
            if (frame.Run())
            {
                return true;
            }
            else
            {
                top--;
                frame.Clear();
            }
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Add(Op op)
    {
        if (Top == 0) throw new StackUnderflowException();
        AtTop.Add(op);   
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref OpFrame Push()
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == MaxCapacity) throw new InvalidOperationException("OpStack is full");
        top++;
        return ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), top - 1);
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
}
