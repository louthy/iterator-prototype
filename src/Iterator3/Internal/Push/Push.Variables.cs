using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare1<A>(ref StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.Add(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(ref frame, GlobalsGen<A>.reset(in ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare2<A>(ref StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.Add(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx2) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(ref frame, GlobalsGen<A>.reset(in ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare3<A>(ref StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.Add(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(ref frame, GlobalsGen<A>.reset(in ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare4<A>(ref StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.Add(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(ref frame, GlobalsGen<A>.reset(in ix));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref1<A>(ref StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(ref frame, G1<A>.arg(in frame.args.GlobalIx1));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref2<A>(ref StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(ref frame, G2<A>.arg(in frame.args.GlobalIx2));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref3<A>(ref StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(ref frame, G3<A>.arg(in frame.args.GlobalIx3));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref4<A>(ref StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(ref frame, G4<A>.arg(in frame.args.GlobalIx4));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(ref StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.Add(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(ref frame, G1<A>.arg(in ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(ref StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.Add(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(ref frame, G2<A>.arg(in ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(ref StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.Add(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(ref frame, G3<A>.arg(in ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(ref StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.Add(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(ref frame, G4<A>.arg(in ix));    
}
