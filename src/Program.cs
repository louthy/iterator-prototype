using System.Diagnostics;
using IteratorTest;
using IteratorTest.Traits;
using I = IteratorTest;
using static LanguageExt.Prelude;

TestSuite.Run();

const int count = 1_000_000;

var items = I.Array.create(..count);

Warmup();
Warmup();
Warmup();

RunForReal();

return;

(int, TimeSpan) RunForReal()
{
    var (t1, e1) = CSharpVersion(items);
    WriteOutput(t1, e1, $"Foreach C# array ({count:N0} items)");

    var (t2, e2) = CurrentLanguageExtArrVersion(items);
    WriteOutput(t2, e2, $"Foreach current LanguageExt Arr<A> ({count:N0} items)");

    var (t3, e3) = IterableKVersion(items);
    WriteOutput(t3, e3, $"IterableK trait stepping ({count:N0} items)");

    var (t4, e4) = ForeachVersion(items);
    WriteOutput(t4, e4, $"Foreach Array<A> ({count:N0} items)");

    var (t5, e5) = StrongIteratorVersion(items);
    WriteOutput(t5, e5, $"Strong Iterator while TryGetValue ({count:N0} items)");

    var (t6, e6) = WeakIteratorVersion(items);
    WriteOutput(t6, e6, $"Weak Iterator while TryGetValue ({count:N0} items)");

    return (t1 + t2 + t3 + t4 + t5 + t6, e1 + e2 + e3 + e4 + e5 + e6);
}

(int, TimeSpan) Warmup()
{
    var (t1, e1) = CSharpVersion(items);
    var (t2, e2) = CurrentLanguageExtArrVersion(items);
    var (t3, e3) = IterableKVersion(items);
    var (t4, e4) = ForeachVersion(items);
    var (t5, e5) = StrongIteratorVersion(items);
    var (t6, e6) = WeakIteratorVersion(items);
    
    return (t1 + t2 + t3 + t4 + t5 + t6, e1 + e2 + e3 + e4 + e5 + e6);
}

void WriteOutput<A>(A output, TimeSpan elapsed, string explain) =>
    Console.WriteLine($"Output: {output}\tElapsed: {elapsed.TotalMicroseconds:F0} µs \tEach: {elapsed.TotalNanoseconds / count:F3} ns \t{explain}");

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the C# array performance 
//  It is the baseline to compare everything else against.
//
(int Total, TimeSpan Elapsed) CSharpVersion(Array<int> array)
{
    var carr = array.AsSpan();

    var total = 0;
    var sw    = Stopwatch.StartNew();

    foreach (var x in carr)
    {
        total += x;
    }

    sw.Stop();
    return (total, sw.Elapsed);
}

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the performance of a generalised Iterator 
//
(int Total, TimeSpan Elapsed) StrongIteratorVersion(Array<int> array)
{
    var total = 0;
    var iter  = array.Forward();

    var sw = Stopwatch.StartNew();
    while (iter.TryGetValue(out var x, out iter))
    {
        total += x;
    }

    sw.Stop();
    return (total, sw.Elapsed);
}

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the performance of a generalised Iterator 
//
(int Total, TimeSpan Elapsed) WeakIteratorVersion(Array<int> array)
{
    var total = 0;
    var iter  = IterableK.fromIterable<I.Array, ArrayState, int>(array);

    var sw = Stopwatch.StartNew();
    while (iter.TryGetValue(out var x, out iter))
    {
        total += x;
    }

    sw.Stop();
    return (total, sw.Elapsed);
}

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the performance of a generalised foreach using the IteratorEnumerator 
//
(int Total, TimeSpan Elapsed) ForeachVersion(Array<int> array)
{
    var total = 0;
    
    var sw = Stopwatch.StartNew();
    foreach(var x in array)
    {
        total += x;
    }

    sw.Stop();
    return (total, sw.Elapsed);
}

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the IterableK trait performance 
//
(int Total, TimeSpan Elapsed) IterableKVersion(Array<int> array1)
{
    var total = 0;
    var state = IterableK.setup<I.Array, ArrayState, int>(array1);

    var sw = Stopwatch.StartNew();
    while (IterableK.step<I.Array, ArrayState, int>(ref state, out var x))
    {
        total += x;
    }

    sw.Stop();
    return (total, sw.Elapsed);
}

