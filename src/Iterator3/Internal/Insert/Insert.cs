using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Insert
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool scope(in StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(in frame, &Pull.coroutine, OpReturn.CoRoutine);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool take(in StackFrame frame, in int amount) =>
        
        // Push take operation
        fun(in frame, &Pull.take, OpReturn.CanVoid) &&
        
        // Push the amount
        arg1(in frame, amount);
}
