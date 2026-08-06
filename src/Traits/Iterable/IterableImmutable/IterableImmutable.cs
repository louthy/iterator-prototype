using System.Runtime.CompilerServices;
using LanguageExt;

namespace IteratorTest.Traits;

/// <summary>
/// Apply this to an instance-type of an `IterableK`
/// </summary>
public interface IterableImmutable<TA, IS, A> : Iterable<TA, Iterator<TA, IS, A>, A>
    where IS : struct
    where TA : class, IterableImmutable<TA, IS, A>
{
    static abstract IS SetupImmutable(in TA ta);
    static abstract bool StepImmutable(in TA ta, in IS state, out A head, out IS tail);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static virtual IterableImmutableEnumerator<TA, IS, A> GetEnumerator(TA ta) =>
        new (in ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ReadOnlySpan<A> Iterable<TA, Iterator<TA, IS, A>, A>.AsSpan(TA ta)
    {
        var w    = ArrayWriter<A>.Init();
        var iter = IterableImmutable.fromStrong<TA, IS, A>(in ta);
        
        while (iter.TryGetValue(out var x, out iter))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static IEnumerable<A> Iterable<TA, Iterator<TA, IS, A>, A>.AsEnumerable(TA ta) =>
        new IterableImmutableEnumerable<TA, IS, A>(ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static virtual void NextImmutableUntyped(in TA ta, ref IteratorMutable<A> next)
    {
        ref var state = ref Unsafe.As<Space128, IS>(ref Unsafe.AsRef(in next.space));
        if (TA.StepImmutable(in ta, in state, out var head, out state))
        {
            next.head = head;
        }
        else
        {
            next.tag = IteratorTag.Empty;
        }
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static virtual void NextImmutable(in TA ta, ref IteratorMutable<TA, IS, A> next)
    {
        ref var state = ref next.space;
        if (TA.StepImmutable(in ta, in state, out var head, out state))
        {
            next.head = head;
        }
        else
        {
            next.tag = IteratorTag.Empty;
        }
    }    
}