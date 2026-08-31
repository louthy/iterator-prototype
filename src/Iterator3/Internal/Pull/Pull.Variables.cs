using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool @return<A>(ref StackFrame frame, in A value) =>
        frame.vars.Push(value);
    
    /// <summary>
    /// Pops the global variable from the stack and pushes its value onto the stack.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool arg<A>(ref StackFrame frame) =>
        frame.vars.Pop<Global<A>>(out var global) &&
        frame.vars.Push(global.Value(ref frame));
    
    /// <summary>
    /// Pops the global variable and yields its value and a Global structure that can be used to update it via
    /// the `out` parameters.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool arg<A>(ref StackFrame frame, out A value, out Global<A> arg)
    {
        if (frame.vars.Pop(out arg))
        {
            value = arg.Value(ref frame);
            return true;
        }
        else
        {
            value = default!;
            arg = default!;
            return false;
        }
    }
    
    /// <summary>
    /// Pops the global variable and yields its Global structure that can be used to update it via
    /// the `out` parameter.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool arg<A>(ref StackFrame frame, out Global<A> arg)
    {
        if (frame.vars.Pop(out arg))
        {
            return true;
        }
        else
        {
            arg = default!;
            return false;
        }
    }
    
    /// <summary>
    /// Pops the constant global variable and yields value via the `out` parameter.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool constarg<A>(ref StackFrame frame, out A arg)
    {
        if (frame.vars.Pop(out arg))
        {
            return true;
        }
        else
        {
            arg = default!;
            return false;
        }
    }
}