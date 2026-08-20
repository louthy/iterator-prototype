using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator2<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    internal readonly IteratorFields2<T, IS, A> fields;

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
        ref var fs = ref Unsafe.AsRef(in tail.fields);
        ref var ta = ref Unsafe.AsRef(in fs.ta);
        ref var s  = ref Unsafe.AsRef(in fs.space);
        
        if (fs.action is null)
        {
            return T.Next(in fields.ta, ref s, out head);
        }
        else
        {
            ref var a = ref Unsafe.AsRef(in fs.action);
            return fs.action.TryGetValue(ref ta, ref a, ref s, out head);
        }
    }
    
    public Iterator2<A> Lower
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => new (fields.ta,
                    fields.action ?? IdAction<T, IS, A>.Default,
                    in Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fields.space)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator2<B> Map<B>(Func<A, B> f) =>
        new Iterator2<T, IS, A, B>(
            fields.ta, 
            (IteratorAction<T, IS, A, B>)(fields.action ?? IdAction<T, IS, A>.Default).Map(f), 
            fields.space)
           .Lower;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator2<T, IS, A> GetEnumerator() =>
        new (in this);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator2<T, IS, A> operator+(A x, Iterator2<T, IS, A> xs) =>
        new (xs.fields.ta, (xs.fields.action ?? IdAction<T, IS, A>.Default).Cons(x), xs.fields.space);
}
