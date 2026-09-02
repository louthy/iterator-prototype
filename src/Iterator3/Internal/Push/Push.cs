using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool pure(ref StackFrame frame) =>

        // Push the yield operation
        fun(ref frame, &Pull.pure);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool pure<A>(ref StackFrame frame, in A value) =>
        
        // Push the constant value
        arg1(ref frame, in value) &&
        
        // Push the yield operation
        fun(ref frame, &Pull.pureV<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame) =>

        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(default(A), out var yieldIx) &&
        
        // Yield what's stored in the global variable
        fun(ref frame, GlobalsGen<A>.yield(in yieldIx));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool yield<A>(ref StackFrame frame, in A value) =>

        // Create a global variable, this will be the storage for our yield value
        frame.globals.Add(value, out var yieldIx) &&
        
        // Yield what's stored in the global variable
        fun(ref frame, GlobalsGen<A>.yieldConst(in yieldIx));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool dup<A>(ref StackFrame frame) =>
        
        // Push the yield operation
        fun(ref frame, &Pull.dup<A>);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool fun(ref StackFrame frame, in delegate*<ref StackFrame, int> f) =>
        frame.Add(f);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool coroutine(ref StackFrame frame) =>
        
        // Push the no-arg coroutine operation
        fun(ref frame, &Pull.coroutine);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.map<A, B>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bimap<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.bimap<A, B, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bimap1<A, B, C>(ref StackFrame frame, in Func<A, B, C> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.bimap1<A, B, C>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool trimap<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.trimap<A, B, C, D>);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool trimap1<A, B, C, D>(ref StackFrame frame, in Func<A, B, C, D> f) =>
        
        // Push the mapping function
        arg1(ref frame, in f) &&
        
        // Add the map operation
        fun(ref frame, &Pull.trimap1<A, B, C, D>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool bind<A, B>(ref StackFrame frame, in Func<A, Iter<B>> f) =>
        
        // Push the bind function
        arg1(ref frame, in f) &&
        
        // Add the bind operation
        fun(ref frame, &Pull.bind<A, B>);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C>(ref StackFrame frame, Func<A, B, C> f) =>
        
        arg1(ref frame, f) &&
        
        // Push apply operation
        fun(ref frame, &Pull.apply<A, B, C>);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply<A, B, C, D>(ref StackFrame frame, Func<A, B, C, D> f) =>
        
        arg1(ref frame, f) &&
        
        // Push apply operation
        fun(ref frame, &Pull.apply<A, B, C, D>);
    
    [MethodImpl(Optimisations.Default)]
    internal static bool apply1<A, B, C, D>(ref StackFrame frame, Func<A, B, C, D> f) =>
        
        arg1(ref frame, f) &&
        
        // Push apply operation
        fun(ref frame, &Pull.apply1<A, B, C, D>);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B>(ref StackFrame frame) => 
        
        // Push tuple operation
        fun(ref frame, &Pull.tuple<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool tuple<A, B, C>(ref StackFrame frame) => 
        
        // Push tuple operation
        fun(ref frame, &Pull.tuple<A, B, C>);    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool elements<A, B>(ref StackFrame frame) => 
        
        // Push elements operation
        fun(ref frame, &Pull.elements<A, B>);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool elements<A, B, C>(ref StackFrame frame) => 
        
        // Push elements operation
        fun(ref frame, &Pull.elements<A, B, C>);    
}
