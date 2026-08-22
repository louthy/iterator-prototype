using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator<A>
{
    internal readonly MiniStack<IteratorFields<A>> fields;

    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator(object ta, IteratorAction<A> action, in Space128 space) =>
        fields = new IteratorFields<A>(ta, action, space);
        */

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator(in MiniStack<IteratorFields<A>> fields) =>
        this.fields = fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator(in IteratorFields<A> fields) =>
        this.fields = MiniStack.singleton(fields);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator<A> tail)
    {
        tail = this;    // Copy
        ref var fs = ref Unsafe.AsRef(in tail.fields);
        return fs.GetAction()
                 .TryGetValue(ref fs.Cast<IteratorFields<A>, IteratorFields>(), out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out Cons<A> cons)
    {
        if (TryGetValue(out var head, out var tail))
        {
            cons = new Cons<A>(head, tail);
            return true;
        }
        else
        {
            cons = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out Nil nil) =>
        // TODO: something more efficient
        TryGetValue(out _, out _);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator<A> GetEnumerator() =>
        new (in this);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<B> Map<B>(Func<A, B> f)
    {
        var fs = fields; // copy
        return new (fs.Map(f));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<B> Bind<B>(Func<A, Iterator<B>> f)
    {
        var fs = fields; // copy
        return new (fs.Bind(f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<A> Concat(in Iterator<A> rhs)
    {
        var fs = fields; // copy
        return new(fs.Concat(in rhs));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<A> Cons(in A x) =>
        new (new IteratorFields<A>(null!, new ConsAction<A>(x, this), default));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator<A> operator +(Iterator<A> xs, Iterator<A> ys) =>
        xs.Concat(ys);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator<A> operator +(A x, Iterator<A> xs) =>
        xs.Cons(x);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref MiniStack<IteratorFields> stack)
    {
        stack.Pop();
        stack.PushMany(in fields.Cast<IteratorFields<A>, IteratorFields>());
    }
}
