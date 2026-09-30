#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = Capacity)]
struct ByteStackMutable
{
    public const int Capacity = 128 - sizeof(int);
    
    [FieldOffset(0)]
    public int Count;
    
    [FieldOffset(4)]
    public byte Stack;
    
    public Span<byte> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in Stack), Count);
    }
    
    public Span<byte> AllValues
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in Stack), Capacity);
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Span<byte> NextValues(int amount) =>
        amount + Count > Capacity
            ? throw new InvalidOperationException("Stack overflow")
            : MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in Stack), Count), amount);
    
    [MethodImpl(Optimisations.Default)]
    public void Add(in ByteStack rhs) =>
        rhs.Values.CopyTo(NextValues(rhs.Count));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PopToTop(int top)
    {
        Count = Math.Min(Count, top);
        return true;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Pop<A>()
    {
        var sizeOf = Unsafe.SizeOf<A>();
        Count -= sizeOf;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Pop<A>(out A value)
    {
        var sizeOf = Unsafe.SizeOf<A>();
        Count -= sizeOf;
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Count));
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

        Count += sizeOf; 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Push<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        ref var stack = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Count);
        ref var entry = ref Unsafe.As<byte, A>(ref stack);
        entry = value;
        Count += sizeOf;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Prepend<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        ref var src  = ref Unsafe.AsRef(in Stack);
        ref var dest = ref Unsafe.AddByteOffset(ref src, sizeOf);

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(ref dest, ref src, (uint)sizeOf);
        
        ref var entry = ref Unsafe.As<byte, A>(ref src);
        entry = value;
        Count += sizeOf;
    }
}
