using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = StackSizeInBytes)]
readonly struct ByteStack
{
    const int StackSizeInBytes = 128 - sizeof(int);
    
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
    public void Pop()
    {
        if(Top == 0) throw new StackUnderflowException("ObjStack underflow");
        
        ref var top = ref Unsafe.AsRef(in Top);

        top -= sizeof(int);
        
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = Unsafe.As<byte, int>(ref Unsafe.AddByteOffset(ref stack, top));
        
        top -= sizeOf;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ByteStack PopSafe()
    {
        var stack = this; // Copy
        stack.Pop();
        return stack;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A Peek<A>()
        where A : unmanaged
    {
        ref var stack  = ref Unsafe.AsRef(in Stack);
        var     sizeOf = sizeof(int) + Unsafe.SizeOf<A>();
        return ref Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack, Top - sizeOf));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Push<A>(in A value)
        where A : unmanaged
    {
        ref var top   = ref Unsafe.AsRef(in Top);
        ref var stack = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Stack), Top);
        ref var entry = ref Unsafe.As<byte, A>(ref stack);
        entry = value;

        var     sizeOf = Unsafe.SizeOf<A>();
        ref var size   = ref Unsafe.As<byte, int>(ref Unsafe.AddByteOffset(ref stack, sizeOf));
        size = sizeOf;
        
        top += sizeOf + sizeof(int);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ByteStack PushSafe<A>(A value)
        where A : unmanaged
    {
        ByteStack stack = default!;
        CopyTo(ref stack);
        stack.Push(in value);
        return stack;
    }
}
