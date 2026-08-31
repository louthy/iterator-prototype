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
    
    [MethodImpl(Optimisations.Default)]
    public IteratorStack(ref object ta, ref IteratorAction<B> action, ref Space128 space)
    {
        this.ta = ref ta;
        this.action = ref action;
        this.space = ref space;
    }

    [MethodImpl(Optimisations.Default)]
    public static ref IteratorStack<A, B> From(ref IteratorStack stack) =>
        ref Unsafe.As<IteratorStack, IteratorStack<A, B>>(ref stack);
}

[SkipLocalsInit]
public ref struct IteratorStack<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    public ref K<T, A> ta;
    public ref IteratorAction<B> action;
    public ref IS space;
    
    [MethodImpl(Optimisations.Default)]
    public IteratorStack(ref K<T, A> ta, ref IteratorAction<B> action, ref IS space)
    {
        this.ta = ref ta;
        this.action = ref action;
        this.space = ref space;
    }

    [MethodImpl(Optimisations.Default)]
    public static ref IteratorStack<T, IS, A, B> From(ref IteratorStack stack) =>
       ref Unsafe.As<IteratorStack, IteratorStack<T, IS, A, B>>(ref stack);
}