using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
readonly struct Vars
{
    public readonly ObjStack objs;
    public readonly ByteStack values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Vars(in ObjStack objs, in ByteStack values)
    {
        this.objs = objs;
        this.values = values;
    }
}

[SkipLocalsInit]
public readonly struct Iter<S, A>
{
    readonly Ops ops;
    readonly Vars vars;
    readonly ByteStack state;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    Iter(in Ops ops, in Vars vars, in ByteStack state)
    {
        this.ops = ops;
        this.vars = vars;
        this.state = state;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iter<S, A> tail)
    {
        head = default!;
        tail = this;

        var tmpVars = tail.vars;
        var frame   = tail.Frame(in tmpVars); // Copy
        
        return ops.Run(ref frame) && frame.Pop(out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    StackFrame Frame(in Vars vs) =>
        new (ref Unsafe.AsRef(in ops), ref Unsafe.AsRef(in vs), ref Unsafe.AsRef(in state));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    StackFrame Frame() =>
        new(ref Unsafe.AsRef(in ops), ref Unsafe.AsRef(in vars), ref Unsafe.AsRef(in state));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    ref Iter<S, B> Cast<B>() =>
        ref Unsafe.As<Iter<S, A>, Iter<S, B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    Iter<S, B> CopyCast<B>() =>
        Unsafe.As<Iter<S, A>, Iter<S, B>>(ref Unsafe.AsRef(in this));        

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Iter<S, B> Map<B>(Func<A, B> f)
    {
        var tb    = CopyCast<B>();
        var frame = tb.Frame();
        Push.map(ref frame, f);
        return tb;
    }

    public static Iter<IS, A> From<T, IS>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var ops   = new Ops();
        var vars  = new Vars();
        var state = new ByteStack();
        var frame = new StackFrame(ref ops, ref vars, ref state);
        
        Push.iterable<T, IS, A>(ref frame, ta);
        return new Iter<IS, A>(in ops, in vars, in state);
    }
}

public static class Iter
{
    public static Iter<IS, A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        Iter<IS, A>.From<T, IS>(ta);
}

public static class NextOpTests
{
    public static void Tests()
    {
        Test0();
        Test3();
        /*
        Test1();
        Test2();
        Test4();*/
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
    
    public static void Test1()
    {
        /*var arr   = Arr.create(1..6);

        var iter1 = Iter.from<Arr, ArrState, int>(arr);
        var iter  = iter1.Prepend(0);
        
        while(iter.TryGetValue(out var head, out iter))
        {
            Console.Write($"{head} ");
        }
        Console.WriteLine();*/
    }
    
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