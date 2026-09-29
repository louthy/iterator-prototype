using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pure(in StackFrame frame) =>

        // Push the yield operation
        fun(in frame, &Pull.pure, OpReturn.Default);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pure<A>(in StackFrame frame, in A value) =>
        
        // Push the constant value
        arg1(in frame, in value) &&
        
        // Push the yield operation
        fun<A>(in frame, &Pull.pureV<A>, OpReturn.Default);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool yield<A>(in StackFrame frame) =>
        
        // Start a new co-routine with what's at the top of the stack as an input argument
        fun(in frame, VarsGen<A>.yield, OpReturn.CoRoutine);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool yield<A>(in StackFrame frame, in A value) =>
        
        // Create a global-var to store the constant value
        frame.globals.AddMutable(in value, out var gix) &&
        
        // Push the constant value to the top of the stack
        fun<A>(in frame, GlobalsGen<A>.pull(gix), OpReturn.Default) &&
        
        // Start a new co-routine with what's at the top of the stack as an input argument
        fun(in frame, VarsGen<A>.yield, OpReturn.CoRoutine);

    public static bool incYield(in StackFrame frame) =>
        fun(in frame, &Pull.incYield, OpReturn.Default);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool coroutine(in StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(in frame, &Pull.coroutine, OpReturn.CoRoutine);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool tuple<A, B>(in StackFrame frame) => 
        
        // Push tuple operation
        fun<A, B, (A, B)>(in frame, &Pull.tuple<A, B>, OpReturn.Default);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool tuple<A, B, C>(in StackFrame frame) => 
        
        // Push tuple operation
        fun<A, B, C, (A, B, C)>(in frame, &Pull.tuple<A, B, C>, OpReturn.Default);    

    [MethodImpl(Optimisations.InliningOnly)]
    internal static bool elements<A, B>(in StackFrame frame) => 
        
        // Push elements operation
        fun2<(A, B), A, B>(in frame, &Pull.elements<A, B>, OpReturn.Default);
        
    [MethodImpl(Optimisations.InliningOnly)]
    internal static bool elements<A, B, C>(in StackFrame frame) => 
        
        // Push elements operation
        fun3<(A, B, C), A, B, C>(in frame, &Pull.elements<A, B, C>, OpReturn.Default);
}
