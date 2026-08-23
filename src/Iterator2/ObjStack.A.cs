using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// ReSharper disable UnassignedReadonlyField
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace IteratorPrototype;

[SkipLocalsInit]
readonly struct ObjStack<A>
    where A : class
{
    const int StackSize = 16;
    
    public readonly int Top;
    public readonly A Object00;
    public readonly A Object01;
    public readonly A Object02;
    public readonly A Object03;
    public readonly A Object04;
    public readonly A Object05;
    public readonly A Object06;
    public readonly A Object07;
    public readonly A Object08;
    public readonly A Object09;
    public readonly A Object0A;
    public readonly A Object0B;
    public readonly A Object0C;
    public readonly A Object0D;
    public readonly A Object0E;
    public readonly A Object0F;
    
    public ref A this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Object00), index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref ObjStack<A> dest)
    {
        Unsafe.CopyBlock(ref Unsafe.As<A, byte>(ref Unsafe.AsRef(in dest.Object00)), 
                         ref Unsafe.As<A, byte>(ref Unsafe.AsRef(in Object00)), 
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
    public ObjStack<A> PopSafe()
    {
        var stack = this; // Copy
        stack.Pop();
        return stack;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A Peek() =>
        ref Unsafe.Add(ref Unsafe.AsRef(in Object00), Top - 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Push(in A value)
    {
        ref var top = ref Unsafe.AsRef(in Top);
        if(top == StackSize) throw new StackOverflowException("ObjStack underflow");
        
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = value;
        
        top++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ObjStack<A> PushSafe(A value)
    {
        ObjStack<A> stack = default!;
        CopyTo(ref stack);
        stack.Push(in value);
        return stack;
    }
    
}
