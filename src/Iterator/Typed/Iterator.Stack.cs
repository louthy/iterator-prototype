using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct IteratorStack<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    public ref K<T, A> ta;
    public ref IteratorAction<A> action;
    public ref IS space;
    
    [MethodImpl(Optimisations.Default)]
    public IteratorStack(ref K<T, A> ta, ref IteratorAction<A> action, ref IS space)
    {
        this.ta = ref ta;
        this.action = ref action;
        this.space = ref space;
    }

    [MethodImpl(Optimisations.Default)]
    public static ref IteratorStack<T, IS, A> From(ref IteratorStack stack) =>
       ref Unsafe.As<IteratorStack, IteratorStack<T, IS, A>>(ref stack);
}