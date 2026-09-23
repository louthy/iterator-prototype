using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Insert
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare<A>(in StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.AddMutable(in value, out var ix) &&

        // Each time this runs, we reset the global to its declared value
        fun(in frame, GlobalsGen<A>.reset(ix));

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg<A>(in StackFrame frame, ushort fromEnd) =>
        
        // Load recent global by providing an index from the end of the globals-list
        frame.globals.AtEnd<A>(fromEnd, out var g) &&
        
        // The operation to load the global has the index built-in
        fromEnd switch
        {
            1 => fun(in frame, G1<A>.arg(g.Index)),   
            2 => fun(in frame, G2<A>.arg(g.Index)),   
            3 => fun(in frame, G3<A>.arg(g.Index)),   
            4 => fun(in frame, G4<A>.arg(g.Index)),
            _ => throw new InvalidOperationException("argument indexes can only be 1, 2, 3 or 4")
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(in StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.AddMutable(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(in frame, G1<A>.arg(ix));
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(in StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.AddMutable(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(in frame, G2<A>.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(in StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.AddMutable(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(in frame, G3<A>.arg(ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(in StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.AddMutable(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(in frame, G4<A>.arg(ix));    
}
