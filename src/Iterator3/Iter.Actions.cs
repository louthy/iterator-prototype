using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public readonly struct IterYield;
public readonly struct IterAwait;
public readonly struct IterPure;
public readonly record struct IterTake(int Amount);
public readonly record struct IterMap<A, B>(Func<A, B> f);

static class IterAction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> await<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.await<A>(ref frame)
                   ? ta1
                   : default;
    }
            
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> pure<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.pure<A>(ref frame)
                   ? ta1
                   : default;
    }
                
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> yield<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.yield<A>(ref frame)
                   ? ta1
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> take<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.take(ref frame, amount)
                   ? ta1
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<B> map<A, B>(Func<A, B> f, in Iter<A> ta)
    {
        var frame = ta.Next<A, B>(out var tb);
        return Push.map(ref frame, f)
                   ? tb
                   : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<B> bind<A, B>(in Iter<A> ta, in Func<A, Iter<B>> f)
    {
        var frame = ta.Next<A, B>(out var tb);
        return Push.bind(ref frame, f)
                   ? tb
                   : default;
    }
}