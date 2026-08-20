using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct IteratorStack<A, B>
{
    public ref object ta;
    public ref IteratorAction<B> action;
    public ref Space128 space;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorStack(ref object ta, ref IteratorAction<B> action, ref Space128 space)
    {
        this.ta = ref ta;
        this.action = ref action;
        this.space = ref space;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref IteratorStack<A, B> From(ref IteratorStack stack) =>
        ref Unsafe.As<IteratorStack, IteratorStack<A, B>>(ref stack);
}

[SkipLocalsInit]
public ref struct IteratorStack<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public ref K<T, A> ta;
    public ref IteratorAction<B> action;
    public ref IS space;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorStack(ref K<T, A> ta, ref IteratorAction<B> action, ref IS space)
    {
        this.ta = ref ta;
        this.action = ref action;
        this.space = ref space;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref IteratorStack<T, IS, A, B> From(ref IteratorStack stack) =>
       ref Unsafe.As<IteratorStack, IteratorStack<T, IS, A, B>>(ref stack);
}