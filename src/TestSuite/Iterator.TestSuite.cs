namespace IteratorPrototype;

public static class IteratorTestSuite
{
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("Test suite");
        
        Test(InOrder, "Show that the items iterate in the order they were added");
        Test(Prepend, "Show that the an item can be prepended (cons'd)");
        Test(PrependLazy, "Show that the an item can be prepended (cons'd) lazily");
        Test(MapToString, "Show that elements can be mapped to other types");
        
        Console.WriteLine();
    }

    static void Test(Action action, string desc)
    {
        Console.WriteLine(desc);
        action();
        Console.WriteLine();
    }

    static void InOrder()
    {
        var iter  = Iterator2.from<Arr, ArrState, int>(Arr.create(1, 2, 3, 4, 5));
        var total = 0;
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
            total += x;
        }
        
        Assert.True(total == 15, "Total should be 15");
    }

    static void Prepend()
    {
        var iter1 = Iterator2.from<Arr, ArrState, int>(Arr.create(4, 5));
        var iter  = 1 + (2 + (3 + iter1));
        var total = 0;
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
            total += x;
        }
        Assert.True(total == 15, "Total should be 15");
    }

    static void PrependLazy()
    {
        var iter1 = Iterator2.from<Arr, ArrState, int>(Arr.create(4, 5));
        var iter  = 1.Cons(() => 2.Cons(() => 3.Cons(() => iter1)));
        var total = 0;
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
            total += x;
        }
        
        Assert.True(total == 15, "Total should be 15");
    }

    static void MapToString()
    {
        var iter  = Iterator2.from<Arr, ArrState, int>(Arr.create(1, 2, 3, 4, 5)).Map(x => $"Item: {x}");
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
        }
    }
}