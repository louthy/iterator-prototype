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
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), index);
    }
    
    [MethodImpl(Optimisations.Default)]
    public void SetSource(in IteratorSource? src) =>
        AtTop.SetSource(in src);
    
    [MethodImpl(Optimisations.Default)]
    public ref IteratorSource<A>? GetSource<A>() =>
        ref AtTop.GetSource<A>();
    
    [MethodImpl(Optimisations.Default)]
    public void CopyTo(ref OpStack dest)
    {
        ref var df0 = ref Unsafe.AsRef(in dest.Frame0);
        ref var df1 = ref Unsafe.AsRef(in dest.Frame1);
        ref var df2 = ref Unsafe.AsRef(in dest.Frame2);
        ref var df3 = ref Unsafe.AsRef(in dest.Frame3);
        
        switch (Top)
        {
            case 0: 
                break;
            
            case 1: 
                df0 = Frame0;
                break;
            case 2:                 
                df0 = Frame0;
                df1 = Frame1;
                break;

            case 3: 
                df0 = Frame0;
                df1 = Frame1;
                df2 = Frame2;
                break;

            case 4: 
                df0 = Frame0;
                df1 = Frame1;
                df2 = Frame2;
                df3 = Frame3;
                break;

            default:
                throw new InvalidOperationException("Invalid OpStack top");
        }
        ref var dtop = ref Unsafe.AsRef(in dest.Top);
        dtop = Top;
    }
    
    [MethodImpl(Optimisations.Default)]
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
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), Top - 1);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run()
    {
        if (Top == 0) return false;
        ref var self   = ref Unsafe.AsRef(in this);
        ref var top    = ref Unsafe.AsRef(in Top);
        ref var frame0 = ref Unsafe.AsRef(in Frame0);
        while (top > 0)
        {
            ref var frame = ref Unsafe.Add(ref frame0, top - 1);
            var     stack = new StackFrame(ref self, ref frame);
            if (frame.Run(ref stack))
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

    [MethodImpl(Optimisations.Default)]
    public void Add(Op op)
    {
        if (Top == 0) throw new StackUnderflowException();
        AtTop.Add(op);   
    }
    
    [MethodImpl(Optimisations.Default)]
    public ref OpFrame Push()
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == MaxCapacity) throw new InvalidOperationException("OpStack is full");
        top++;
        return ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), top - 1);
    }
    
    [MethodImpl(Optimisations.Default)]
    public void Push(in OpFrame frame)
    {
        ref var top   = ref Unsafe.AsRef(in Top);
        if(top == MaxCapacity) throw new InvalidOperationException("OpStack is full");
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Frame0), top);
        entry = frame;
        top++;
    }
}
