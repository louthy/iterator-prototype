#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = DataCapacity2 + IndexCapacity * IndexItemSize + sizeof(ushort) + sizeof(ushort))]
public readonly struct ByteList2
{
    // We expect most value-types to be integers or similar, so an index that is a 
    // quarter of the size of the data seems reasonable (each index element is one-byte).
    
    const int IndexItemSize = 1;
    const int IndexCapacity = DataCapacity / 4;
    const int DataCapacity = 128;
    const int DataCapacity2 = DataCapacity * 2;
    
    [FieldOffset(0)]
    public readonly byte index;
    
    [FieldOffset(IndexCapacity)]
    public readonly byte data;
    
    [FieldOffset(IndexCapacity + DataCapacity2)]
    readonly ushort top;
    
    [FieldOffset(IndexCapacity + DataCapacity2 + sizeof(ushort))]
    public readonly ushort Count;

    [MethodImpl(Optimisations.Default)]
    ref byte Offset(in ushort i) =>
        ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in index), i * IndexItemSize);
    
    [MethodImpl(Optimisations.Default)]
    public ref A At<A>(ushort ix)
        where A : unmanaged
    {
        ref var offset = ref Offset(ix);
        ref var stack  = ref Unsafe.AsRef(in data);
        return ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, offset));
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool At<A>(in ushort ix, out A value)
        where A : unmanaged
    {
        ref var offset = ref Offset(in ix);
        ref var stack  = ref Unsafe.AsRef(in data);
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, offset));
        return true;
    }
    
    [MethodImpl(Optimisations.Default)]
    public ref A DeclaredAt<A>(ushort ix)
        where A : unmanaged =>
        ref Unsafe.AddByteOffset(ref At<A>(ix), Unsafe.SizeOf<A>());
    
    [MethodImpl(Optimisations.Default)]
    public bool DeclaredAt<A>(in ushort ix, out A value)
        where A : unmanaged
    {
        value = DeclaredAt<A>(ix);
        return true;
    }
    
    [MethodImpl(Optimisations.Default)]
    public ref A RestoreAt<A>(ushort ix)
        where A : unmanaged
    {
        ref var variable = ref At<A>(ix);
        ref var declared = ref Unsafe.AddByteOffset(ref variable, Unsafe.SizeOf<A>());
        variable = declared;
        return ref variable;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool RestoreAt<A>(in ushort ix, out A value)
        where A : unmanaged
    {
        if (ix < Count)
        {
            value = RestoreAt<A>(ix);
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool Add<A>(in A value)
        where A : unmanaged =>
        Add(in value, out _);
    
    [MethodImpl(Optimisations.Default)]
    public bool Add<A>(in A value, out ushort ix)
        where A : unmanaged
    {
        unchecked
        {
            var sizeOf  = Unsafe.SizeOf<A>();
            var sizeOf2 = sizeOf << 1;
            var newTop  = top + sizeOf2;
            if (Count >= IndexCapacity || newTop > DataCapacity2)
            {
                ix = 0;
                return false;
            }
            ix = Count;
            ref var c = ref Unsafe.AsRef(in Count);
            c++;

            ref var d0 = ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in data), top));
            ref var d1 = ref Unsafe.AddByteOffset(ref d0, sizeOf);
            ref var i  = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in index), ix);
            ref var t  = ref Unsafe.AsRef(in top);

            i = (byte)top;
            d0 = value;
            d1 = value;
            t = (ushort)newTop;

            return true;
        }
    }
}
