using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator2<A>
{
    readonly IteratorFields2<A> fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator2(object ta, IteratorAction<A> action, in Space128 space) =>
        fields = new IteratorFields2<A>(ta, action, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<A> tail)
    {
        tail = this;    // Copy
        ref var s = ref Unsafe.AsRef(in tail.fields.space);
        return fields.action.TryGetValue(in fields.ta, ref s, out head);
    }
}
