using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare1<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare2<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx2) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare3<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare4<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix));
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool const1<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddConst(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix;

        // Each time this runs, we reset the global to its declared value
        return true;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool const2<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddConst(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx2) = ix;

        // Each time this runs, we reset the global to its declared value
        return true;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool const3<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddConst(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix;

        // Each time this runs, we reset the global to its declared value
        return true;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool const4<A>(in StackFrame frame, in A value)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddConst(in value, out var ix)) return false;
        Unsafe.AsRef(in frame.args.GlobalIx4) = ix;

        // Each time this runs, we reset the global to its declared value
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref1<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G1<A>.arg(frame.args.GlobalIx1));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref2<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G2<A>.arg(frame.args.GlobalIx2));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref3<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G3<A>.arg(frame.args.GlobalIx3));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref4<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G4<A>.arg(frame.args.GlobalIx4));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G1<A>.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G2<A>.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G3<A>.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G4<A>.arg(ix));    
}
