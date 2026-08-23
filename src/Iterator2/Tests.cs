namespace IteratorPrototype;

public class IteratorTest2
{
    public static void Run()
    {
        Test1();
        Test2();
    }
    
    public static void Test1()
    {
        var arr   = Arr.create(1..6);
        var iter1 = Iterator2.from<Arr, ArrState, int>(arr);
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
        var iter1 = Iterator2.from<Arr, ArrState, string>(arr);
        var iter  = iter1.Prepend("Zero");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
}