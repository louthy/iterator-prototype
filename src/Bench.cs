using System.Diagnostics;

namespace IteratorPrototype;

public abstract class Bench<A>
    where A : Bench<A>, new()
{
    protected const int Count = 1_000_000;
    protected abstract string Explain { get; }
    protected abstract void Main();
    protected abstract ConsoleColor Color { get; }

    public static void Mark()
    {
        var b = new A();
        b.Run();
    }

    TimeSpan Run()
    {
        const int runs = 5;
    
        for (var i = 0; i < runs; i++)
        {
            Main();
        }

        var elapsed = TimeSpan.Zero;
        var memory  = 0L;
        
        for (var i = 0; i < runs; i++)
        {
            var ms = GC.GetTotalAllocatedBytes(true);
            
            // Inner timer begin
            var sw = Stopwatch.StartNew();
            Main();
            sw.Stop();
            // Inner timer end

            memory += Math.Max(0, GC.GetTotalAllocatedBytes(true) - ms - 40 /* stopwatch size */);
            
            elapsed += sw.Elapsed;
        }
    
        elapsed /= runs;
        memory /= runs;
    
        var restore = Console.ForegroundColor;
        Console.ForegroundColor = Color;
        Console.WriteLine($"Elapsed: {elapsed.TotalMicroseconds:F0} µs \tEach: {elapsed.TotalNanoseconds / Count:F3} ns  \t Memory: {memory} bytes \t{Explain}");
        Console.ForegroundColor = restore;
        return elapsed;
    }
}

public static class Bench
{
    public const ConsoleColor Baseline = ConsoleColor.Cyan;
    public const ConsoleColor Mutable = ConsoleColor.Yellow;
    public const ConsoleColor Immutable = ConsoleColor.Magenta;
    public const ConsoleColor Iterator2 = ConsoleColor.Red;
    public const ConsoleColor Iterator3 = ConsoleColor.White;

    public static void Key()
    {
        var restore = Console.ForegroundColor;
        
        Console.WriteLine();

        Console.Write("Key:  ");

        Console.ForegroundColor = Baseline;
        Console.Write("■");
        Console.ForegroundColor = restore;
        Console.Write(" baseline   ");

        Console.ForegroundColor = Mutable;
        Console.Write("■");
        Console.ForegroundColor = restore;
        Console.Write(" mutable process   ");

        Console.ForegroundColor = Immutable;
        Console.Write("■");
        Console.ForegroundColor = restore;
        Console.Write(" immutable process   ");

        Console.ForegroundColor = Iterator2;
        Console.Write("■");
        Console.ForegroundColor = restore;
        Console.Write(" iterator2 ideas   ");

        Console.WriteLine();        
    }
}
