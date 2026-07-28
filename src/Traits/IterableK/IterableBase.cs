using System.Runtime.CompilerServices;
using LanguageExt;
using LanguageExt.Traits;

namespace IteratorTest.Traits;

/// <summary>
/// Apply this to an instance-type of an `IterableK`
/// </summary>
public interface IterableBase<T, TS, TA, A> : K<T, A>
    where T : IterableK<T, TS>
    where TS : struct
    where TA : IterableBase<T, TS, TA, A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    Iterator<T, TS, A> Forward()
    {
        var     ta = this;
        var     i1 = T.Forward(ta);
        ref var i2 = ref Unsafe.As<Iterator<A>, Iterator<T, TS, A>>(ref i1);
        return i2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IterableKEnumerator<T, TS, A> GetEnumerator() =>
        new (this);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    ReadOnlySpan<A> AsSpan()
    {
        var ta = this;
        var w  = ArrayWriter<A>.Init();
        var s  = T.Setup(ta);
        while (T.Step(ta, ref s, out var x))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    IEnumerable<A> AsEnumerable() =>
        new IteratorEnumerable<T, TS, A>(this);
}