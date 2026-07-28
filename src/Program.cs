using System.Diagnostics;
using IteratorTest;
using IteratorTest.Traits;
using LanguageExt;
using I = IteratorTest;
using static LanguageExt.Prelude;

TestSuite.Run();

const int count = 1_000_000;

Bench(CSharpVersion,                $"Foreach C# array ({count:N0} items)");
Bench(CurrentLanguageExtArrVersion, $"Foreach current LanguageExt Arr<A> ({count:N0} items)");
Bench(IterableKVersion,             $"IterableK trait stepping ({count:N0} items)");
Bench(ForeachVersion,               $"Foreach Array<A> ({count:N0} items)");
Bench(StrongIteratorVersion,        $"Strong Iterator while TryGetValue ({count:N0} items)");
Bench(WeakIteratorVersion,          $"Weak Iterator while TryGetValue ({count:N0} items)");

return;

(A Output, TimeSpan Elapses) Bench<A>(Func<(A Output, TimeSpan Elapsed)> fun, string desc)
{
    var (wu_t1, wu_e1) = fun();
    var (wu_t2, wu_e2) = fun();
    var (wu_t3, wu_e3) = fun();
    var (wu_t4, wu_e4) = fun();
    var (wu_t5, wu_e5) = fun();

    var (t1, e1) = fun();
    var (t2, e2) = fun();
    var (t3, e3) = fun();
    var (t4, e4) = fun();
    var (t5, e5) = fun();

    var e = (e1 + e2 + e3 + e4 + e5) / 5;
    if (!eq(t1, t2) || !eq(t1, t3) || !eq(t1, t4) || !eq(t1, t5))
    {
        Console.WriteLine($"Different outputs for: {desc}");
    }
    WriteOutput(t1, e, desc);
    return (t1, e);

    static bool eq(A lhs, A rhs) =>
        lhs?.Equals(rhs) ?? false;
}

(int, TimeSpan) Warmup()
{
    var (t1, e1) = CSharpVersion();
    var (t2, e2) = CurrentLanguageExtArrVersion();
    var (t3, e3) = IterableKVersion();
    var (t4, e4) = ForeachVersion();
    var (t5, e5) = StrongIteratorVersion();
    var (t6, e6) = WeakIteratorVersion();
    
    return (t1 + t2 + t3 + t4 + t5 + t6, e1 + e2 + e3 + e4 + e5 + e6);
}

void WriteOutput<A>(A output, TimeSpan elapsed, string explain) =>
    Console.WriteLine($"Output: {output}\tElapsed: {elapsed.TotalMicroseconds:F0} µs \tEach: {elapsed.TotalNanoseconds / count:F3} ns \t{explain}");

//----------------------------------------------------------------------------------------------------------------------
//
//  This tests the C# array performance 
//  It is the baseline to compare everything else against.
//
(int Total, TimeSpan Elapsed) CSharpVersion()
{
    var array = I.Array.create(..count);
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
(int Total, TimeSpan Elapsed) StrongIteratorVersion()
{
    var array = I.Array.create(..count);
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
(int Total, TimeSpan Elapsed) WeakIteratorVersion()
{
    var array = I.Array.create(..count);
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
(int Total, TimeSpan Elapsed) ForeachVersion()
{
    var array = I.Array.create(..count);
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
(int Total, TimeSpan Elapsed) IterableKVersion()
{
    var array = I.Array.create(..count);
    var karr  = array.Kind();
    var total = 0;
    var state = IterableK.setup<I.Array, ArrayState, int>(karr);

    var sw  = Stopwatch.StartNew();
    while (IterableK.step(karr, ref state, out var x))
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
(int Total, TimeSpan Elapsed) CurrentLanguageExtArrVersion()
{
    var array = I.Array.create(..count);
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