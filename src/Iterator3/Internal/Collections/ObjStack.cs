#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
public readonly struct ObjStack
{
    const int Capacity = 16;
    
    public readonly object Object00;
    public readonly object Object01;
    public readonly object Object02;
    public readonly object Object03;
    public readonly object Object04;
    public readonly object Object05;
    public readonly object Object06;
    public readonly object Object07;
    public readonly object Object08;
    public readonly object Object09;
    public readonly object Object0A;
    public readonly object Object0B;
    public readonly object Object0C;
    public readonly object Object0D;
    public readonly object Object0E;
    public readonly object Object0F;
    public readonly int Top;
    
    public ref object this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Object00), index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref ObjStack dest)
    {
        Unsafe.CopyBlock(ref Unsafe.As<object, byte>(ref Unsafe.AsRef(in dest.Object00)), 
                         ref Unsafe.As<object, byte>(ref Unsafe.AsRef(in Object00)), 
                         (uint)(Top * Unsafe.SizeOf<nint>()));
        
        ref var dtop = ref Unsafe.AsRef(in dest.Top);
        dtop = Top;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopToTop(int top)
    {
        Debug.Assert(top <= Top);
        if(top == Top) return true;
        
        ref var tref  = ref Unsafe.AsRef(in Top);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), tref - 1);
        
        while(tref > top)
        {
            tref--;
            entry = null!;
            entry = ref Unsafe.Add(ref entry, -1);
        }
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop()
    {
        ref var top = ref Unsafe.AsRef(in Top);
        top--;
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = null!;
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>(out A value)
    {
        ref var top = ref Unsafe.AsRef(in Top);
        top--;
        ref var entry = ref Unsafe.As<object, A>(ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top));
        value = entry;
        entry = default!;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Peek<A>(out A value)
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), Top - 1);
        value = Unsafe.As<object, A>(ref entry);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push<A>(in A value)
        where A : class 
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == Capacity) return false;
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = value;
        top++;
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Prepend<A>(in A value)
        where A : class
    {
        if (Top + 1 > Capacity) return false;
        ref var top  = ref Unsafe.AsRef(in Top);
        ref var src  = ref Unsafe.AsRef(in Object00);
        ref var dest = ref Unsafe.Add(ref src, 1);

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(ref Unsafe.As<object, byte>(ref dest), ref Unsafe.As<object, byte>(ref src), (uint)(Top * Unsafe.SizeOf<nint>()));
        src = value;
        top++;
        return true;
    }
}
