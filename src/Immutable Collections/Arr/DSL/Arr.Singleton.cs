using System.Runtime.CompilerServices;
using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace IteratorPrototype.DSL;

class ArrSingleton<A>(A value) : Arr<A>
{
    public readonly A Singleton = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override ref readonly A AtRef(int index)
    {
        if (index == 0)
        {
            return ref Singleton;
        }
        else
        {
           return ref Unsafe.NullRef<A>();
        }
    }
    
    public override object Value =>
        new Cons<Arr, ArrState, A>(Singleton, default);

    public override bool HasValue =>
        true;

    public override bool IsEmpty =>
        false;

    public override int Count => 
        1;
    
    public override Arr<A> Tail =>
        Empty;

    public override Arr<A> Init =>
        Empty;

    public override LE.Option<A> At(Index index) =>
        index.GetOffset(Count) == 0
            ? Singleton
            : default;

    public override bool TryGetValue(out Nil nil)
    {
        nil = default;
        return false;
    }

    public override bool TryGetValue(out A head, out Iterator<Arr, ArrState, A> tail)
    {
        head = Singleton;
        tail = default;
        return true;
    }

    public override ReadOnlySpan<A> AsSpan() =>
        new (in Singleton);

    public override ReadOnlySpan<A> AsSpan(int skip) =>
        skip > 0
            ? ReadOnlySpan<A>.Empty
            : new ReadOnlySpan<A>(in Singleton);

    public override ReadOnlySpan<A> AsSpan(int skip, int take) =>
        skip > 0 || take < 1
            ? ReadOnlySpan<A>.Empty
            : new ReadOnlySpan<A>(in Singleton);

    public override Arr<A> Slice(int skip) =>
        skip > 0
            ? Empty
            : this;

    public override Arr<A> Slice(int skip, int take) =>
        skip > 0 || take < 1
            ? Empty
            : this;

    public override LE.Option<Arr<A>> SetItem(Index index, A val) =>
        index.GetOffset(Count) == 0
            ? new ArrSingleton<A>(val)
            : None;  

    public override Arr<A> Add(in A v)
    {
        var arr = new A[2];
        arr[0] = Singleton;
        arr[1] = v;
        return new ArrMany<A>(arr, 0, 2);
    }

    public override Arr<A> Cons(in A val)
    {
        var arr = new A[2];
        arr[0] = val;
        arr[1] = Singleton;
        return new ArrMany<A>(arr, 0, 2);
    }

    public override Arr<A> AddRange(in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var rcount = range.Length;
        var ncount = 1 + rcount;
        var narray = new A[ncount];
        var nrspan = narray.AsSpan(1);
        
        narray[0] = Singleton;
        range.CopyTo(nrspan);
        
        return new ArrMany<A>(narray, 0, ncount);
    }

    public override Arr<A> ConsRange(in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        var rcount = range.Length;
        var ncount = 1 + rcount;
        var narray = new A[ncount];
        var nrspan = narray.AsSpan();
        
        narray[^1] = Singleton;
        range.CopyTo(nrspan);
        
        return new ArrMany<A>(narray, 0, ncount);
    }

    public override LE.Option<Arr<A>> Insert(Index index, in A val) =>
        index.GetOffset(Count) switch
        {
            0 => new ArrMany<A>([val, Singleton], 0, 2),
            1 => new ArrMany<A>([Singleton, val], 0, 2),
            _ => LE.Option<Arr<A>>.None
        };

    public override LE.Option<Arr<A>> InsertRange(Index index, in ReadOnlySpan<A> range)
    {
        if (range.Length == 0) return this;
        switch (index.GetOffset(Count))
        {
            case 0:
            {
                var narray = new A[range.Length + 1];
                narray[Count] = Singleton;
                range.CopyTo(narray.AsSpan());
                return Arr.create(narray);
            }
            case 1:
            {
                var narray = new A[range.Length + 1];
                narray[0] = Singleton;
                range.CopyTo(narray.AsSpan(1));
                return Arr.create(narray);
            }
            default:
                return None;
        }
    }

    public override Arr<A> RemoveAtHead() =>
        Empty;

    public override Arr<A> RemoveAtLast() =>
        Empty;

    public override Arr<A> RemoveAt(Index index) =>
        index.GetOffset(Count) switch
        {
            0 => Empty,
            _ => this
        };

    public override Arr<A> RemoveAt(ReadOnlySpan<Index> indices)
    {
        foreach (var ix in indices)
        {
            if(ix.GetOffset(Count) == 0)
                return Empty;
        }
        return this;
    }

    public override Arr<A> RemoveRange(in Range range)
    {
        var begin = range.Start.GetOffset(Count);
        var end   = range.End.GetOffset(Count);
        (begin, end) = begin > end 
                           ? (end, begin) 
                           : (begin, end);

        return begin == 0 && end == 1
                   ? Empty
                   : this;
    }

    public override Arr<A> Reverse() =>
        this;

    public override Arr<A> Copy() =>
        new ArrSingleton<A>(Singleton);

    public override Arr<B> Map<B>(Func<A, B> f) =>
        new ArrSingleton<B>(f(Singleton));
        
    public override Arr<B> Bind<B>(Func<A, Arr<B>> f) =>
        f(Singleton);
    
    public override Arr<B> Bind<B>(Func<A, K<Arr, B>> f) =>
        +f(Singleton);

    public override Arr<A> Filter(Func<A, bool> f) =>
        f(Singleton)
            ? this
            : ArrEmpty<A>.Default;

    public override Arr<A> Choose(K<Arr, A> tb) =>
        this;

    public override bool Equals<EqA>(K<Arr, A>? other) =>
        other is ArrSingleton<A> rhs && EqA.Equals(Singleton, rhs.Singleton);

    public override int CompareTo<OrdA>(K<Arr, A>? rhs) =>
        rhs switch
        {
            ArrSingleton<A> single => OrdA.Compare(Singleton, single.Singleton),
            ArrMany<A>             => -1,
            _                      => 1
        };

    public override string ToString() =>
        Singleton?.ToString() ?? "[null]";

    protected override int CalculateHashCode(int offsetBasis = -2128831035)
    {
        var       hash  = offsetBasis;
        const int prime = 16777619;

        unchecked
        {
            return ((Singleton?.GetHashCode() ?? 0) ^ hash) * prime;
        }        
    }
}
