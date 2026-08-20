using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator2<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    readonly IteratorFields2<T, IS, A, B> fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator2(K<T, A> ta, IteratorAction<B> action, in IS space) =>
        fields = new IteratorFields2<T, IS, A, B>(ta, action, in space);

    public Iterator2<B> Lower
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => new (fields.ta,
                    (IteratorAction<B>?)fields.action ?? PureAction<T, IS, B>.Default,
                    Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fields.space)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out B head, out Iterator2<T, IS, A, B> tail)
    {
        tail = this;    // Copy
        ref var ta = ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in tail.fields.ta));
        ref var a  = ref Unsafe.As<IteratorAction<B>, IteratorAction>(ref Unsafe.AsRef(in tail.fields.action));
        ref var s  = ref Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in tail.fields.space));
        return fields.action.TryGetValue(ref ta, ref a, ref s, out head);        
    }
}
