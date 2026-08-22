using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class ConcatAction<T, IS, A>(IteratorAction<T, IS, A> first, Iterator<T, IS, A> next) : IteratorAction<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    bool IteratorAction<A>.TryGetValue(ref MiniStack<IteratorFields> stack, out A head)
    {
        if (first.TryGetValue(ref stack, out head))
        {
            return true;
        }
        else
        {
            if (next.TryGetValue(out head, out var tail))
            {
                // TODO: Replace the stack with the `next.tail` stack
                //       It may be the case that we should just pop the top of the current stack and the push the 
                //       tail stack.  Will need testing with monadic binding.
                stack = tail.fields.Cast<IteratorFields<T, IS, A>, IteratorFields>();
                return true;
            }
            else
            {
                return false;
            }
        }
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields<T, IS, A>> stack, out A head)
    {
        if (first.TryGetValue(ref stack, out head))
        {
            return true;
        }
        else
        {
            if (next.TryGetValue(out head, out var tail))
            {
                // TODO: Replace the stack with the `next.tail` stack
                //       It may be the case that we should just pop the top of the current stack and the push the 
                //       tail stack.  Will need testing with monadic binding.
                stack = tail.fields;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
