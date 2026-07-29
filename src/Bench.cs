using System.Diagnostics;

namespace IteratorPrototype;

public abstract class Bench<A>
    where A : Bench<A>, new()
{
    protected const int Count = 1_000_000;
    protected abstract string Explain { get; }
    protected abstract void Main();

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
        for (var i = 0; i < runs; i++)
        {
            var sw      = Stopwatch.StartNew();
            Main();
            sw.Stop();
            elapsed += sw.Elapsed;
        }
    
        elapsed /= runs;
    
        Console.WriteLine($"Elapsed: {elapsed.TotalMicroseconds:F0} µs \tEach: {elapsed.TotalNanoseconds / Count:F3} ns \t{Explain}");
        return elapsed;
    }
}

