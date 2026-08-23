using System.Runtime.CompilerServices;
// ReSharper disable UnassignedReadonlyField
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace IteratorPrototype.Internal.Collections;

[SkipLocalsInit]
readonly struct ObjStack
{
    const int StackSize = 16;
    
    public readonly int Top;
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
    public void Pop()
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == 0) throw new StackUnderflowException("ObjStack underflow");
        
        top--;
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = null!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ObjStack PopSafe()
    {
        var stack = this; // Copy
        stack.Pop();
        return stack;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A Peek<A>()
        where A : class =>
        ref Unsafe.As<object, A>(ref Unsafe.Add(ref Unsafe.AsRef(in Object00), Top - 1));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Push<A>(in A value)
        where A : class 
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == StackSize) throw new StackOverflowException("ObjStack underflow");
        
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = value;
        
        top++;
    }
}
