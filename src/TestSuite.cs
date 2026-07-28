namespace IteratorTest;

public class TestSuite
{
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Test suite");
        InOrder();
        Prepend();
    }
    
    public static void InOrder()
    {
        Console.WriteLine("Show that the items iterate in the order they were added");
        var arr = Array.create(1, 2, 3, 4, 5);
        foreach (var x in arr)
        {
            Console.Write($"{x} ");
        }
        Console.WriteLine();
    }
        
    public static void Prepend()
    {
        Console.WriteLine("Show that the an item can be prepended (cons'd)");
        var iter = 1 + (2 + (3 + Array.create(4, 5).Forward()));
        foreach (var x in iter)
        {
            Console.Write($"{x} ");
        }
        Console.WriteLine();
    }
}