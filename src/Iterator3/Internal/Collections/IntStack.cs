using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
public readonly struct UnStack<A>
    where A : unmanaged
{
    const int Capacity = 16;
    
    public readonly A Item0;
    public readonly A Item1;
    public readonly A Item2;
    public readonly A Item3;
    public readonly A Item4;
    public readonly A Item5;
    public readonly A Item6;
    public readonly A Item7;
    public readonly A Item8;
    public readonly A Item9;
    public readonly A ItemA;
    public readonly A ItemB;
    public readonly A ItemC;
    public readonly A ItemD;
    public readonly A ItemE;
    public readonly A ItemF;
    public readonly int Top;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public UnStack() =>
        Top = 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public UnStack(params ReadOnlySpan<A> items)
    {
        var span = MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in Item0), Capacity);
        items.CopyTo(span);
        Top = items.Length;
    }
  
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop()
    {
        ref var top = ref Unsafe.AsRef(in Top);
        top --;
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop(out A value)
    {
        ref var top = ref Unsafe.AsRef(in Top);
        top--;
        value = Unsafe.Add(ref Unsafe.AsRef(in Item0), top);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Peek(out A value)
    {
        value = Unsafe.Add(ref Unsafe.AsRef(in Item0), Top - 1);
        return true;
    }

    public ref A PeekAt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Item0), Top - 1);
    }

    public ref int TopRef
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.AsRef(in Top);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push(in A value)
    {
        if (Top >= Capacity) return false;
        ref var top   = ref Unsafe.AsRef(in Top);
        ref var entry = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Item0), Top);
        entry = value;
        top++;
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Prepend(in A value)
    {
        if (Top >= Capacity) return false;
        ref var top  = ref Unsafe.AsRef(in Top);
        ref var src  = ref Unsafe.AsRef(in Item0);
        ref var dest = ref Unsafe.Add(ref src, 1);

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(ref Unsafe.As<A, byte>(ref dest), ref Unsafe.As<A, byte>(ref src), (uint)Unsafe.SizeOf<A>());
        
        src = value;
        top++;
        return true;
    }
}
