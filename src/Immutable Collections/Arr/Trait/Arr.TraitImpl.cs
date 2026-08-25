using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorPrototype.Iterator3.Internal;
using LanguageExt;
using LanguageExt.Traits;

namespace IteratorPrototype;

public partial class Arr : 
    Monad<Arr>, 
    MonoidK<Arr>,
    Alternative<Arr>,
    Tr.Countable<Arr>,
    Tr.Indexable<Arr, int>,
    Tr.Indexable<Arr, Index>,
    Tr.RefIndexable<Arr, Index>,
    Tr.RefIndexable<Arr, int>,
    Tr.IterableMutable<Arr, ArrState, ArrStateRef>/*,
    Traversable<Arr>,
    Tr.Indexable<Arr, Index>,
    Natural<Arr, LE.Seq>,
    Natural<Arr, LE.Iterable>,
    Natural<Arr, LE.Lst>,
    Natural<Arr, LE.Set>,
    Natural<Arr, LE.HashSet>,
    Foldable<Arr, Arr.FoldState>,
    FoldableBack<Arr, Arr.FoldState>
    */
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Option<A> Tr.Indexable<Arr, Index>.At<A>(Index index, K<Arr, A> ta) =>
        ta.As().At(index);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static Option<A> Tr.Indexable<Arr, int>.At<A>(int index, K<Arr, A> ta) =>
        ta.As().At(index);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ref readonly A Tr.RefIndexable<Arr, Index>.AtRef<A>(in Index index, in K<Arr, A> ta)
    {
        if (index.IsFromEnd)
        {
            return ref Unsafe.Add(ref Unsafe.As<K<Arr, A>, A>(ref Unsafe.AsRef(in ta)), (nint)(uint)index.GetOffset(ta.Count) /* force zero-extension */);
        }
        else
        {
            return ref Unsafe.Add(ref Unsafe.As<K<Arr, A>, A>(ref Unsafe.AsRef(in ta)), (nint)(uint)index.Value /* force zero-extension */);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ref readonly A Tr.RefIndexable<Arr, int>.AtRef<A>(in int index, in K<Arr, A> ta) =>
        ref Unsafe.Add(ref Unsafe.As<K<Arr, A>, A>(ref Unsafe.AsRef(in ta)), (nint)(uint)index /* force zero-extension */);
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ReadOnlySpan<A> Tr.Iterable<Arr>.AsSpan<A>(K<Arr, A> ta) =>
        (+ta).AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static int Tr.Countable<Arr>.Count<A>(K<Arr, A> fa) =>
        fa is Arr<A> arr ? arr.Count : 0;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ArrState Tr.IterableImmutable<Arr, ArrState>.SetupImmutable<A>(in K<Arr, A> ta) =>
        new (0, ta.Count);
        

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool Tr.IterableImmutable<Arr, ArrState>.StepImmutable<A>(
        in K<Arr, A> ta, 
        in ArrState ts, 
        out A head, 
        out ArrState tail)
    {
        var index = ts.Index;
        var count = ts.Count;

        if (index >= count)
        {
            head = default!;
            tail = default!;
            return false;
        }

        ref var arr = ref Unsafe.As<K<Arr, A>, Arr<A>>(ref Unsafe.AsRef(in ta));
        head = arr.Values[index];
        tail = new ArrState(index + 1, count);

        return true;    
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Next<A>(ref StackFrame frame)
    {
        // Take the state value off the stack
        ref var ts = ref frame.RefState<ArrState>();

        // Take the iterable instance off the stack
        frame.PopObj<Arr<A>>(out var ta);

        // Step the iterable
        ref var index = ref Unsafe.AsRef(in ts.Index);
        var     count = ts.Count;
        if (index >= count)
        {
            frame.PopState<ArrState>();
            return false;
        }

        // Get the value
        ref var vs = ref MemoryMarshal.GetArrayDataReference(ta.Values);
        ref var v  = ref Unsafe.Add(ref vs, index);
        
        // Push the acquired head value onto the stack
        frame.Push(in v);
        index++;
        
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static ArrStateRef Tr.IterableMutable<Arr, ArrState, ArrStateRef>.SetupMutable<A>(K<Arr, A> ta)
    {
        var array = ta.AsSpan();
        if (array.IsEmpty) return default;

        ref var          items    = ref Unsafe.AsRef(in array[0]);
        ref readonly var itemsEnd = ref Unsafe.Add(ref Unsafe.AsRef(in array[^1]), 1);
        var              stateA   = new ArrStateRef<A>(ref items, in itemsEnd);
        return Unsafe.As<ArrStateRef<A>, ArrStateRef>(ref stateA);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static bool Tr.IterableMutable<Arr, ArrState, ArrStateRef>.StepMutable<A>(K<Arr, A> ta, ref ArrStateRef ts, out A value)
    {
        ref var          state    = ref Unsafe.As<ArrStateRef, ArrStateRef<A>>(ref ts);
        ref var          items    = ref state.Items;
        ref readonly var itemsEnd = ref state.ItemsEnd;

        if (Unsafe.AreSame(in items, in itemsEnd))
        {
            value = default!;
            return false;
        }

        value = items;
        
        items = ref Unsafe.Add(ref items, 1);
        state = new ArrStateRef<A>(ref items, in itemsEnd);
        return true;
    }    
    
    static K<Arr, B> Monad<Arr>.Bind<A, B>(K<Arr, A> ma, Func<A, K<Arr, B>> f)
    {
        var w  = ArrayWriter<B>.Init();
        var ts = ma.SetupMutable<Arr, ArrState, ArrStateRef, A>();
        while (ma.StepMutable<Arr, ArrState, ArrStateRef, A>(ref ts, out var a))
        {
            var mb  = +f(a);
            var ts1 = mb.SetupMutable<Arr, ArrState, ArrStateRef, B>();
            while (mb.StepMutable<Arr, ArrState, ArrStateRef, B>(ref ts1, out var b))
            {
                w.Add(b);
            }
        }
        return w.ToArr();
    }

    static K<Arr, B> Monad<Arr>.Recur<A, B>(A value, Func<A, K<Arr, Next<A, B>>> f) =>
        // TODO: We need an Iterable recur that is fast!
        create(Monad.enumerableRecur(value, x => f(x).As().AsEnumerable()));

    static K<Arr, B> Functor<Arr>.Map<A, B>(Func<A, B> f, K<Arr, A> ma)
    {
        var w  = ArrayWriter<B>.Init(ma.Count);
        var ts = ma.SetupMutable<Arr, ArrState, ArrStateRef, A>();
        while (ma.StepMutable<Arr, ArrState, ArrStateRef, A>(ref ts, out var a))
        {
            var b = f(a);
            w.Add(b);
        }
        return w.ToArr();
    }

    static K<Arr, A> Applicative<Arr>.Pure<A>(A value) =>
        singleton(value);

    static K<Arr, B> Applicative<Arr>.Apply<A, B>(K<Arr, Func<A, B>> mf, K<Arr, A> ma)
    {
        var writer = ArrayWriter<B>.Init();
        var fs = mf.SetupMutable<Arr, ArrState, ArrStateRef, Func<A, B>>();
        while (mf.StepMutable<Arr, ArrState, ArrStateRef, Func<A, B>>(ref fs, out var f))
        {
            var ast = ma.SetupMutable<Arr, ArrState, ArrStateRef, A>();
            while (ma.StepMutable<Arr, ArrState, ArrStateRef, A>(ref ast, out var a))
            {
                writer.Add(f(a));
            }
        }
        return writer.ToArr();
    }    

    static K<Arr, B> Applicative<Arr>.Apply<A, B>(K<Arr, Func<A, B>> mf, Memo<Arr, A> ma)
    {
        var writer = ArrayWriter<B>.Init();
        
        var fs = mf.SetupMutable<Arr, ArrState, ArrStateRef, Func<A, B>>();
        while (mf.StepMutable<Arr, ArrState, ArrStateRef, Func<A, B>>(ref fs, out var f))
        {
            var fa     = ma.Value;
            var ast    = fa.SetupMutable<Arr, ArrState, ArrStateRef, A>();
            while (fa.StepMutable<Arr, ArrState, ArrStateRef, A>(ref ast, out var a))
            {
                writer.Add(f(a));
            }
        }
        return writer.ToArr();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static K<Arr, A> MonoidK<Arr>.Empty<A>() =>
        Arr<A>.Empty;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static K<Arr, A> Alternative<Arr>.Empty<A>() =>
        Arr<A>.Empty;

    static K<Arr, A> SemigroupK<Arr>.Combine<A>(K<Arr, A> ma, K<Arr, A> mb)
    {
        var writer = ArrayWriter<A>.Init(ma.Count + mb.Count);
        writer.AddRange(ma.AsSpan());
        writer.AddRange(mb.AsSpan());
        return writer.ToArr();
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static K<Arr, A> Choice<Arr>.Choose<A>(K<Arr, A> ma, K<Arr, A> mb) => 
        ma is Arr<A> { IsEmpty: true } ? mb : ma;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    static K<Arr, A> Choice<Arr>.Choose<A>(K<Arr, A> ma, Memo<Arr, A> mb) => 
        ma is Arr<A> { IsEmpty: true } ? mb.Value : ma;

    /*
    static bool Foldable<Arr>.IsEmpty<A>(K<Arr, A> ta) =>
        ta.As().IsEmpty;

    /// <summary>
    /// Sort the items in the foldable structure in the order dictated by the ordering function
    /// </summary>
    /// <param name="comparer">Ordering function</param>
    /// <param name="ta">Foldable structure</param>
    /// <returns>An array of sorted values</returns>
    static Arr<A> Foldable<Arr>.Sort<A>(Comparison<A> comparer, K<Arr, A> ta)
    {
        var arr = ta.As();
        var cnt = arr.Count;
        if (cnt <= 0) return Arr<A>.Empty;
        if (cnt >= int.MaxValue) throw new ArgumentException("Arr: Foldable.Sort: structure too large");

        var xs  = ta.As().AsSpan();
        var yss = new A[cnt];
        var ys  = new Span<A>(yss);

        xs.CopyTo(ys);
        ys.Sort(comparer);

        return new Arr<A>(yss, 0, cnt);
    }

    /// <summary>
    /// Sort the items in the foldable structure in the order dictated by the ordering function using the key selector.
    /// </summary>
    /// <param name="key">Key selector function</param>
    /// <param name="comparer">Ordering function</param>
    /// <param name="ta">Foldable structure</param>
    /// <returns>An array of sorted values</returns>
    static Arr<A> Foldable<Arr>.Sort<A, Key>(Func<A, Key> key, Comparison<Key> comparer, K<Arr, A> ta)
    {
        var arr = ta.As();
        var cnt = arr.Count;
        if (cnt <= 0) return Arr<A>.Empty;
        if (cnt >= int.MaxValue) throw new ArgumentException("Arr: Foldable.Sort: structure too large");

        var xs  = ta.As().AsSpan();
        var yss = new A[cnt];
        var ys  = new Span<A>(yss);
        xs.CopyTo(ys);

        var ks = ArrayPool<Key>.Shared.Rent((int)cnt);
        var ix = 0L;
        foreach (var x in xs)
        {
            ks[ix] = key(x);
            ix++;
        }

        ks.Sort(ys, comparer);

        ArrayPool<Key>.Shared.Return(ks);

        return new Arr<A>(yss, 0, cnt);
    }

    static Arr<A> Foldable<Arr>.ToArr<A>(K<Arr, A> ta) =>
        ta.As();

    static LE.Lst<A> Foldable<Arr>.ToLst<A>(K<Arr, A> ta) =>
        new(ta.As().AsEnumerable());

    static Iterable<A> Foldable<Arr>.ToIterable<A>(K<Arr, A> ta) =>
        ta.As().AsIterable();

    static LE.Seq<A> Foldable<Arr>.ToSeq<A>(K<Arr, A> ta) =>
        LE.Seq.FromArray(ta.As().ToArray());

    static K<F, K<Arr, B>> Traversable<Arr>.Traverse<F, A, B>(Func<A, K<F, B>> f, K<Arr, A> ta)
    {
        return Foldable.fold(addItem, F.Pure(new SeqStrict<B>(new B[ta.Count], 0, 0, 0, 0)), ta)
                       .Map(bs => new Arr<B>(bs.data, bs.start, bs.Count).Kind());

        K<F, SeqStrict<B>> addItem(K<F, SeqStrict<B>> state, A value) =>
            Applicative.lift((bs, b) => (SeqStrict<B>)bs.Add(b), state, f(value));
    }

    static K<F, K<Arr, B>> Traversable<Arr>.TraverseM<F, A, B>(Func<A, K<F, B>> f, K<Arr, A> ta) =>
        ta.FoldM((bs, a) => f(a).Map(bs.Add), LE.Seq<B>.Empty)
          .Map(bs => create(bs.AsSpan()).Kind());

    static K<Seq, A> Natural<Arr, Seq>.Transform<A>(K<Arr, A> fa) =>
        toSeq(fa.As().ToSeq());

    static K<Iterable, A> Natural<Arr, Iterable>.Transform<A>(K<Arr, A> fa) =>
        fa.As().AsIterable();

    static K<Lst, A> Natural<Arr, Lst>.Transform<A>(K<Arr, A> fa) =>
        toLst(fa.As());

    static K<Set, A> Natural<Arr, Set>.Transform<A>(K<Arr, A> fa) =>
        toSet(fa.As());

    static K<HashSet, A> Natural<Arr, HashSet>.Transform<A>(K<Arr, A> fa) =>
        toHashSet(fa.As());

    public static Iterator<A> ForwardIterator<A>(K<Arr, A> fa)
    {
        var items = +fa;
        return new Iterator<A>.IterArr(items, 0, items.Count);
    }

    public static Iterator<A> BackwardIterator<A>(K<Arr, A> fa)
    {
        var items = +fa;
        return new Iterator<A>.IterArrBkwd(items, items.Count - 1, items.Count);
    }

    static Option<A> Indexable<Arr, long>.At<A>(long index, K<Arr, A> ta)
    {
        var arr = ta.As();
        return index >= 0 && index < arr.Count
                   ? Some(arr[index])
                   : Option<A>.None;
    }

    static Option<A> Indexable<Arr, int>.At<A>(int index, K<Arr, A> ta)
    {
        var arr = ta.As();
        return index >= 0 && index < arr.Count
                   ? Some(arr[index])
                   : Option<A>.None;
    }

    static Option<A> Indexable<Arr, Index>.At<A>(Index index, K<Arr, A> ta)
    {
        var arr = ta.As();
        return index.IsFromEnd
                   ? index.Value > 0 && index.Value <= arr.Count
                         ? Some(arr[arr.Count - index.Value])
                         : None
                   : index.Value >= 0 && index.Value < arr.Count
                       ? Some(arr[index.Value])
                       : None;
    }

    static Option<A> Indexable<Arr, LongIndex>.At<A>(LongIndex index, K<Arr, A> ta)
    {
        var arr = ta.As();
        return index.IsFromEnd
                   ? index.Value > 0 && index.Value <= arr.Count
                         ? Some(arr[arr.Count - index.Value])
                         : None
                   : index.Value >= 0 && index.Value < arr.Count
                       ? Some(arr[index.Value])
                       : None;
    }
*/

}
