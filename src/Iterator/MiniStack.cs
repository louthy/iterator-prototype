// ReSharper disable UnassignedField.Local
#pragma warning disable CS0169 // Field is never used

using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct MiniStack<A>
    where A : allows ref struct
{
    A item0;
    A item1;
    A item2;
    A item3;
    
    public int Top;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Push(in A value)
    {
        if (Top == 4) throw new StackOverflowException();
        ref var t = ref Unsafe.Add(ref Unsafe.AsRef(in item0), Top);
        t = value;
        Top++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A Pop()
    {
        if (Top == 0) throw new StackUnderflowException();
        Top--;
        return ref Unsafe.Add(ref Unsafe.AsRef(in item0), Top);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A Peek()
    {
        if (Top == 0) throw new StackUnderflowException();
        return ref Unsafe.Add(ref Unsafe.AsRef(in item0), Top - 1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref MiniStack<B> Cast<B>(ref MiniStack<A> stack)
        where B : allows ref struct =>
        ref Unsafe.As<MiniStack<A>, MiniStack<B>>(ref stack);
}

public static class MiniStack
{
    extension<A>(ref MiniStack<A> stack)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref MiniStack<B> Cast<B>()
            where B : allows ref struct =>
            ref Unsafe.As<MiniStack<A>, MiniStack<B>>(ref stack);
    }
}

public class StackUnderflowException : Exception;