using IteratorPrototype;
using IteratorTest;
using IteratorTest.Traits;
using LanguageExt;
using I = IteratorTest;
using static LanguageExt.Prelude;

TestSuite.Run();

Bench<CSharpVersion>.Mark();
Bench<CurrentLanguageExtArrVersion>.Mark();
Bench<IterableKVersion>.Mark();
Bench<ForeachVersionRef>.Mark();
Bench<ForeachVersionNonRef>.Mark();
Bench<StrongIteratorVersion>.Mark();
Bench<WeakIteratorVersion>.Mark();
Bench.Key();

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the C# array performance 
//  It is the baseline to compare everything else against.
//

public class CSharpVersion : Bench<CSharpVersion>
{
    readonly int[] array = I.Array.create(..Count).AsSpan().ToArray();

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
    readonly Arr<int> arr = toArray(I.Array.create(..Count).AsSpan());

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
//  This tests the IterableK trait performance 
//
public class IterableKVersion : Bench<IterableKVersion>
{
    readonly Array<int> array = I.Array.create(..Count);

    protected override string Explain =>
        $"IterableK trait stepping ({Count:N0} items)";

    protected override void Main()
    {
        var state = IterableMutable.setup<Array<int>, ArrayState, ArrayStateRef, int>(array);
        var total = 0;
        
        while (IterableMutable.step<Array<int>, ArrayState, ArrayStateRef, int>(array, ref state, out var x))
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
    readonly Array<int> array = I.Array.create(..Count);

    protected override string Explain =>
        $"Ref Struct Foreach Array<A> ({Count:N0} items)";

    protected override void Main()
    {
        var total = 0;
        foreach(var x in array)
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
    readonly Array<int> array = I.Array.create(..Count);

    protected override string Explain =>
        $"Non-Ref Struct Foreach Array<A> ({Count:N0} items)";

    protected override void Main()
    {
        var total = 0;
        foreach(var x in array.yield)
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
    readonly Array<int> array = I.Array.create(..Count);

    protected override string Explain =>
        $"Strong Iterator while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = array.Forward();
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
    readonly Array<int> array = I.Array.create(..Count);

    protected override string Explain =>
        $"Weak Iterator while TryGetValue ({Count:N0} items)";

    protected override void Main()
    {
        var iter  = IterableImmutable.from<Array<int>, ArrayState, int>(in array);
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
