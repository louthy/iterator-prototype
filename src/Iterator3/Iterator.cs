using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static class Iterator
{
    public static Iterator<IS, A> from<T, IS, A>(K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var cont = Cont.iterable<T, IS, A>(ta);
        return new Iterator<IS, A>(cont, T.SetupImmutable(ta));
    }
    
    public static void Tests()
    {
        Test1();
        Test2();
        Test3();
        Test4();
    }
    
    public static void Test1()
    {
        var arr   = Arr.create(1..6);
        var iter1 = from<Arr, ArrState, int>(arr);
        var iter  = iter1.Prepend(0);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
    
    public static void Test2()
    {
        var arr   = Arr.create("One", "Two", "Three", "Four", "Five");
        var iter1 = from<Arr, ArrState, string>(arr);
        var iter  = iter1.Prepend("Zero");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
        
    public static void Test3()
    {
        var arr  = Arr.create(1..6);
        var iter = from<Arr, ArrState, int>(arr).Map(x => $"Item {x}");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }    
        
    public static void Test4()
    {
        var arr  = Arr.create(1..4);
        
        var iter1 = from<Arr, ArrState, int>(arr);
        var iter  = iter1.Bind(x => iter1.Map(y => x * y));
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
}

public record Iterator<S, A>(Cont<S, A> cont, S state)
{
    public bool TryGetValue(out A head, out Iterator<S, A> tail)
    {
        if (cont.TryGetValue(state, out head, out var next, out var nextState))
        {
            tail = new Iterator<S, A>(next, nextState);
            return true;
        }
        else
        {
            tail = null!;
            return false;
        }
    }
    
    public Cont<S, A> Await => 
        Cont.constant(state, cont);
    
    public Iterator<S, A> Prepend(A value) =>
        new (Cont.yield(value, cont), state);

    public Iterator<S, B> Map<B>(Func<A, B> f) =>
        new (cont.Map(f), state);

    public Iterator<S, B> Bind<B>(Func<A, Iterator<S, B>> f) =>
        new (cont.Bind(x => f(x).Await), state);

    public IEnumerable<A> AsEnumerable() =>
        Await.AsEnumerable(state);
}

public static class Cont
{
    public static Cont<S, A> constant<S, A>(S state, Cont<S, A> next) =>
        new ConstantCont<S, A>(state, next);
    
    public static Cont<S, A> yield<S, A>(Func<S, Cont<S, A>> next) =>
        new YieldLazy<S, A>(next);
    
    public static Cont<S, A> yield<S, A>(A value, Func<S, Cont<S, A>> next) =>
        new YieldCont<S, A>(value, next);
    
    public static Cont<S, A> yield<S, A>(A value, Cont<S, A> next) =>
        new YieldCont2<S, A>(value, next);
    
    public static Cont<S, A> flatten<S, A>(Cont<S, Cont<S, A>> continuations) =>
        new FlattenCont<S, A>(continuations);
    
    public static Cont<S, B> bind<S, A, B>(Cont<S, A> sa, Func<A, Cont<S, B>> f) =>
        new BindCont<S, A, B>(sa, f);
    
    public static Cont<S, A> concat<S, A>(Cont<S, A> left, Cont<S, A> right) =>
        new ConcatCont<S, A>(left, right);
    
    public static Cont<IS, A> iterable<T, IS, A>(K<T, A> ta) 
        where T : Tr.IterableImmutable<T, IS> 
        where IS : unmanaged =>
        new IterableCont<T, IS, A>(ta);
    
    public static ContBreak @break =>
        default;    
}

public readonly struct ContBreak;

public abstract record Cont<S, A>
{
    public abstract bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S nextState);
    
    public Cont<S, B> Map<B>(Func<A, B> f) =>
        new MapCont<S, A, B>(this, f);
    
    public Cont<S, B> Bind<B>(Func<A, Cont<S, B>> f) =>
        new BindCont<S, A, B>(this, f);
    
    public IEnumerable<A> AsEnumerable(S state)
    {
        var iter = this;
        while (iter.TryGetValue(in state, out var head, out iter, out state))
        {
            yield return head;
        }
    } 
    
    public static implicit operator Cont<S, A>(ContBreak _) =>
        new BreakCont<S, A>();
}

public record YieldCont<S, A>(A Value, Func<S, Cont<S, A>> Next) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S nextState)
    {
        head = Value;
        tail = Next(state);
        nextState = state;
        return true;
    }
}

public record YieldCont2<S, A>(A Value, Cont<S, A> Next) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S nextState)
    {
        head = Value;
        tail = Next;
        nextState = state;
        return true;
    }
}

public record YieldLazy<S, A>(Func<S, Cont<S, A>> Next) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S nextState) =>
        Next(state).TryGetValue(in state, out head, out tail, out nextState);
}

public record ConstantCont<S, A>(S state, Cont<S, A> Next) : Cont<S, A>
{
    public override bool TryGetValue(in S _, out A head, out Cont<S, A> tail, out S nextState) =>
        Next.TryGetValue(state, out head, out tail, out nextState);
}

public record MapCont<S, A, B>(Cont<S, A> ca, Func<A, B> f) : Cont<S, B>
{
    public override bool TryGetValue(in S state, out B head, out Cont<S, B> tail, out S nextState)
    {
        if (ca.TryGetValue(in state, out var ha, out var ta, out nextState))
        {
            head = f(ha);
            tail = new MapCont<S, A, B>(ta, f);
            return true;
        }
        else
        {
            head = default!;
            tail = Cont.@break;
            return false;
        }
    }
}

public record BindCont<S, A, B>(Cont<S, A> sa, Func<A, Cont<S, B>> f) : Cont<S, B>
{
    public override bool TryGetValue(in S state, out B head, out Cont<S, B> tail, out S nextState)
    {
        if (sa.TryGetValue(in state, out var a, out var ta, out nextState))
        {
            return Cont.concat(f(a), Cont.constant(nextState, Cont.bind(ta, f)))
                       .TryGetValue(in nextState, out head, out tail, out nextState);
        }
        else
        {
            head = default!;
            tail = Cont.@break;
            return false;
        }
    }
}
public record FlattenCont<S, A>(Cont<S, Cont<S, A>> Many) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S nextState)
    {
        if(Many.TryGetValue(in state, out var first, out var remaining, out nextState))
        {
            return Cont.concat(first, Cont.constant(nextState, Cont.flatten(remaining)))
                       .TryGetValue(in nextState, out head, out tail, out nextState);
        }
        else
        {
            head = default!;
            tail = Cont.@break;
            return false;
        }
    }
}

public record ConcatCont<S, A>(Cont<S, A> Head, Cont<S, A> Tail) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S outState)
    {
        if (Head.TryGetValue(in state, out head, out var t, out outState))
        {
            tail = new ConcatCont<S, A>(t, Tail);
            return true;
        }
        else
        {
            return Tail.TryGetValue(in state, out head, out tail, out outState);
        }
    }
}

public record BreakCont<S, A> : Cont<S, A>
{
    public override bool TryGetValue(in S state, out A head, out Cont<S, A> tail, out S outState)
    {
        head = default!;
        tail = new BreakCont<S, A>();
        outState = state;
        return false;
    }
}

public record IterableCont<T, IS, A>(K<T, A> ta) : Cont<IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public override bool TryGetValue(in IS state, out A head, out Cont<IS, A> tail, out IS nextState)
    {
        if (T.StepImmutable(ta, state, out head, out nextState))
        {
            tail = this;
            return true;
        }
        else
        {
            tail = Cont.@break;
            return false;
        }
    }
}
