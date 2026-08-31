using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool declare<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs, we reset the global to its declared value
        fun(ref frame, G.reset<A>(in ix));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool @const<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(ref frame, G.pull<A>(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool var<A>(ref StackFrame frame, in A value) =>
        
        // Push the value to the globals-list
        frame.globals.Add(in value, out var ix) &&

        // Each time this runs we acquire the constant value from the globals-list
        fun(ref frame, G.pullM<A>(in ix));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool arg<A>(ref StackFrame frame, in ushort fromEnd) =>

        // Load recent global by providing an index from the end of the globals-list
        frame.globals.AtEnd<A>(in fromEnd, out var g) &&
        
        // The operation to load the global has the index built-in
        fun(ref frame, G.pullM<A>(in g.Index));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool constarg<A>(ref StackFrame frame, in ushort fromEnd) =>

        // Load recent global by providing an index from the end of the globals-list
        frame.globals.AtEnd<A>(in fromEnd, out var g) &&
        
        // The operation to load the global has the index built-in
        fun(ref frame, G.pull<A>(in g.Index));

    public static bool pusharg<A>(ref StackFrame frame) =>
        
        // Load recent global by providing an index from the end of the globals-list
        frame.globals.AtEnd<A>(1, out var g) &&
        
        // The operation to load the global has the index built-in
        fun(ref frame, G.push<A>(in g.Index));
}
