namespace IteratorPrototype;

public static class ArrTestSuite
{
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("Test suite");
        
        Test(InOrder, "Show that the items iterate in the order they were added");
        Test(Prepend, "Show that the an item can be prepended (cons'd)");
        Test(PrependLazy, "Show that the an item can be prepended (cons'd) lazily");
        
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
        var arr   = Arr.create(1, 2, 3, 4, 5);
        var total = 0;
        
        foreach (var x in arr)
        {
            Console.Write($"{x} ");
            total += x;
        }
        
        Assert.True(total == 15, "Total should be 15");
    }

    static void Prepend()
    {
        var iter  = 1 + (2 + (3 + Arr.create(4, 5).Forward()));
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
        var iter  = 1.Cons(() => 2.Cons(() => 3.Cons(() => Arr.create(4, 5).Forward())));
        var total = 0;
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
            total += x;
        }
        
        Assert.True(total == 15, "Total should be 15");
    }
}