using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator2<A>
{
    internal readonly IteratorFields2<A> fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator2(object ta, IteratorAction<A> action, in Space128 space) =>
        fields = new IteratorFields2<A>(ta, action, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<A> tail)
    {
        tail = this;    // Copy
        ref var a = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in tail.fields.action));
        ref var s = ref Unsafe.AsRef(in tail.fields.space);
        return fields.action.TryGetValue(in fields.ta, ref a, ref s, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator2<A> GetEnumerator() =>
        new (in this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<B> Map<B>(Func<A, B> f) =>
        new (fields.ta, fields.action.Map(f), fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<A> operator+(A x, Iterator2<A> xs) =>
        new (xs.fields.ta, xs.fields.action.Cons(x), xs.fields.space);
}
