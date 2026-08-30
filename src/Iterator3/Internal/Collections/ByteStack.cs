#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = Capacity)]
public readonly struct ByteStack
{
    public const int Capacity = 128 - sizeof(int);
    
    [FieldOffset(0)]
    public readonly int Count;
    
    [FieldOffset(4)]
    public readonly byte Stack;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Add(in ByteStack rhs)
    {
        if(rhs.Count + Count > Capacity)
        {
            return false;
        }

        var     sizeOfPtr = Unsafe.SizeOf<nint>();
        var     srcSize   = (uint)(rhs.Count * sizeOfPtr);
        ref var dest      = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), rhs.Count * sizeOfPtr);
        ref var src       = ref Unsafe.AsRef(in rhs.Stack);
        
        Unsafe.CopyBlock(ref dest, ref src, srcSize);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopToTop(int top)
    {
        ref var t = ref Unsafe.AsRef(in Count);
        t = Math.Min(t, top);
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>()
    {
        var     sizeOf = Unsafe.SizeOf<A>();
        ref var top    = ref Unsafe.AsRef(in Count);
        top -= sizeOf;
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>(out A value)
    {
        var     sizeOf = Unsafe.SizeOf<A>();
        ref var top    = ref Unsafe.AsRef(in Count);
        top -= sizeOf;
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), top));
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Peek<A>(out A value)
        where A : unmanaged
    {
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = Unsafe.SizeOf<A>();
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, Count - sizeOf));
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A PeekAt<A>()
        where A : unmanaged =>
        ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Count - Unsafe.SizeOf<A>()));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        if (Count + sizeOf > Capacity) return false;
        ref var top   = ref Unsafe.AsRef(in Count);
        ref var stack = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Count);
        ref var entry = ref Unsafe.As<byte, A>(ref stack);
        entry = value;
        top += sizeOf;
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Prepend<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        if (Count + sizeOf > Capacity) return false;
        ref var top  = ref Unsafe.AsRef(in Count);
        ref var src  = ref Unsafe.AsRef(in Stack);
        ref var dest = ref Unsafe.AddByteOffset(ref src, sizeOf);

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(ref dest, ref src, (uint)sizeOf);
        
        ref var entry = ref Unsafe.As<byte, A>(ref src);
        entry = value;
        top += sizeOf;
        return true;
    }
}
