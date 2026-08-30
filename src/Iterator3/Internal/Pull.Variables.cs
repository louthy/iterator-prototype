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
            Log.value($"pop arg {value} [ix:{arg.Index}]", ref frame);
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
            Log.value($"pop arg {arg.Value(ref frame)} [ix:{arg.Index}]", ref frame);
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
            Log.value($"pop arg {arg}", ref frame);
            return true;
        }
        else
        {
            arg = default!;
            return false;
        }
    }

    
    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PullState assign<A>(ref StackFrame frame) =>
        
        // Get the left-hand side of the assignment
        frame.vars.Pop<Global<A>>(out var lhs) &&

        // Get the right-hand side of the assignment
        frame.vars.Pop<A>(out var rhs) &&
        
        // Do the assigning
        lhs.Update(ref frame, rhs)

      | Log.value($"global({lhs.Index}) = {rhs}", ref frame)
        
            ? @continue(ref frame)
            : empty(ref frame);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool @const<A>(ref StackFrame frame, out A value)
    {
        if(frame.vars.Pop(out value))
        {
            Log.value($"pop const {value}", ref frame);
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool globalM<A>(ref StackFrame frame, out Global<A> g) =>
        frame.vars.Pop(out g)

      | Log.value($"pop global({g.Index}) out {g.Value(ref frame)}", ref frame);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool global<A>(ref StackFrame frame, out A value)
    {
        if(frame.vars.Pop<Global<A>>(out var global))
        {
            value = global.Value(ref frame);
            Log.value($"pop global({global.Index}) out {value}", ref frame);
            return true;
        }
        else
        {
            value = default!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool global<A>(ref StackFrame frame) =>
        frame.vars.Pop<Global<A>>(out var g) &&
        frame.vars.Push(in g.Value(ref frame))

      | Log.value($"swap global({g.Index}) for {g.Value(ref frame)}", ref frame);
      */
    
}