using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

using static Iter;

public static class IterTests
{
    public static void Tests()
    {
        Log.enable();

        Basic9();
        
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
                 | take(10)
                 | select<int, string>(x => $"'{x}'");
        
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

    public static int Sum(Iter<int> iter) =>
        iter switch
        {
            Cons<int>(var x, var xs) =>
                x + Sum(xs),

            Nil => 0
        };
    
    public static void Basic3()
    {
        var iter = singleton(1) 
                      | select<int, string>(x => $"'{x}'");
        
        while(iter is Cons<string>(var head, var tail))
        {
            Console.WriteLine($"{head} ");
            iter = tail;
        }
        
        Console.WriteLine();
    }

    public static void Basic4()
    {
        var iter = many(10, 20, 30, 40, 50);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic5()
    {
        var iter = many(10, 20)
                 * many(100, 200)
                 | select((int x, int y) => x * y);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic6()
    {
        var iter = many(1, 2, 3)
                 * many("One", "Two", "Three")
                 * many(true, false)
                 | select((int x, string y, bool z) => $"{x}. {y} ({z.ToString().ToLower()})");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic7()
    {
        var iter = many(1, 2, 3)
                 * many("One", "Two", "Three")
                 * many(true, false)
                 * many(ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue)
                 | select((int x, string y, bool z, ConsoleColor c) => $"{x}. {y} {z.ToString().ToLower()} {c}");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.WriteLine($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic8()
    {
        var iter = many(1, 2, 3)
                 * many("One", "Two", "Three")
                 | select((int x, string y) => $"{x}. {y}");
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Basic9()
    {
        var iter = many(1, 2) + many(3, 4) | pure;
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        
        Console.WriteLine();
    }

    public static void Test0()
    {
        var arr  = Arr.create(1..6);
        var iter = from(arr);

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
        var iter = from(arr).Map(x => $"Item {x}");
        
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