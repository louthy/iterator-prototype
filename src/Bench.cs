using System.Diagnostics;

namespace IteratorPrototype;

public abstract class Bench<A>
    where A : Bench<A>, new()
{
    protected static int DefaultCount = 1_000_000;
    protected abstract int Count { get; }
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

        var elapsed         = TimeSpan.Zero;
        var memoryAllocated = 0L;
        
        for (var i = 0; i < runs; i++)
        {
            var alloc = GC.GetTotalAllocatedBytes(true) + 40 /* stopwatch size */;
            
            // Inner timer begin
            var sw = Stopwatch.StartNew();
            Main();
            sw.Stop();
            // Inner timer end

            memoryAllocated += Math.Max(0, GC.GetTotalAllocatedBytes(true) - alloc);
            
            elapsed += sw.Elapsed;
        }
    
        elapsed /= runs;
        memoryAllocated /= runs;

        var memAllocStr = memoryAllocated switch
                          {
                              < 10   * 1024 => $"Mem (used): {memoryAllocated} bytes",
                              < 1024 * 1024 => $"Mem (used): {memoryAllocated / 1024} kb",
                              _             => $"Mem (used): {memoryAllocated / 1024 / 1024} mb",
                          };
        
        var restore = Console.ForegroundColor;
        Console.ForegroundColor = Color;
        Console.WriteLine($"Elapsed: {elapsed.TotalMicroseconds:F0} µs \tEach: {elapsed.TotalNanoseconds / Count:F3} ns \t {memAllocStr} \t{Explain}");
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
    public const ConsoleColor Iterator4 = ConsoleColor.Green;

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

        Console.ForegroundColor = Iterator3;
        Console.Write("■");
        Console.ForegroundColor = restore;
        Console.Write(" iterator3 ideas   ");

        Console.ForegroundColor = Iterator4;
        Console.Write("■");
        Console.ForegroundColor = restore;
        Console.Write(" iterator4 ideas   ");

        Console.WriteLine();        
    }
}
