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
        if (cont.TryGetValue(state, out var nextState, out head, out var next))
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
        new (Cont.map(cont, f), state);

    public Iterator<S, B> Bind<B>(Func<A, Iterator<S, B>> f) =>
        new (Cont.bind(cont, f), state);

    public IEnumerable<A> AsEnumerable() =>
        Await.AsEnumerable(state);
}

public static class Cont
{
    public static Cont<S, A> constant<S, A>(S state, Cont<S, A> next) =>
        new ConstantCont<S, A>(state, next);
    
    public static Cont<S, A> yield<S, A>(A value, Cont<S, A> next) =>
        new YieldCont<S, A>(value, next);
    
    public static Cont<S, B> map<S, A, B>(Cont<S, A> sa, Func<A, B> f) =>
        new MapCont<S, A, B>(sa, f);
    
    public static Cont<S, A> flatten<S, A>(Cont<S, Cont<S, A>> continuations) =>
        new FlattenCont<S, A>(continuations);

    public static Cont<S, A> flatten<S, A>(Cont<S, Iterator<S, A>> continuations) =>
        new FlattenIteratorCont<S, A>(continuations);

    public static Cont<S, B> bind<S, A, B>(Cont<S, A> sa, Func<A, Cont<S, B>> f) =>
        flatten(map(sa, f));

    public static Cont<S, B> bind<S, A, B>(Cont<S, A> sa, Func<A, Iterator<S, B>> f) =>
        flatten(map(sa, f));
    
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
    public abstract bool TryGetValue(in S state, out S nextState, out A head, out Cont<S, A> tail);
    
    public IEnumerable<A> AsEnumerable(S state)
    {
        var iter = this;
        while (iter.TryGetValue(in state, out state, out var head, out iter))
        {
            yield return head;
        }
    } 
    
    public static implicit operator Cont<S, A>(ContBreak _) =>
        BreakCont<S, A>.Default;
}

record YieldCont<S, A>(A value, Cont<S, A> next) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out S nextState, out A head, out Cont<S, A> tail)
    {
        head = value;
        tail = next;
        nextState = state;
        return true;
    }
}

record ConstantCont<S, A>(S state, Cont<S, A> next) : Cont<S, A>
{
    public override bool TryGetValue(in S _, out S nextState, out A head, out Cont<S, A> tail) =>
        next.TryGetValue(state, out nextState, out head, out tail);
}

record MapCont<S, A, B>(Cont<S, A> ca, Func<A, B> f) : Cont<S, B>
{
    public override bool TryGetValue(in S state, out S nextState, out B head, out Cont<S, B> tail)
    {
        if (ca.TryGetValue(in state, out nextState, out var ha, out var ta))
        {
            head = f(ha);
            tail = Cont.map(ta, f);
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

record FlattenCont<S, A>(Cont<S, Cont<S, A>> many) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out S nextState, out A head, out Cont<S, A> tail)
    {
        if(many.TryGetValue(in state, out nextState, out var first, out var remaining))
        {
            return Cont.concat(first, Cont.constant(nextState, Cont.flatten(remaining)))
                       .TryGetValue(in nextState, out nextState, out head, out tail);
        }
        else
        {
            head = default!;
            tail = Cont.@break;
            return false;
        }
    }
}

record FlattenIteratorCont<S, A>(Cont<S, Iterator<S, A>> many) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out S nextState, out A head, out Cont<S, A> tail)
    {
        if(many.TryGetValue(in state, out nextState, out var first, out var remaining))
        {
            return Cont.concat(first.Await, Cont.constant(nextState, Cont.flatten(remaining)))
                       .TryGetValue(in nextState, out nextState, out head, out tail);
        }
        else
        {
            head = default!;
            tail = Cont.@break;
            return false;
        }
    }
}

record ConcatCont<S, A>(Cont<S, A> first, Cont<S, A> second) : Cont<S, A>
{
    public override bool TryGetValue(in S state, out S nextState, out A head, out Cont<S, A> tail)
    {
        if (first.TryGetValue(in state, out nextState, out head, out var t))
        {
            tail = new ConcatCont<S, A>(t, second);
            return true;
        }
        else
        {
            return second.TryGetValue(in state, out nextState, out head, out tail);
        }
    }
}

record BreakCont<S, A> : Cont<S, A>
{
    public static readonly Cont<S, A> Default = new BreakCont<S, A>();
    
    public override bool TryGetValue(in S state, out S nextState, out A head, out Cont<S, A> tail)
    {
        head = default!;
        tail = Default;
        nextState = state;
        return false;
    }
}

record IterableCont<T, IS, A>(K<T, A> ta) : Cont<IS, A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public override bool TryGetValue(in IS state, out IS nextState, out A head, out Cont<IS, A> tail)
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
