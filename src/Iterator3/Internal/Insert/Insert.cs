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
        fun(in frame, &Pull.take, OpReturn.CanVoidOrSkip) &&
        
        // Push the amount
        arg1(in frame, amount);
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool takeUntil<A>(in StackFrame frame, Func<A, bool> f) =>
        
        // Push take operation
        fun(in frame, &Pull.takeUntil<A>, OpReturn.CanVoidOrSkip) &&
        
        // Push the predicate
        arg1(in frame, f);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool takeWhile<A>(in StackFrame frame, Func<A, bool> f) =>
        
        // Push take operation
        fun(in frame, &Pull.takeWhile<A>, OpReturn.CanVoidOrSkip) &&
        
        // Push the predicate
        arg1(in frame, f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool skip(in StackFrame frame, in int amount) =>
        
        // Push take operation
        fun(in frame, &Pull.skip, OpReturn.CanVoidOrSkip) &&
        
        // Push the amount
        arg1(in frame, amount);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool skipUntil<A>(in StackFrame frame, Func<A, bool> f) =>
        
        // Push take operation
        fun(in frame, &Pull.skipUntil<A>, OpReturn.CanVoidOrSkip) &&
        
        // Push the predicate
        arg1(in frame, f);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool skipWhile<A>(in StackFrame frame, Func<A, bool> f) =>
        
        // Push take operation
        fun(in frame, &Pull.skipWhile<A>, OpReturn.CanVoidOrSkip) &&
        
        // Push the predicate
        arg1(in frame, f);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool yield<A>(in StackFrame frame, in A value) =>
        
        // Create a global-var to store the constant value
        frame.globals.AddMutable(in value, out var gix) &&
        
        // Start a new co-routine with what's at the top of the stack as an input argument
        fun(in frame, VarsGen<A>.yield, OpReturn.CoRoutine) &&
        
        // Push the constant value to the top of the stack
        fun<A>(in frame, GlobalsGen<A>.pull(gix), OpReturn.Default);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool cons<A>(in StackFrame frame, in A head, in Iter<A> tail) =>
    
        // Flag indicating we've processed
        declare1(in frame, true) &&
        
        // Declare the head
        declare2(in frame, in head) &&
        
        // Declare the tail
        declare3(in frame, in tail) &&

        fun(in frame, &Pull.cons<A>, OpReturn.Default);
}
