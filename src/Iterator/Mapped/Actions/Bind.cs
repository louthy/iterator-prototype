using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
sealed class BindAction<A, B>(IteratorAction<A> action, Func<A, Iterator<B>> f) : IteratorAction<A, B>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(ref MiniStack<IteratorFields> stack, out B head)
    {
        ref var flags = ref stack.Flags;

        while (true)
        {
            if (flags == 0)
            {
                while (action.TryGetValue(ref stack, out var x))
                {
                    var ib = f(x);
                    if (ib.TryGetValue(out head, out var tail))
                    {
                        stack.PushMany(in tail.fields.Cast<IteratorFields<B>, IteratorFields>());
                        flags = 1;
                        return true;
                    }
                }
                head = default!;
                flags = 0;
                return false;
            }
            else
            {
                var actionB = stack.GetAction<B>();
                if (actionB.TryGetValue(ref stack, out head))
                {
                    return true;
                }
                else
                {
                    flags = 0;
                    stack.Pop();
                }
            }
        }
    }   
}
