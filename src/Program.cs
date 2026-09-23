// ReSharper disable VirtualMemberCallInConstructor

using IteratorPrototype;
using IteratorPrototype.Iterator3;
using IteratorPrototype.Iterator4;
using IteratorPrototype.Memory;
using IteratorPrototype.Traits;
using IteratorPrototype.Types;
using static LanguageExt.Prelude;
using static IteratorPrototype.Iterator3.Iter;

Console.WriteLine(Ty<ConsoleColor>.Pretty);
    

//IteratorTestSuite.Run();
//IteratorTest2.Run();
//IteratorPrototype.Iterator3.Iterator.Tests();
IterTests.Tests();
//IterTests4.Tests();

/*
Bench<CSharpVersion>.Mark();
Bench<CurrentLanguageExtArrVersion>.Mark();
Bench<IterableVersion>.Mark();
Bench<ForeachVersionRef>.Mark();
Bench<ForeachVersionNonRef>.Mark();
Bench<StrongIteratorVersion>.Mark();
Bench<WeakIteratorVersion>.Mark();
Bench<Iterator2Version>.Mark();
Bench<Iterator2ForEachVersion>.Mark();
*/
Bench<Iterator3Version>.Mark();
//Bench<IterApplyTest>.Mark();
//Bench<IterBindTest>.Mark();
//Bench<IterBoxingTest>.Mark();

//Bench<SekAddTest>.Mark();
//Bench<TreeListAddTest>.Mark();

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
    readonly int[] array;

    public CSharpVersion() =>
        array = Root.Array.create(..Count).AsSpan().ToArray();
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly LE.Arr<int> arr;

    public CurrentLanguageExtArrVersion() =>
        arr = toArray(Arr.create(..Count).AsSpan());
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Arr<int> array;

    public IterableVersion() =>
        array = Arr.create(..Count);
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Arr<int> array;
    
    public ForeachVersionRef() =>
        array = Arr.create(..Count);
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Arr<int> array;
    
    public ForeachVersionNonRef() =>
        array = Arr.create(..Count);
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Iterator<Arr, ArrState, int> iterator; 

    public StrongIteratorVersion() =>
        iterator = IterableImmutable.from<Arr, ArrState, int>(Arr.create(..Count));
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Root.Iterator<int> iterator;

    public WeakIteratorVersion() =>
        iterator = Arr.create(..Count).Forward();
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Iterator2<int> iterator;
    
    public Iterator2Version() =>
        iterator = Iterator2.from<Arr, ArrState, int>(Arr.create(..Count));
    
    protected override int Count { get; } = 
        DefaultCount;

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
    readonly Iterator2<int> iterator;

    public Iterator2ForEachVersion() =>
        iterator = Iterator2.from<Arr, ArrState, int>(Arr.create(..Count));
    
    protected override int Count { get; } = 
        DefaultCount;

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

//----------------------------------------------------------------------------------------------------------------------

public class Iterator3Version : Bench<Iterator3Version>
{
    readonly Iter<int> iterator;

    public Iterator3Version() =>
        iterator = from<Arr, ArrState, int>(Arr.create(..Count));
    
    protected override int Count { get; } = 
        DefaultCount;

    protected override string Explain =>
        $"Iter3, for Arr, using while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Iterator3;
}

//----------------------------------------------------------------------------------------------------------------------

public class IterApplyTest : Bench<IterApplyTest>
{
    readonly Iter<int> iterator;
    
    const int X = 100000;
    const int Y = 10;
    
    public IterApplyTest()
    {
        iterator = from<Arr, ArrState, int>(Arr.create(..X))
                 * from<Arr, ArrState, int>(Arr.create(..Y))
                 | select((int x, int y) => x + y);
    }
    
    protected override int Count => 
        X * Y;

    protected override string Explain =>
        "Applicative apply of two iterators followed by map";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x))
        {
            total += x;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Iterator3;
}

//----------------------------------------------------------------------------------------------------------------------

public class IterBindTest : Bench<IterBindTest>
{
    readonly Iter<int> iterator;
    
    const int X = 1000;
    const int Y = 1000;
    
    public IterBindTest()
    {
        var tx = from<Arr, ArrState, int>(Arr.create(..X));
        var ty = from<Arr, ArrState, int>(Arr.create(..Y));
        
        iterator = tx >> bind((int _) => ty);
    }
    
    protected override int Count => 
        X * Y;

    protected override string Explain =>
        "Monad bind of two iterators";

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
        Bench.Iterator3;
}

//----------------------------------------------------------------------------------------------------------------------

public class IterBoxingTest : Bench<IterBoxingTest>
{
    const int X = 333;
    const int Y = 333;
    string[] labels = ["One", "Two", "Three"];
    
    // Structs with managed members
    readonly Iter<(int, int, string)> iterator;

    public IterBoxingTest() =>
        iterator = from<Arr, ArrState, int>(Arr.create(..X))
                 * from<Arr, ArrState, int>(Arr.create(..Y))
                 * from(labels);
    
    protected override int Count => 
        X * Y * labels.Length;

    protected override string Explain =>
        "Use the product of two iterators to cause a need for boxes. They should come from the Box Pool";

    protected override void Main()
    {
        var iter  = iterator;
        var total = 0;
        while (iter.TryGetValue(out var x, out iter))
        {
            total += x.Item1;
        }

        ignore(total);
    }

    protected override ConsoleColor Color => 
        Bench.Iterator3;
}

//----------------------------------------------------------------------------------------------------------------------

public class SekAddTest : Bench<SekAddTest>
{
    protected override string Explain =>
        $"Add {Count} integers to a Sek list";

    protected override int Count { get; } = 
        DefaultCount;

    protected override void Main()
    {
        Sq<int> seq = default;
        for(var i = 0; i < Count; i++)
        {
            seq = seq.Add(i);
        }
        ignore(seq);
    }

    protected override ConsoleColor Color => 
        Bench.Iterator4;
}

//----------------------------------------------------------------------------------------------------------------------

public class TreeListAddTest : Bench<TreeListAddTest>
{
    const int X = 1000;
    const int Y = 1000;
    
    protected override string Explain =>
        $"Add {Count} integers to a TreeList";

    protected override int Count { get; } = 
        X * Y;

    protected override void Main()
    {
        for(var i = 0; i < X; i++)
        {
            var list = TreeList<int>.Empty;
            for (var j = 0; j < Y; j++)
            {
                list = list.Add(i);
            }
            list.Dispose();
        }
    }

    protected override ConsoleColor Color => 
        Bench.Iterator4;
}


/*
public class Iterator3ForEachVersion : Bench<Iterator3ForEachVersion>
{
    readonly Iter<ArrState, int> iterator = 
        Iter.from<Arr, ArrState, int>(Arr.create(..Count));

    protected override string Explain =>
        $"Iter3, for Arr, using foreach ({Count:N0} items)";

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
        Bench.Iterator3;
}
*/


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
