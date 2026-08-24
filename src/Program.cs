using IteratorPrototype;
using IteratorPrototype.Traits;
using static LanguageExt.Prelude;

//IteratorTestSuite.Run();
IteratorTest2.Run();

Bench<CSharpVersion>.Mark();
Bench<CurrentLanguageExtArrVersion>.Mark();
Bench<IterableVersion>.Mark();
Bench<ForeachVersionRef>.Mark();
Bench<ForeachVersionNonRef>.Mark();
Bench<StrongIteratorVersion>.Mark();
Bench<WeakIteratorVersion>.Mark();
Bench<Iterator2Version>.Mark();
Bench<Iterator2ForEachVersion>.Mark();

//Bench<MappedIteratorVersion>.Mark();
//Bench<MonadBindIteratorVersion>.Mark();
Bench.Key();

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the C# array performance 
//  It is the baseline to compare everything else against.
//

public class CSharpVersion : Bench<CSharpVersion>
{
    readonly int[] array = Root.Array.create(..Count).AsSpan().ToArray();

    protected override string Explain =>
        $"Foreach C# array ({Count:N0} items)";

    protected override void Main()
    {
        var total = 0;
        foreach (var x in array)
        {
            total += x;
        }
        ignore(total);
    }

    protected override ConsoleColor Color { get; } =
        Bench.Baseline;
}

//----------------------------------------------------------------------------------------------------------------------

public class CurrentLanguageExtArrVersion : Bench<CurrentLanguageExtArrVersion>
{
    readonly LE.Arr<int> arr = toArray(Arr.create(..Count).AsSpan());

    protected override string Explain =>
        $"Foreach current LanguageExt Arr<A> ({Count:N0} items)";

    protected override void Main()
    {
        var total = 0;
        foreach(var x in arr)
        {
            total += x;
        }
        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Mutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class IterableVersion : Bench<IterableVersion>
{
    readonly Arr<int> array = Arr.create(..Count);

    protected override string Explain =>
        $"Arr Iterable trait implementation ({Count:N0} items)";

    protected override void Main()
    {
        var state = IterableMutable.setup<Arr, ArrState, ArrStateRef, int>(array);
        var total = 0;
        
        while (IterableMutable.step<Arr, ArrState, ArrStateRef, int>(array, ref state, out var x))
        {
            total += x;
        }
        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Mutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class ForeachVersionRef : Bench<ForeachVersionRef>
{
    readonly Arr<int> array = Arr.create(..Count);

    protected override string Explain =>
        $"Ref Struct Foreach Arr<A> ({Count:N0} items)";

    protected override void Main()
    {
        var total = 0;
        foreach(var x in array.reference)
        {
            total += x;
        }
        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Mutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class ForeachVersionNonRef : Bench<ForeachVersionNonRef>
{
    readonly Arr<int> array = Arr.create(..Count);

    protected override string Explain =>
        $"Non-Ref Struct Foreach Arr<A> ({Count:N0} items)";

    protected override void Main()
    {
        var total = 0;
        foreach(var x in array.nonref)
        {
            total += x;
        }
        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Mutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class StrongIteratorVersion : Bench<StrongIteratorVersion>
{
    readonly Iterator<Arr, ArrState, int> iterator = 
        IterableImmutable.from<Arr, ArrState, int>(Arr.create(..Count));

    protected override string Explain =>
        $"Strong Iterator, for Arr, using while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x, out iter))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Immutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class WeakIteratorVersion : Bench<WeakIteratorVersion>
{
    readonly Iterator<int> iterator = 
        Arr.create(..Count).Forward();

    protected override string Explain =>
        $"Weak Iterator, for Arr, using while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x, out iter))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color { get; } =
        Bench.Immutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class Iterator2Version : Bench<Iterator2Version>
{
    readonly Iterator2<int> iterator = 
        Iterator2.from<Arr, ArrState, int>(Arr.create(..Count));

    protected override string Explain =>
        $"Iterator2, for Arr, using while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x, out iter))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Iterator2;
}

public class Iterator2ForEachVersion : Bench<Iterator2ForEachVersion>
{
    readonly Iterator2<int> iterator = 
        Iterator2.from<Arr, ArrState, int>(Arr.create(..Count));

    protected override string Explain =>
        $"Iterator2, for Arr, using foreach ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        foreach (var x in iter)
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Iterator2;
}


/*
//----------------------------------------------------------------------------------------------------------------------

public class MappedIteratorVersion : Bench<MappedIteratorVersion>
{
    readonly Iterator<int> iterator = 
        Arr.create(..Count).Forward().Map(x => x * 2);

    protected override string Explain =>
        $"Mapped Iterator, for Arr, using while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x, out iter))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Immutable;
}

//----------------------------------------------------------------------------------------------------------------------

public class MonadBindIteratorVersion : Bench<MonadBindIteratorVersion>
{
    static readonly Iterator<int> inner = Arr.create(1, 2, 3, 4, 5, 6, 7, 8, 9, 10).Forward();
    
    readonly Iterator<int> iterator = 
        Arr.create(..Count).Forward().Bind(x => inner);

    protected override string Explain =>
        $"Monad bind Iterator, for Arr, using while TryGetValue (10 x {Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x, out iter))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Immutable;
}
*/