//----------------------------------------------------------------------------------------------------------------------
//
//  Current LanguageExt Arr<A> foreach version 
//
(int Total, TimeSpan Elapsed) CurrentLanguageExtArrVersion(Array<int> array)
{
    var arr   = toArray(array.AsSpan());
    var total = 0;

    var sw = Stopwatch.StartNew();
    foreach(var x in arr)
    {
        total += x;
    }

    sw.Stop();
    return (total, sw.Elapsed);
}

/*
TimeSpan ILCastTest()
{
    var iter = Iterator.fromIterable<I.Array, ArrayState, int>(I.Array.create(..count));
    var to   = IL.cast<Iterator<I.Array, ArrayState, int>, Iterator<int>>();
    var from = IL.cast<Iterator<int>, Iterator<I.Array, ArrayState, int>>();

    var sw = Stopwatch.StartNew();
    for (var i = 0; i < count; i++)
    {
        ILCast(to, from, ref iter);
    }

    sw.Stop();
    return sw.Elapsed;
}

void ILCast<T, TS, A>(
    Func<Iterator<T, TS, A>, Iterator<A>> to,
    Func<Iterator<A>, Iterator<T, TS, A>> from, 
    ref Iterator<T, TS, A> iterator)
    where T : IterableK<T, TS>
    where TS : struct
{
    var iter2 = to(iterator);
    var iter3 = from(iter2);
    iterator = iter3;
}

TimeSpan UnsafeCastTest()
{
    var iter = Iterator.fromIterable<I.Array, ArrayState, int>(I.Array.create(..count));
    ToDelegate<I.Array, ArrayState, int> to = UnsafeCastTo<I.Array, ArrayState, int>;
    FromDelegate<I.Array, ArrayState, int> from = UnsafeCastFrom<I.Array, ArrayState, int>;

    var sw = Stopwatch.StartNew();
    for (var i = 0; i < count; i++)
    {
        ref var inter1 = ref Unsafe.As<Iterator<I.Array, ArrayState, int>, Iterator<int>>(ref Unsafe.AsRef(in iter));
        //ref var inter2 = ref Unsafe.As<Iterator<int>, Iterator<I.Array, ArrayState, int>>(ref Unsafe.AsRef(in inter1));
        //UnsafeCast(to, from, ref iter);
    }

    sw.Stop();
    return sw.Elapsed;
}

ref Iterator<A> UnsafeCastTo<T, TS, A>(in Iterator<T, TS, A> iterator)
    where T : IterableK<T, TS>
    where TS : struct =>
    ref Unsafe.As<Iterator<T, TS, A>, Iterator<A>>(ref Unsafe.AsRef(in iterator));

ref Iterator<T, TS, A> UnsafeCastFrom<T, TS, A>(in Iterator<A> iterator)
    where T : IterableK<T, TS>
    where TS : struct =>
    ref Unsafe.As<Iterator<A>, Iterator<T, TS, A>>(ref Unsafe.AsRef(in iterator));

void UnsafeCast<T, TS, A>(
    ToDelegate<T, TS, A> to,
    FromDelegate<T, TS, A> from, 
    ref Iterator<T, TS, A> iterator)
    where T : IterableK<T, TS>
    where TS : struct
{
    var iter2 = to(iterator);
    var iter3 = from(iter2);
    iterator = iter3;
}

public delegate ref Iterator<A> ToDelegate<T, TS, A>(in Iterator<T, TS, A> iterator)
    where T : IterableK<T, TS>
    where TS : struct;

public delegate ref Iterator<T, TS, A> FromDelegate<T, TS, A>(in Iterator<A> iterator)
    where T : IterableK<T, TS>
    where TS : struct;*/