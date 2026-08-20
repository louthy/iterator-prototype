using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator<T, IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : struct
{
    internal readonly IteratorFields<T, IS, A> fields;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator(K<T, A> ta, in IS space) =>
        fields = new IteratorFields<T, IS, A>(ta, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Iterator(K<T, A> ta, IteratorAction<A> action, in IS space) =>
        fields = new IteratorFields<T, IS, A>(ta, action, space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator<T, IS, A> tail)
    {
        tail = this; // Copy
        ref var fs = ref Unsafe.AsRef(in tail.fields);
        ref var ta = ref Unsafe.As<K<T, A>, object>(ref Unsafe.AsRef(in fs.ta));

        if (fs.action is null)
        {
            ref var s = ref Unsafe.AsRef(in fs.space);
            return T.Next(in fields.ta, ref s, out head);
        }
        else
        {
            ref var a = ref Unsafe.As<IteratorAction<A>, IteratorAction>(ref Unsafe.AsRef(in fs.action));
            ref var s = ref Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fs.space));

            var stack = new IteratorStack(ref ta, ref a, ref s);
            return fs.action.TryGetValue(ref stack, out head);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal bool TryGetValueInternal(ref IteratorStack stack, out A head)
    {
        ref var fs = ref Unsafe.AsRef(in fields);
        ref var ts = ref Unsafe.As<Space128, IS>(ref stack.space);
        
        stack.ta = fs.ta;
        stack.action = fs.action!;
        ts = fs.space;
        
        return fs.action is null 
                   ? T.Next(in fs.ta, ref ts, out head) 
                   : fs.action.TryGetValue(ref stack, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal bool TryGetValueInternal(ref IteratorStack<T, IS, A> stack, out A head)
    {
        ref var fs = ref Unsafe.AsRef(in fields);
        ref var ts = ref stack.space;
        stack.ta = fs.ta;
        stack.action = fs.action!;
        ts = fs.space;
        
        return stack.action is null 
                   ? T.Next(in stack.ta, ref stack.space, out head) 
                   : stack.action.TryGetValue(ref IteratorStack.From(ref stack), out head);
    }
    
    public Iterator<A> Lower
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => new (fields.ta,
                    fields.action ?? PureAction<T, IS, A>.Default,
                    in Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fields.space)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iterator<B> Map<B>(Func<A, B> f) =>
        new Iterator2<T, IS, A, B>(
            fields.ta, 
            (fields.action ?? PureAction<T, IS, A>.Default).Map(f), 
            fields.space)
           .Lower;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public IteratorEnumerator<T, IS, A> GetEnumerator() =>
        new (in this);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Iterator<T, IS, A> operator+(A x, Iterator<T, IS, A> xs) =>
        new (xs.fields.ta, (xs.fields.action ?? PureAction<T, IS, A>.Default).Cons(x), xs.fields.space);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref IteratorStack stack)
    {
        ref readonly var fs = ref fields;
        stack.ta = fs.ta!;
        stack.action = fs.action!;
        stack.space = Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fs.space));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref IteratorStack<T, IS, A> stack)
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
        space = Unsafe.As<IS, Space128>(ref Unsafe.AsRef(in fs.space));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Prime(ref K<T, A> ta, ref IteratorAction<A> action, ref IS space)
    {
        ref readonly var fs = ref fields;
        ta = fs.ta;
        action = fs.action!;
        space = fs.space;
    }
}
