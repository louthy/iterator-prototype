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
    public virtual Iterator<T, TS, A> Forward()
    {
        var     ta = this;
        var     i1 = T.Forward(ta);
        ref var i2 = ref Unsafe.As<Iterator<A>, Iterator<T, TS, A>>(ref i1);
        return i2;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public virtual IterableKEnumerator<T, TS, A> GetEnumerator() =>
        new (this);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public virtual ReadOnlySpan<A> ToArray()
    {
        var ta = this;
        var w  = ArrayWriter<A>.Init();
        var s  = T.Setup(ta);
        while (T.Step(ref s, out A x))
        {
            ArrayWriter<A>.Add(ref w, x);
        }
        return w.View;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public virtual IEnumerable<A> AsEnumerable() =>
        new IteratorEnumerable<T, TS, A>(this);
    
}