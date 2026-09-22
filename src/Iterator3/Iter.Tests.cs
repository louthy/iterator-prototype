using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

using static IteratorPrototype.Iterator3.Iter;

public static class IterTests
{
    public static void Tests()
    {
        Log.enable();
        
        Basic1();
        Basic2();
        Basic3();
        Basic4();
        Basic5();
        Basic6();
        Basic7();
        Basic8();
        Basic9();
    }

    public static void Basic1()
    {
        var iter = forever(1)
                 | select<int, string>(x => $"'{x}'")
                 | take(10);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic2()
    {
        var iter = forever(1) | take(0);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic3()
    {
        var iter = singleton(1) 
                      | select<int, string>(x => $"'{x}'");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic4()
    {
        var iter = from(10, 20, 30, 40, 50);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic5()
    {
        var iter = from(10, 20)
                 * from(100, 200)
                 | select((int x, int y) => x * y);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic6()
    {
        var iter = from(1, 2, 3)
                 * from("One", "Two", "Three")
                 * from(true, false)
                 | select((int x, string y, bool z) => $"{x}. {y} ({z.ToString().ToLower()})");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic7()
    {
        var iter = from(1, 2, 3)
                 * from("One", "Two", "Three")
                 * from(true, false)
                 * from(ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue)
                 | select((int x, string y, bool z, ConsoleColor c) => $"{x}. {y} {z.ToString().ToLower()} {c}");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic8()
    {
        var iter = from(1, 2, 3)
                 * from("One", "Two", "Three")
                 | select((int x, string y) => $"{x}. {y}");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic9()
    {
        var iter = from(1, 2) + from(3, 4);
        
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
        var iter = from<Arr, ArrState, int>(arr).Map(x => $"Item {x}");
        
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