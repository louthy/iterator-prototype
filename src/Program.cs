using IteratorPrototype;
using IteratorPrototype.Traits;
using static LanguageExt.Prelude;

TestSuite.Run();

Bench<CSharpVersion>.Mark();
Bench<CurrentLanguageExtArrVersion>.Mark();
Bench<IterableVersion>.Mark();
Bench<ForeachVersionRef>.Mark();
Bench<ForeachVersionNonRef>.Mark();
Bench<StrongIteratorVersion>.Mark();
Bench<WeakIteratorVersion>.Mark();
Bench<StrongIterator2Version>.Mark();
Bench<WeakIterator2Version>.Mark();
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
//
//  Current LanguageExt Arr<A> foreach version 
//
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
//
//  This tests the Iterable trait performance 
//
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
//
//  This tests the performance of a generalised foreach using the IteratorEnumerator 
//
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
//
//  This tests the performance of a generalised foreach using the IteratorEnumerator 
//
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
//
//  This tests the performance of a generalised Iterator 
//
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
//
//  This tests the performance of a generalised Iterator 
//
public class WeakIteratorVersion : Bench<WeakIteratorVersion>
{
    readonly Iterator<int> iterator = 
        IterableImmutable.fromWeak<Arr, ArrState, int>(Arr.create(..Count));

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
//
//  This tests the performance of a generalised Iterator2 Strong
//

public class StrongIterator2Version : Bench<StrongIterator2Version>
{
    readonly Iterator2<Arr, ArrState, int> iterator = 
        Iterator2.from<Arr, ArrState, int>(Arr.create(..Count));

    protected override string Explain =>
        $"Strong Iterator2, for Arr, using while TryGetValue ({Count:N0} items)";

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
//
//  This tests the performance of a generalised Iterator2 Weak
//

public class WeakIterator2Version : Bench<WeakIterator2Version>
{
    readonly Iterator2<int> iterator = 
        Iterator2.fromWeak<Arr, ArrState, int>(Arr.create(..Count));

    protected override string Explain =>
        $"Weak Iterator2, for Arr, using while TryGetValue ({Count:N0} items)";

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
