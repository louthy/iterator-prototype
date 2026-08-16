using System.Runtime.CompilerServices;
using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace IteratorPrototype.DSL;

class ArrEmpty<A> : Arr<A>
{
    public static readonly Arr<A> Default = new ArrEmpty<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override ref readonly A AtRef(int index) => 
        ref Unsafe.NullRef<A>();
    
    public override bool HasValue =>
        true;

    public override object? Value =>
        Nil.Obj;

    public override bool IsEmpty =>
        true;
    
    public override int Count => 
        0;

    public override Arr<A> Tail =>
        this;

    public override Arr<A> Init =>
        this;

    public override LE.Option<A> At(Index index) =>
        default;

    public override bool TryGetValue(out Nil nil)
    {
        nil = default;
        return true;
    }

    public override bool TryGetValue(out A head, out Iterator<Arr, ArrState, A> tail)
    {
        head = default!;
        tail = default!;
        return false;
    }

    public override ReadOnlySpan<A> AsSpan() =>
        ReadOnlySpan<A>.Empty;

    public override ReadOnlySpan<A> AsSpan(int skip) =>
        ReadOnlySpan<A>.Empty;

    public override ReadOnlySpan<A> AsSpan(int skip, int take) =>
        ReadOnlySpan<A>.Empty;
    
    public override Arr<A> Slice(int skip) =>
        this;

    public override Arr<A> Slice(int skip, int take) =>
        this;

    public override LE.Option<Arr<A>> SetItem(Index index, A val) =>
        None;

    public override Arr<A> Add(in A value) =>
        new ArrSingleton<A>(value);

    public override Arr<A> Cons(in A value) =>
        new ArrSingleton<A>(value);

    public override Arr<A> AddRange(in ReadOnlySpan<A> range) =>
        range.Length switch
        {
            0 => Empty,
            1 => new ArrSingleton<A>(range[0]),
            _ => new ArrMany<A>([.. range], 0, range.Length),
        };

    public override Arr<A> ConsRange(in ReadOnlySpan<A> range) =>
        AddRange(in range);

    public override LE.Option<Arr<A>> Insert(Index index, in A value) =>
        index.GetOffset(Count) switch
        {
            0 => new ArrSingleton<A>(value),
            _ => None
        };
    
    public override LE.Option<Arr<A>> InsertRange(Index index, in ReadOnlySpan<A> range) =>
        range.Length == 0
            ? this
            : index.GetOffset(Count) switch
              {
                  0 => Arr.create(range),
                  _ => None
              };

    public override Arr<A> RemoveAtHead() =>
        this;

    public override Arr<A> RemoveAtLast() =>
        this;

    public override Arr<A> RemoveAt(Index index) =>
        this;
    
    public override Arr<A> RemoveAt(ReadOnlySpan<Index> indices) =>
        this;
    
    public override Arr<A> RemoveRange(in Range range) =>
        this;
    
    public override Arr<A> Copy() =>
        this;

    public override Arr<A> Reverse() =>
        this;

    public override Arr<B> Map<B>(Func<A, B> f) =>
        ArrEmpty<B>.Default;
    
    public override Arr<B> Bind<B>(Func<A, Arr<B>> f) =>
        ArrEmpty<B>.Default;
    
    public override Arr<B> Bind<B>(Func<A, K<Arr, B>> f) =>
        ArrEmpty<B>.Default;
    
    public override Arr<A> Filter(Func<A, bool> f) =>
        this;

    public override Arr<A> Choose(K<Arr, A> tb) =>
        +tb;

    public override bool Equals<EqA>(K<Arr, A>? other) =>
        other is ArrEmpty<A>;

    public override int CompareTo<OrdA>(K<Arr, A>? other) =>
        other switch
        {
            null        => 0,
            ArrEmpty<A> => 0,
            _           => 1
        };
    
    public override string ToString() =>
        "[]";

    protected override int CalculateHashCode(int offsetBasis = -2128831035) =>
        offsetBasis;

}