using IteratorPrototype.Traits;
using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct IteratorStack
{
    public ref object ta;
    public ref IteratorAction action;
    public ref Space128 space;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorStack(ref object ta, ref IteratorAction action, ref Space128 space)
    {
        this.ta = ref ta;
        this.action = ref action;
        this.space = ref space;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref IteratorStack From<T, IS, A>(ref IteratorStack<T, IS, A> stack) 
        where T : IterableImmutable<T, IS>
        where IS : struct =>
        ref Unsafe.As<IteratorStack<T, IS, A>, IteratorStack>(ref stack);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref IteratorStack From<T, IS, A, B>(ref IteratorStack<T, IS, A, B> stack) 
        where T : IterableImmutable<T, IS>
        where IS : struct =>
        ref Unsafe.As<IteratorStack<T, IS, A, B>, IteratorStack>(ref stack);    
}