using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pure(in StackFrame frame) =>

        // Push the yield operation
        fun(in frame, &Pull.pure);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pure<A>(in StackFrame frame, in A value) =>
        
        // Push the constant value
        arg1(in frame, in value) &&
        
        // Push the yield operation
        fun(in frame, &Pull.pureV<A>);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool yield<A>(in StackFrame frame) =>
        
        // Start a new co-routine with what's at the top of the stack as an input argument
        fun(in frame, VarsGen<A>.yield);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool yield<A>(in StackFrame frame, in A value) =>
        
        // Create a global-var to store the constant value
        frame.globals.AddMutable(in value, out var gix) &&
        
        // Push the constant value to the top of the stack
        fun(in frame, GlobalsGen<A>.pull(gix)) &&
        
        // Start a new co-routine with what's at the top of the stack as an input argument
        fun(in frame, VarsGen<A>.yield);


    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(in StackFrame frame, in IterOp f) =>
        frame.Add(f);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool coroutine(in StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(in frame, &Pull.coroutine);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool tuple<A, B>(in StackFrame frame) => 
        
        // Push tuple operation
        fun(in frame, &Pull.tuple<A, B>);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool tuple<A, B, C>(in StackFrame frame) => 
        
        // Push tuple operation
        fun(in frame, &Pull.tuple<A, B, C>);    

    [MethodImpl(Optimisations.InliningOnly)]
    internal static bool elements<A, B>(in StackFrame frame) => 
        
        // Push elements operation
        fun(in frame, &Pull.elements<A, B>);
        
    [MethodImpl(Optimisations.InliningOnly)]
    internal static bool elements<A, B, C>(in StackFrame frame) => 
        
        // Push elements operation
        fun(in frame, &Pull.elements<A, B, C>);
}
