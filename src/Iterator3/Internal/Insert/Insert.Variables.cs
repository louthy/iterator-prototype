using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Insert
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool declare<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs, we reset the global to its declared value
        fun(ref frame, GlobalsGen<A>.reset(in ix));

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg<A>(ref StackFrame frame, in ushort fromEnd) =>
        
        // Load recent global by providing an index from the end of the globals-list
        frame.globals.AtEnd<A>(in fromEnd, out var g) &&
        
        // The operation to load the global has the index built-in
        fromEnd switch
        {
            1 => fun(ref frame, G1<A>.arg(in g.Index)),   
            2 => fun(ref frame, G2<A>.arg(in g.Index)),   
            3 => fun(ref frame, G3<A>.arg(in g.Index)),   
            4 => fun(ref frame, G4<A>.arg(in g.Index)),
            _ => throw new InvalidOperationException("argument indexes can only be 1, 2, 3 or 4")
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(ref frame, G1<A>.arg(in ix));
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(ref frame, G2<A>.arg(in ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(ref frame, G3<A>.arg(in ix));    
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(ref frame, G4<A>.arg(in ix));    
}
