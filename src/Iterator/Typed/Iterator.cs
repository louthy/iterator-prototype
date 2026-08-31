using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    internal readonly MiniStack<IteratorFields<T, IS, A>> fields;

    [MethodImpl(Optimisations.Default)]
    internal Iterator(K<T, A> ta, in IS space)
    {
        var entry = new IteratorFields<T, IS, A>(ta, space);
        fields = MiniStack.singleton(entry);
    }

    [MethodImpl(Optimisations.Default)]
    internal Iterator(K<T, A> ta, IteratorAction<A> action, in IS space)
    {
        var entry = new IteratorFields<T, IS, A>(ta, action, space);
        fields = MiniStack.singleton(entry);
    }

    [MethodImpl(Optimisations.Default)]
    internal Iterator(IteratorFields<T, IS, A> entry) =>
        fields = MiniStack.singleton(entry);

    [MethodImpl(Optimisations.Default)]
    internal Iterator(in MiniStack<IteratorFields<T, IS, A>> fields) =>
        this.fields = fields;

    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(out A head, out Iterator<T, IS, A> tail)
    {
        tail = this; // Copy

        ref var fs  = ref Unsafe.AsRef(in tail.fields);
        ref var top = ref fs.Peek();
        if (top.action is null)
        {
            ref var s = ref Unsafe.AsRef(in top.space);
            return T.StepImmutable(in top.ta, in s, out head, out s);
        }
        else
        {
            ref var a = ref Unsafe.AsRef(in top.action);
            return a.TryGetValue(ref fs.Cast<IteratorFields<T, IS, A>, IteratorFields>(), out head);
        }
    }

    [MethodImpl(Optimisations.Default)]
    internal bool TryGetValueInternal(ref MiniStack<IteratorFields> stack, out A head)
    {
        ref var nstack = ref stack.Cast<IteratorFields, IteratorFields<T, IS, A>>();
        nstack.PushMany(in fields);
        ref var top = ref nstack.Peek();
        
        if (top.action is null)
        {
            ref var s = ref Unsafe.AsRef(in top.space);
            return T.StepImmutable(in top.ta, in s, out head, out s);
        }
        else
        {
            ref var a = ref Unsafe.AsRef(in top.action);
            return a.TryGetValue(ref stack, out head);
        }
    }

    [MethodImpl(Optimisations.Default)]
    internal bool TryGetValueInternal(ref MiniStack<IteratorFields<T, IS, A>> stack, out A head)
    {
        stack.PushMany(in fields);
        ref var top = ref stack.Peek();
        
        if (top.action is null)
        {
            ref var s = ref Unsafe.AsRef(in top.space);
            return T.StepImmutable(in top.ta, in s, out head, out s);
        }
        else
        {
            ref var a = ref Unsafe.AsRef(in top.action);
            return a.TryGetValue(ref stack.Cast<IteratorFields<T, IS, A>, IteratorFields>(), out head);
        }
    }
    
    public Iterator<A> Lower
    {
        [MethodImpl(Optimisations.Default)]
        get => new (in fields.Cast<IteratorFields<T, IS, A>, IteratorFields<A>>());
    }

    [MethodImpl(Optimisations.Default)]
    public Iterator<B> Map<B>(Func<A, B> f)
    {
        var     fieldsA = fields; // copy
        ref var fieldsB = ref fieldsA.Map(f);
        return new Iterator<B>(in fieldsB.Cast<IteratorFields<T, IS, B>, IteratorFields<B>>());
    }

    [MethodImpl(Optimisations.Default)]
    public Iterator<B> Bind<B>(Func<A, Iterator<B>> f)
    {
        var     fieldsA = fields; // copy
        ref var fieldsB = ref fieldsA.Bind(f);
        return new Iterator<B>(in fieldsB.Cast<IteratorFields<T, IS, B>, IteratorFields<B>>());
    }

    [MethodImpl(Optimisations.Default)]
    public Iterator<A> Concat(Iterator<A> rhs)
    {
        var     nf = fields; // copy
        ref var rf = ref nf.Concat(in rhs);
        return new Iterator<A>(in rf.Cast<IteratorFields<T, IS, A>, IteratorFields<A>>());
    }

    [MethodImpl(Optimisations.Default)]
    public Iterator<T, IS, A> Concat(in Iterator<T, IS, A> rhs)
    {
        var     nf = fields; // copy
        ref var rf = ref nf.Concat(in rhs);
        return new Iterator<T, IS, A>(in rf);
    }

    [MethodImpl(Optimisations.Default)]
    public Iterator<T, IS, A> Cons(in A x) =>
        new (new IteratorFields<T, IS, A>(null!, new ConsAction<T, IS, A>(x, this), default));

    [MethodImpl(Optimisations.Default)]
    public IteratorEnumerator<T, IS, A> GetEnumerator() =>
        new (in this);

    [MethodImpl(Optimisations.Default)]
    public static Iterator<A> operator +(Iterator<T, IS, A> xs, Iterator<A> ys) =>
        xs.Concat(ys);

    [MethodImpl(Optimisations.Default)]
    public static Iterator<T, IS, A> operator +(Iterator<T, IS, A> xs, Iterator<T, IS, A> ys) =>
        xs.Concat(ys);

    [MethodImpl(Optimisations.Default)]
    public static Iterator<T, IS, A> operator +(in A x, Iterator<T, IS, A> xs) =>
        xs.Cons(in x);

    /*
    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref IteratorFields stack)
    {
        ref var top = ref fields;
        
        ref readonly var fs = ref fields;
        stack.ta = fs.ta!;
        stack.action = fs.action!;
        stack.space = Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fs.space));
    }

    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref IteratorStack<T, IS, A> stack)
    {
        ref readonly var fs = ref fields;
        stack.ta = fs.ta!;
        stack.action = fs.action!;
        stack.space = fs.space;
    }

    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref MiniStack<IteratorFields> stack) =>
        Prime(ref stack.Peek());

    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref MiniStack<IteratorFields<T, IS, A>> stack) =>
        Prime(ref stack.Peek());

    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref object ta, ref IteratorAction action, ref Space128 space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta!;
        action = fs.action!;
        space = Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fs.space));
    }
    
    [MethodImpl(Optimisations.Default)]
    internal void Prime(ref K<T, A> ta, ref IteratorAction<A> action, ref IS space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta;
        action = fs.action!;
        space = fs.space;
    }
    */
}
