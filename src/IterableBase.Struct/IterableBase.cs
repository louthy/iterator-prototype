using System.Runtime.CompilerServices;
using LanguageExt;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

/// <summary>
/// Apply this to an instance-type of an `IterableK`
/// </summary>
public interface IterableBase<T, IS, TA, A> : K<T, A>
    where T : IterableK<T, IS>
    where IS : struct
    where TA : IterableBase<T, IS, TA, A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator<T, IS, A> Forward()
    {
        var     ta = this;
        var     i1 = T.Forward(ta);
        ref var i2 = ref Unsafe.As<Iterator<A>, Iterator<T, IS, A>>(ref i1);
        return i2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IterableKEnumerator<T, IS, A> GetEnumerator() =>
        new (this);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    ReadOnlySpan<A> AsSpan()
    {
        var ta   = this;
        var w    = ArrayWriter<A>.Init();
        var iter = IterableK.fromIterableStrong<T, IS, A>(ta);
        
        while (iter.TryGetValue(out var x, out iter))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IEnumerable<A> AsEnumerable() =>
        new IteratorEnumerable<T, IS, A>(this);
}