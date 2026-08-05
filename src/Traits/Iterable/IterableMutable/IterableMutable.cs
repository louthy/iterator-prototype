using System.Runtime.CompilerServices;
using LanguageExt;

namespace IteratorTest.Traits;

/// <summary>
/// Apply this to an instance-type of an `IterableK`
/// </summary>
public interface IterableMutable<TA, IS, MS, A> : IterableImmutable<TA, IS, A>
    where TA : class, IterableMutable<TA, IS, MS, A>
    where IS : struct
    where MS : allows ref struct
{
    static abstract MS SetupMutable(TA ta);
    static abstract bool StepMutable(TA ta, ref MS ts, out A value);

    static Iterator<TA, IS, A> Iterable<TA, Iterator<TA, IS, A>, A>.Forward(TA ta)
    {
        var ts = TA.SetupImmutable(ta);
        return TA.StepImmutable(ta, in ts, out var head, out var tail) 
                   ? new Iterator<TA, IS, A>(in head, in ta, in tail) 
                   : default;
    }

    static IEnumerable<A> Iterable<TA, Iterator<TA, IS, A>, A>.AsEnumerable(TA ta) =>
        new IterableMutableEnumerable<TA, IS, MS, A>(ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    new static virtual IterableMutableEnumerator<TA, IS, MS, A> GetEnumerator(TA ta) =>
        new (ta);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ReadOnlySpan<A> Iterable<TA, Iterator<TA, IS, A>, A>.AsSpan(TA ta)
    {
        var w = ArrayWriter<A>.Init();
        var s = TA.SetupMutable(ta);
        while (TA.StepMutable(ta, ref s, out var x))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }
}
