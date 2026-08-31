#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = IndexCapacity * IndexItemSize + DataCapacity + sizeof(ushort) + sizeof(ushort))]
public readonly struct ByteList
{
    // We expect most value-types to be integers or similar, so an index that is a 
    // quarter of the size of the data seems reasonable (each index element is one-byte).
    
    const int IndexItemSize = 1;
    const int IndexCapacity = DataCapacity / 4;
    const int DataCapacity = 128;
    
    [FieldOffset(0)]
    public readonly byte index;
    
    [FieldOffset(IndexCapacity)]
    public readonly byte data;
    
    [FieldOffset(IndexCapacity + DataCapacity)]
    readonly ushort top;
    
    [FieldOffset(IndexCapacity + DataCapacity + sizeof(ushort))]
    public readonly ushort Count;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    ref byte Offset(int i)
    {
        if (i < IndexCapacity)
        {
            return ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in index), i * IndexItemSize);
        }
        else
        {
            throw new IndexOutOfRangeException();
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool At<A>(int ix, out A value)
        where A : unmanaged
    {
        ref var offset = ref Offset(ix);
        ref var stack  = ref Unsafe.AsRef(in data);
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, offset));
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Add<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = (ushort)Unsafe.SizeOf<A>();
        if (Count >= IndexCapacity || top + sizeOf > DataCapacity) return false;
        
        ref var d = ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in data), top));
        ref var i = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in index), Count);
        ref var t = ref Unsafe.AsRef(in top);
        
        i = (byte)top;
        d = value;
        t += sizeOf;

        return true;
    }
}
