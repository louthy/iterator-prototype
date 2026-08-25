using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly unsafe struct Next
{
    internal readonly delegate*<ref StackFrame, bool> Fun;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal Next(delegate*<ref StackFrame, bool> f) : this() =>
        Fun = f;
}

static class Pull
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool empty(ref StackFrame frame) =>
        false;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool yield(ref StackFrame frame) =>
        true;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool map<A, B>(ref StackFrame frame) =>
        
        // Take the value off the stack
        frame.Pop<A>(out var a) &&

        // Peek at the map function
        frame.PopObj<Func<A, B>>(out var f) &&

        // Push the mapped value on the stack
        frame.Push(f(a));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constant<S>(ref StackFrame frame) 
        where S : unmanaged =>
        
        // Take the state value off the top of the stack, which will leave the
        // constant state behind 
        frame.PopState<S>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool iterable<T, IS, A>(ref StackFrame frame)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged =>
        T.Next<A>(ref frame);
        /*
        // Take the state value off the stack
        frame.PopState<IS>(out var s) &&

        // Take the iterable instance off the stack
        frame.PopObj<K<T, A>>(out var ta) &&

        // Step the iterable
        T.StepImmutable(in ta, in s, out var head, out s) &&

        // Push the new state on the stack
        frame.PushState(in s) &&

        // Push the acquired head value onto the stack
        frame.Push(in head);
        */
    
}

static unsafe class Push
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constant<S>(ref StackFrame frame, in S constant) 
        where S : unmanaged =>
        
        // Push the constant state value on to the stack
        frame.UnshiftState(in constant) &&
        
        // Push the constant operation
        frame.Add(&Pull.constant<S>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool map<A, B>(ref StackFrame frame, in Func<A, B> f) =>
        
        // Push the mapping function
        frame.UnshiftObj(in f) &&
        
        // Add the map operation
        frame.Add(&Pull.map<A, B>);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool iterable<T, IS, A>(ref StackFrame frame, in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged  =>
        
        // Push the initial iterable state onto the stack
        frame.UnshiftState(T.SetupImmutable(in ta)) &&
        
        // Push the iterable instance onto the stack
        frame.UnshiftObj(in ta) &&
        
        // Push the operation
        frame.Add(&Pull.iterable<T, IS, A>);

}
