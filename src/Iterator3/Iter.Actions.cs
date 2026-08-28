using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public readonly struct IterYield;
public readonly struct IterAwait;
public readonly struct IterPure;
public readonly record struct IterTake(int amount);
public readonly record struct IterMap<A, B>(Func<A, B> f);
public readonly record struct IterBimap<A, B, C>(Func<A, B, C> f);
public readonly record struct IterPair<A, B>;
public readonly record struct IterPairConst<A, B>(A first, B second);

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
    public static Iter<(A First, B Second)> pair<A, B>(in Iter<B> tb)
    {
        var frame = tb.Next<B, (A, B)>(out var tab);
        return Push.tuple<A, B>(ref frame)
                   ? tab
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
    public static Iter<C> bimap<A, B, C>(Func<A, B, C> f, in Iter<(A, B)> tab)
    {
        var frame = tab.Next<(A, B), C>(out var tc);
        return Push.bimap1(ref frame, f)
                   ? tc
                   : default;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<D> trimap<A, B, C, D>(Func<A, B, C, D> f, in Iter<(A, B, C)> tab)
    {
        var frame = tab.Next<(A, B, C), D>(out var tc);
        return Push.trimap1(ref frame, f)
                   ? tc
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