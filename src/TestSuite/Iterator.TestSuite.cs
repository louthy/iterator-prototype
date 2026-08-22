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
        Test(BindToString, "Show that elements can be monad bound to other types");
        
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
        var iter  = Arr.create(1, 2, 3, 4, 5).Forward();
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

    static void MapToString()
    {
        var iter  = Arr.create(1, 2, 3, 4, 5)
                       .Forward()
                       .Map(x => $"Item: {x}");
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
        }
    }
    
    public static void BindToString()
    {
        var iter = Arr.create(1, 2, 3, 4, 5)
                      .Forward()
                      .Bind(x => Arr.create($"Item 1: {x * 1}", $"Item 2: {x * 2}", $"Item 3: {x * 3}").Forward());
        
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
        }
    }
}