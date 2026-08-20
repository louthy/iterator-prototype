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
        ref var ta = ref Unsafe.AsRef(in tail.fields.ta);
        ref var a  = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in tail.fields.action));
        ref var s  = ref Unsafe.AsRef(in tail.fields.space);
        return fields.action.TryGetValue(ref ta, ref a, ref s, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator2<A> GetEnumerator() =>
        new (in this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<B> Map<B>(Func<A, B> f) =>
        new (fields.ta, fields.action.Map(f), fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<B> Bind<B>(Func<A, Iterator2<B>> f) =>
        new (fields.ta, fields.action.Bind(f), fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<A> Concat(Iterator2<A> rhs) =>
        new (fields.ta, fields.action.Concat(rhs), fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref object ta, ref Space128 space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta!;
        space = fs.space;
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref object ta, ref IteratorAction action, ref Space128 space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta!;
        action = fs.action!;
        space = fs.space;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref object ta, ref IteratorAction<A> action, ref Space128 space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta!;
        action = fs.action!;
        space = fs.space;
    }
}
