using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

public interface IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in object ta, ref Space128 space, out A head);
}

public interface IteratorAction<T, IS, A> : IteratorAction<A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    bool IteratorAction<A>.TryGetValue(in object obj, ref Space128 space, out A head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return TryGetValue(in ta, ref ts, out head);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in K<T, A> ta, ref IS space, out A head);
}

public interface IteratorAction<T, IS, A, B> : IteratorAction<B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    bool IteratorAction<B>.TryGetValue(in object obj, ref Space128 space, out B head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return TryGetValue(in ta, ref ts, out head);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool TryGetValue(in K<T, A> ta, ref IS space, out B head);
}

public class ConsAction<T, IS, A>(A Head) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(in K<T, A> obj, ref IS space, out A head)
    {
        head = Head;
        return true;
    }
}

public class MapAction<T, IS, A, B>(Func<A, B> f) : IteratorAction<T, IS, A, B>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(in K<T, A> ta, ref IS ts, out B head)
    {
        if (T.StepImmutable(in ta, in ts, out var h, out ts))
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
}

public class IdAction<T, IS, A> : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public static readonly IteratorAction<T, IS, A> Default = new IdAction<T, IS, A>();

    public bool TryGetValue(in object obj, ref Space128 space, out A head)
    {
        ref readonly var ta = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in obj));
        ref var          ts = ref Unsafe.As<Space128, IS>(ref space);
        return T.StepImmutable(in ta, in ts, out head, out ts);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(in K<T, A> ta, ref IS ts, out A head) =>
        T.StepImmutable(in ta, in ts, out head, out ts);
}
