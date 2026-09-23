using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Insert
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(in StackFrame frame, in IterOp f) =>
        frame.Prepend(f);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool scope(in StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(in frame, &Pull.coroutine);
 
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool take(in StackFrame frame, in int amount) =>
        
        // Push take operation
        fun(in frame, &Pull.take) &&
        
        // Push the amount
        arg1(in frame, amount);
}
