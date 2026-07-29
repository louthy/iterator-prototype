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
    
        Console.WriteLine($"Elapsed: {elapsed.TotalMicroseconds:F0} µs \tEach: {elapsed.TotalNanoseconds / Count:F3} ns \t{Explain}\t Memory: {memory} bytes");
        return elapsed;
    }
}

