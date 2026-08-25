using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static class Iterator
{
    public static Iterator<A> from<T, IS, A>(K<T, A> ta) 
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        new IterableSource<T, IS, A>(ta, T.SetupImmutable(ta));

    
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

public abstract record Iterator<A>
{
    public bool TryGetValue(out A head, out Iterator<A> tail)
    {
        if (Await().TryGetValue(out head, out var next))
        {
            tail = new ContSource<A>(next);
            return true;
        }
        else
        {
            tail = null!;
            return false;
        }
    }
    
    public abstract Cont<A> Await();

    public Iterator<A> Prepend(A value) =>
        new ConsSource<A>(value, this);

    public Iterator<B> Map<B>(Func<A, B> f) =>
        new MapIterator<A, B>(this, f);

    public Iterator<B> Bind<B>(Func<A, Iterator<B>> f) =>
        new BindIterator<A, B>(this, f);

    public IEnumerable<A> AsEnumerable() =>
        Await().AsEnumerable();
}

public static class Cont
{
    public static Cont<A> yield<A>(Func<Cont<A>> next) =>
        new YieldLazy<A>(next);
    
    public static Cont<A> yield<A>(A value, Func<Cont<A>> next) =>
        new YieldCont<A>(value, next);
    
    public static Cont<A> flatten<A>(Cont<Cont<A>> continuations) =>
        new YieldFlattenCont<A>(continuations);
    
    public static Cont<A> concat<A>(Cont<A> left, Cont<A> right) =>
        new ConcatCont<A>(left, right);
    
    public static ContBreak @break =>
        default;    
}

public readonly struct ContBreak;

public abstract record Cont<A>
{
    public abstract Cont<B> Map<B>(Func<A, B> f);
    public abstract bool TryGetValue(out A head, out Cont<A> tail);
    
    public IEnumerable<A> AsEnumerable()
    {
        var iter = this;
        while (iter.TryGetValue(out var head, out iter))
        {
            yield return head;
        }
    } 
    
    public static implicit operator Cont<A>(ContBreak _) =>
        new BreakCont<A>();
}

public record YieldCont<A>(A Value, Func<Cont<A>> Next) : Cont<A>
{
    public override Cont<B> Map<B>(Func<A, B> f) =>
        Cont.yield(f(Value), () => Next().Map(f));

    public override bool TryGetValue(out A head, out Cont<A> tail)
    {
        head = Value;
        tail = Next();
        return true;
    }
}

public record YieldLazy<A>(Func<Cont<A>> Next) : Cont<A>
{
    public override Cont<B> Map<B>(Func<A, B> f) =>
        Cont.yield(() => Next().Map(f));

    public override bool TryGetValue(out A head, out Cont<A> tail) =>
        Next().TryGetValue(out head, out tail);
}

public record YieldFlattenCont<A>(Cont<Cont<A>> Many) : Cont<A>
{
    public override Cont<B> Map<B>(Func<A, B> f) =>
        Cont.flatten(Many.Map(cx => cx.Map(f)));

    public override bool TryGetValue(out A head, out Cont<A> tail)
    {
        if(Many.TryGetValue(out var first, out var remaining))
        {
            return Cont.concat(first, new YieldFlattenCont<A>(remaining))
                       .TryGetValue(out head, out tail);
        }
        else
        {
            head = default!;
            tail = Cont.@break;
            return false;
        }
    }
}

public record ConcatCont<A>(Cont<A> Head, Cont<A> Tail) : Cont<A>
{
    public override Cont<B> Map<B>(Func<A, B> f) =>
        new ConcatCont<B>(Head.Map(f), Tail.Map(f));

    public override bool TryGetValue(out A head, out Cont<A> tail)
    {
        if (Head.TryGetValue(out head, out var t))
        {
            tail = new ConcatCont<A>(t, Tail);
            return true;
        }
        else
        {
            return Tail.TryGetValue(out head, out tail);
        }
    }
}

public record BreakCont<A> : Cont<A>
{
    public override Cont<B> Map<B>(Func<A, B> f) =>
        new BreakCont<B>();

    public override bool TryGetValue(out A head, out Cont<A> tail)
    {
        head = default!;
        tail = new BreakCont<A>();
        return false;
    }
}

record ContSource<A>(Cont<A> cont) : Iterator<A>
{
    public override Cont<A> Await() =>
        cont;
} 

record IterableSource<T, IS, A>(K<T, A> ta, IS state) : Iterator<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public override Cont<A> Await()
    {
        if (T.StepImmutable(ta, state, out var head, out var newState))
        {
            return Cont.yield(head, () => new IterableSource<T, IS, A>(ta, newState).Await());
        }
        else
        {
            return Cont.@break;
        }
    }
} 

record ConsSource<A>(A head, Iterator<A> tail) : Iterator<A>
{
    public override Cont<A> Await() =>
        Cont.yield(head, () => tail.Await());
} 

record MapIterator<A, B>(Iterator<A> source, Func<A, B> f) : Iterator<B>
{
    public override Cont<B> Await() =>
        source.Await().Map(f);
}

record BindIterator<A, B>(Iterator<A> source, Func<A, Iterator<B>> f) : Iterator<B>
{
    public override Cont<B> Await() =>
        Cont.flatten(source.Await().Map(b => f(b).Await()));
}