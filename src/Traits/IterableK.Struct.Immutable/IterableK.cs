using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

/// <summary>
/// A specialised version of <see cref="IterableK{T}"/> that allows fast enumeration using an immutable `struct` state.
/// </summary>
/// <typeparam name="T">Trait type</typeparam>
/// <typeparam name="IS">Immutable state type</typeparam>
public interface IterableK<T, IS> : IterableK<T>
    where T : IterableK<T, IS>
    where IS : struct
{
    static abstract IS SetupImmutable<A>(K<T, A> ta);
    static abstract bool StepImmutable<A>(K<T, A> ta, in IS state, out A head, out IS tail);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static virtual void NextImmutableUntyped<A>(object taObj, ref IteratorMutable<A> next)
    {
        ref var ta    = ref Unsafe.As<object, K<T, A>>(ref taObj);
        ref var state = ref Unsafe.As<Space128, IS>(ref Unsafe.AsRef(in next.space));
        if (T.StepImmutable(ta, in state, out var head, out state))
        {
            next.head = head;
        }
        else
        {
            next.tag = IteratorTag.Empty;
        }
    }    

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static virtual void NextImmutable<A>(K<T, A> ta, ref IteratorMutable<T, IS, A> next)
    {
        ref var state = ref next.space;
        if (T.StepImmutable(ta, in state, out var head, out state))
        {
            next.head = head;
        }
        else
        {
            next.tag = IteratorTag.Empty;
        }
    }    

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Iterator<A> IterableK<T>.Forward<A>(K<T, A> ta) =>
        IterableK.fromIterable<T, IS, A>(ta);    
}
