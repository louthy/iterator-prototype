using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConsAction<T, IS, A>(A x, Iterator<T, IS, A> xs) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        head = x;
        stack.Pop();
        stack.PushMany(in Unsafe.As<MiniStack<IteratorFields<T, IS, A>>, MiniStack<IteratorFields>>(ref Unsafe.AsRef(in xs.fields))); 
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields<T, IS, A>> stack, out A head)
    {
        head = x;
        stack.Pop();
        stack.PushMany(in xs.fields); 
        return true;
    }
}
