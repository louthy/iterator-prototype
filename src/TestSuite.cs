namespace IteratorTest;

public class TestSuite
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
        var arr = Array.create(1, 2, 3, 4, 5);
        foreach (var x in arr)
        {
            Console.Write($"{x} ");
        }
    }

    static void Prepend()
    {
        var iter = 1 + (2 + (3 + Array.create(4, 5).Forward()));
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
        }
    }

    static void PrependLazy()
    {
        var iter = 1.Cons(() => 2.Cons(() => 3.Cons(() => Array.create(4, 5).Forward())));
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
        }
    }
}