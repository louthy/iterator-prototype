namespace IteratorPrototype.Iterator3;

using static IteratorPrototype.Iterator3.Iter;

public static class IterTests
{
    public static void Tests()
    {
        Basic0();
        Basic1();
        /*
        Test0();
        Test3();
        Test1();
        Test2();
        Test4();*/
    }

    public static void Basic0()
    {
        var iter = forever(1)
                      | map<int, string>(x => $"'{x}'")
                      | take(10);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic1()
    {
        var iter = singleton(1) 
                     | map<int, string>(x => $"'{x}'");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic()
    {
        var iter = pair(1, 2);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        
        Console.WriteLine();
    }
    
    public static void Test0()
    {
        var arr  = Arr.create(1..6);
        var iter = Iter.from<Arr, ArrState, int>(arr);

        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }
    
    /*
    public static void Test1()
    {
        var arr  = Arr.create(1..6);
        var iter = Iter.from<Arr, ArrState, int>(arr).Prepend(0);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }*/
    
    public static void Test2()
    {
        /*var arr   = Arr.create("One", "Two", "Three", "Four", "Five");
        var iter1 = Iter.from<Arr, ArrState, string>(arr);
        var iter  = iter1.Prepend("Zero");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();*/
    }
        
    public static void Test3()
    {
        var arr  = Arr.create(1..6);
        var iter = Iter.from<Arr, ArrState, int>(arr).Map(x => $"Item {x}");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();
    }    
        
    public static void Test4()
    {
        /*var arr  = Arr.create(1..4);
        
        var iter1 = Iter.from<Arr, ArrState, int>(arr);
        var iter  = iter1.Bind(x => iter1.Map(y => x * y));
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();*/
    }
}