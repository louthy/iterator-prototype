using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class MapAction<A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<A, B>
{
    [MethodImpl(Optimisations.Default)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorFields> stack, out B head)
    {
        if (action.TryGetValue(ref stack, out var h))
        {
            head = f(h);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }        
    }

    [MethodImpl(Optimisations.Default)]
    IteratorAction<C> IteratorAction<B>.Map<C>(Func<B, C> g) =>
        new MapAction<A, C>(action, x => g(f(x)));
}

[SkipLocalsInit]
public sealed class MapAction<T, IS, A, B>(IteratorAction<A> action, Func<A, B> f) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(Optimisations.Default)]
    bool IteratorAction<B>.TryGetValue(ref MiniStack<IteratorFields> stack, out B head)
    {
        if (action.TryGetValue(ref stack, out var h))
        {
            head = f(h);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    bool IteratorAction<T, IS, A, B>.TryGetValue(ref MiniStack<IteratorFields<T, IS, A, B>> stack, out B head)
    {
        ref var s = ref Unsafe.As<MiniStack<IteratorFields<T, IS, A, B>>, MiniStack<IteratorFields>>(ref stack);
        if (action.TryGetValue(ref s, out var h))
        {
            head = f(h);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    IteratorAction<C> IteratorAction<B>.Map<C>(Func<B, C> g) =>
        new MapAction<T, IS, A, C>(action, x => g(f(x)));
}
