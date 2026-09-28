#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = Capacity)]
readonly struct ByteStack
{
    public const int Capacity = 128 - sizeof(int);
    
    [FieldOffset(0)]
    public readonly int Count;
    
    [FieldOffset(4)]
    public readonly byte Stack;
    
    [MethodImpl(Optimisations.Default)]
    public void Add(in ByteStack rhs)
    {
        var     sizeOfPtr = Unsafe.SizeOf<nint>();
        var     srcSize   = (uint)(rhs.Count * sizeOfPtr);
        ref var dest      = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), rhs.Count * sizeOfPtr);
        ref var src       = ref Unsafe.AsRef(in rhs.Stack);
        
        Unsafe.CopyBlock(ref dest, ref src, srcSize);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PopToTop(int top)
    {
        ref var t = ref Unsafe.AsRef(in Count);
        t = Math.Min(t, top);
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Pop<A>()
    {
        var     sizeOf = Unsafe.SizeOf<A>();
        ref var top    = ref Unsafe.AsRef(in Count);
        top -= sizeOf;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Pop<A>(out A value)
    {
        var     sizeOf = Unsafe.SizeOf<A>();
        ref var top    = ref Unsafe.AsRef(in Count);
        top -= sizeOf;
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), top));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dup<A>()
    {
        var     sizeOf = Unsafe.SizeOf<A>();
        var     last = Count - sizeOf;
        var     next = Count;
        ref var src  = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), last);
        ref var dst  = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), next);
        Unsafe.CopyBlock(ref dst, ref src, (uint)sizeOf);
        
        ref var top  = ref Unsafe.AsRef(in Count);
        top += sizeOf;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Peek<A>(out A value)
        where A : unmanaged
    {
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = Unsafe.SizeOf<A>();
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, Count - sizeOf));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref A PeekAt<A>()
        where A : unmanaged =>
        ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Count - Unsafe.SizeOf<A>()));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Push<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        ref var top   = ref Unsafe.AsRef(in Count);
        ref var stack = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Count);
        ref var entry = ref Unsafe.As<byte, A>(ref stack);
        entry = value;
        top += sizeOf;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Prepend<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        ref var top  = ref Unsafe.AsRef(in Count);
        ref var src  = ref Unsafe.AsRef(in Stack);
        ref var dest = ref Unsafe.AddByteOffset(ref src, sizeOf);

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(ref dest, ref src, (uint)sizeOf);
        
        ref var entry = ref Unsafe.As<byte, A>(ref src);
        entry = value;
        top += sizeOf;
    }
}
