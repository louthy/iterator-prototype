using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = Capacity)]
readonly struct ByteStack
{
    const int Capacity = 128 - sizeof(int);
    
    [FieldOffset(0)]
    public readonly int Top;
    
    [FieldOffset(4)]
    public readonly byte Stack;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref ByteStack dest)
    {
        Unsafe.CopyBlock(ref Unsafe.AsRef(in dest.Stack), in Stack, (uint)Top);
        ref var dtop = ref Unsafe.AsRef(in dest.Top);
        dtop = Top;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Clear()
    {
        ref var self = ref Unsafe.AsRef(in this);
        Unsafe.InitBlock(ref Unsafe.As<ByteStack, byte>(ref self), 0, Capacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>()
    {
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = Unsafe.SizeOf<A>();
        //if (Top < sizeOf) return false;
        ref var top    = ref Unsafe.AsRef(in Top);
        top -= sizeOf;
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Pop<A>(out A value)
    {
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = Unsafe.SizeOf<A>();
        /*
        if (Top < sizeOf)
        {
            value = default!;
            return false;
        }
        */
        ref var top    = ref Unsafe.AsRef(in Top);
        top -= sizeOf;
        value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, top));
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A Peek<A>()
        where A : unmanaged
    {
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = Unsafe.SizeOf<A>();
        return ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, Top - sizeOf));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Push<A>(in A value)
        where A : unmanaged
    {
        var sizeOf = Unsafe.SizeOf<A>();
        if (Top + sizeOf > Capacity) return false;
        ref var top   = ref Unsafe.AsRef(in Top);
        ref var stack = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Top);
        ref var entry = ref Unsafe.As<byte, A>(ref stack);
        entry = value;
        top += sizeOf;
        return true;
    }
}
