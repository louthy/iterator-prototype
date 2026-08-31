using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public readonly struct IterAwait;
public readonly struct IterPure;
public readonly struct IterScope;
public readonly record struct IterTake(int amount);
public readonly record struct IterMap<A, B>(Func<A, B> f);
public readonly record struct IterBimap<A, B, C>(Func<A, B, C> f);
public readonly record struct IterTrimap<A, B, C, D>(Func<A, B, C, D> f);

static class IterAction
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Iter<A> take<A>(int amount, in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.take<A>(ref frame, amount)
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
            
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Iter<A> scope<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.scope(ref frame)
                   ? ta1
                   : default;
    }
            
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Iter<A> pure<A>(in Iter<A> ta)
    {
        var frame = ta.Next(out var ta1);
        return Push.pure(ref frame)
                   ? ta1
                   : default;
    }

}