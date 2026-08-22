using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public record LazyIteratorAction<A>(Func<Iterator<A>> xs) : IteratorAction<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        // Get the head A value that was stashed where the iterable reference normally goes
        head = Unsafe.As<object, A>(ref stack.GetThis());
        
        // Remove this lazy-iterator action from the stack
        stack.Pop();

        // Lazily acquire the tail iterator
        var tail = xs();
        
        // Cast to the stack type we're using
        ref readonly var fs = ref Unsafe.As<MiniStack<IteratorFields<A>>, MiniStack<IteratorFields>>(ref Unsafe.AsRef(in tail.fields));
        
        // Push everything in the tail to the current stack
        stack.PushMany(in fs);

        return true;
    }
}