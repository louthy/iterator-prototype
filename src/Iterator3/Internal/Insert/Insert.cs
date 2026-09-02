using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Insert
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(ref StackFrame frame, in delegate*<ref StackFrame, int> f) =>
        frame.Prepend(f);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool scope(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(ref frame, &Pull.coroutine);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool take(ref StackFrame frame, in int amount) =>
        
        // Push take operation
        fun(ref frame, &Pull.take) &&
        
        // Push the amount
        arg1(ref frame, amount);
}
