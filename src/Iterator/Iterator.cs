using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator<A>
{
    internal readonly MiniStack<IteratorFields<A>> fields;

    /*
    [MethodImpl(Optimisations.Default)]
    internal Iterator(object ta, IteratorAction<A> action, in Space128 space) =>
        fields = new IteratorFields<A>(ta, action, space);
        */

    [MethodImpl(Optimisations.Default)]
    internal Iterator(in MiniStack<IteratorFields<A>> fields) =>
        this.fields = fields;

    [MethodImpl(Optimisations.Default)]
    internal Iterator(in IteratorFields<A> fields) =>
        this.fields = MiniStack.singleton(fields);
    
    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(out A head, out Iterator<A> tail)
    {
        tail = this;    // Copy
        ref var fs = ref Unsafe.AsRef(in tail.fields);
        return fs.GetAction()
                 .TryGetValue(ref fs.Cast<IteratorFields<A>, IteratorFields>(), out head);
    }

    [MethodImpl(Optimisations.Default)]
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

    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(out Nil nil) =>
        // TODO: something more efficient
        TryGetValue(out _, out _);
    
    [MethodImpl(Optimisations.Default)]
    public IteratorEnumerator<A> GetEnumerator() =>
        new (in this);

    [MethodImpl(Optimisations.Default)]
    public Iterator<B> Map<B>(Func<A, B> f)
    {
        var fs = fields; // copy
        return new (fs.Map(f));
    }
    
    [MethodImpl(Optimisations.Default)]
    public Iterator<B> Bind<B>(Func<A, Iterator<B>> f)
    {
        var fs = fields; // copy
        return new (fs.Bind(f));
    }

    [MethodImpl(Optimisations.Default)]
    public Iterator<A> Concat(in Iterator<A> rhs)
    {
        var fs = fields; // copy
        return new(fs.Concat(in rhs));
    }
    
    [MethodImpl(Optimisations.Default)]
    public Iterator<A> Cons(in A x) =>
        new (new IteratorFields<A>(null!, new ConsAction<A>(x, this), default));

    [MethodImpl(Optimisations.Default)]
    public static Iterator<A> operator +(Iterator<A> xs, Iterator<A> ys) =>
        xs.Concat(ys);

    [MethodImpl(Optimisations.Default)]
    public static Iterator<A> operator +(A x, Iterator<A> xs) =>
        xs.Cons(x);

    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref MiniStack<IteratorFields> stack)
    {
        stack.Pop();
        stack.PushMany(in fields.Cast<IteratorFields<A>, IteratorFields>());
    }
}
