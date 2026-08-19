using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    readonly IteratorFields2<T, IS, A> fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator2(K<T, A> ta, in IS space) =>
        fields = new IteratorFields2<T, IS, A>(ta, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator2(K<T, A> ta, IteratorAction<T, IS, A> action, in IS space) =>
        fields = new IteratorFields2<T, IS, A>(ta, action, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<T, IS, A> tail)
    {
        tail = this;    // Copy
        ref var s = ref Unsafe.AsRef(in tail.fields.space);
        return fields.action is null 
                   ? T.Next(in fields.ta, ref s, out head) 
                   : fields.action.TryGetValue(in fields.ta, ref s, out head);
    }

    public Iterator2<A> Lower
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => new (fields.ta,
                    fields.action ?? IdAction<T, IS, A>.Default,
                    Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fields.space)));
    }
}
