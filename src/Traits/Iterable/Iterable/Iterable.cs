using System.Runtime.CompilerServices;
using LanguageExt;

namespace IteratorTest.Traits;

public interface Iterable<A>
    where A : allows ref struct;

public interface Iterable<TA, A> : Iterable<A>
    where TA : Iterable<TA, A>
    where A : allows ref struct
{
    public TA Self
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
        get => (TA)this;
    }
}

public interface Iterable<TA, IA, A> : Iterable<TA, A>
    where TA : Iterable<TA, IA, A>
    where IA : IIterator<IA, A>
{
    /// <summary>
    /// Iterates from the first item in the structure to the last.
    /// </summary>
    /// <param name="ta">Structure to iterate</param>
    /// <returns>Iterator</returns>
    static abstract IA Forward(TA ta);

    /// <summary>
    /// Writes the structure to a flat array and returns as a `ReadOnlySpan`.
    /// </summary>
    /// <param name="ta"></param>
    /// <returns></returns>
    static virtual ReadOnlySpan<A> AsSpan(TA ta)
    {
        var w = ArrayWriter<A>.Init();
        var i = TA.Forward(ta);
        while (i.TryGetValue(out var x, out i))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static virtual IEnumerable<A> AsEnumerable(TA ta)
    {
        var i = TA.Forward(ta);
        while (i.TryGetValue(out var x, out i))
        {
            yield return x;
        }
    }
}