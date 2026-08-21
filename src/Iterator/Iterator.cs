using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator<A>
{
    internal readonly IteratorFields<A> fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator(object ta, IteratorAction<A> action, in Space128 space) =>
        fields = new IteratorFields<A>(ta, action, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator<A> tail)
    {
        tail = this;    // Copy

        var stack = new MiniStack<IteratorStack>();
        
        var entry = new IteratorStack(
            ref Unsafe.AsRef(in tail.fields.ta), 
            ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in tail.fields.action)), 
            ref Unsafe.AsRef(in tail.fields.space));
        
        stack.Push(in entry);
        
        return fields.action.TryGetValue(ref stack, out head);
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
    public Iterator<B> Map<B>(Func<A, B> f) =>
        new (fields.ta, fields.action.Map(f), fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<B> Bind<B>(Func<A, Iterator<B>> f) =>
        new (fields.ta, fields.action.Bind(f), fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<A> Concat(Iterator<A> rhs) =>
        new (fields.ta, fields.action.Concat(rhs), fields.space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator<A> operator +(Iterator<A> xs, Iterator<A> ys) =>
        xs.Concat(ys);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator<A> operator +(A x, Iterator<A> xs) =>
        new(xs.fields.ta, xs.fields.action.Cons(x), xs.fields.space);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref object ta, ref Space128 space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta!;
        space = fs.space;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref MiniStack<IteratorStack> stack) =>
        Prime(ref stack.Peek());
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref IteratorStack stack)
    {
        ref readonly var fs = ref fields;
        stack.ta = fs.ta!;
        stack.action = fs.action!;
        stack.space = fs.space;
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
