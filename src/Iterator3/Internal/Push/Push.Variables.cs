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
    
    public static bool declare<A, B>(in StackFrame frame, in A value1, in B value2)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value1, out var ix1) ||
           !frame.globals.AddMutable(in value2, out var ix2)) return false;
        
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix1;
        Unsafe.AsRef(in frame.args.GlobalIx2) = ix2;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix1)) && 
               fun(in frame, GlobalsGen<B>.reset(ix2));
    }
    
    public static bool declare<A, B, C>(in StackFrame frame, in A value1, in B value2, in C value3)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value1, out var ix1) ||
           !frame.globals.AddMutable(in value2, out var ix2) ||
           !frame.globals.AddMutable(in value3, out var ix3)) return false;
        
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix1;
        Unsafe.AsRef(in frame.args.GlobalIx2) = ix2;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix3;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix1)) && 
               fun(in frame, GlobalsGen<B>.reset(ix2)) && 
               fun(in frame, GlobalsGen<C>.reset(ix3));
    }
    
    public static bool declare<A, B, C, D>(in StackFrame frame, in A value1, in B value2, in C value3, in D value4)
    {
        // Push the value to the globals-list
        if(!frame.globals.AddMutable(in value1, out var ix1) ||
           !frame.globals.AddMutable(in value2, out var ix2) ||
           !frame.globals.AddMutable(in value3, out var ix3) ||
           !frame.globals.AddMutable(in value4, out var ix4)) return false;
        
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix1;
        Unsafe.AsRef(in frame.args.GlobalIx2) = ix2;
        Unsafe.AsRef(in frame.args.GlobalIx3) = ix3;
        Unsafe.AsRef(in frame.args.GlobalIx4) = ix4;

        // Each time this runs, we reset the global to its declared value
        return fun(in frame, GlobalsGen<A>.reset(ix1)) && 
               fun(in frame, GlobalsGen<B>.reset(ix2)) && 
               fun(in frame, GlobalsGen<C>.reset(ix3)) && 
               fun(in frame, GlobalsGen<D>.reset(ix4));
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
        fun(in frame, G1.arg(frame.args.GlobalIx1));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref2<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G2.arg(frame.args.GlobalIx2));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref3<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G3.arg(frame.args.GlobalIx3));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool ref4<A>(in StackFrame frame) =>
        
        // Each time this runs we make the global available as an argument 
        fun(in frame, G4.arg(frame.args.GlobalIx4));    

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G1.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G2.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G3.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(in StackFrame frame, in A value) =>
        
        // Declare a new value to be used as the argument
        frame.globals.AddConst(in value, out var ix) &&
        
        // Make sure it gets loaded from the globals each time we run
        fun(in frame, G4.arg(ix));    
}
