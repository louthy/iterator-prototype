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
    [MethodImpl(Optimisations.Default)]
    public static bool @return<A>(ref StackFrame frame, in A value) =>
        frame.vars.Push(value);

    [MethodImpl(Optimisations.Default)]
    public static bool pop<A>(ref StackFrame frame, out A value) =>
        frame.vars.Pop(out value);

    [MethodImpl(Optimisations.Default)]
    public static bool pop<A>(ref StackFrame frame) =>
        frame.vars.Pop<A>();
    
    [MethodImpl(Optimisations.Default)]
    public static bool peek<A>(ref StackFrame frame, out A value) =>
        frame.vars.Peek(out value);
    
    [MethodImpl(Optimisations.Default)]
    public static bool push<A>(ref StackFrame frame, in A value) =>
        frame.vars.Push(in value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg1<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx1, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg2<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx2, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg3<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx3, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg4<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx4, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool update1<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx1) = value;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public static bool update2<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx2) = value;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public static bool update3<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx3) = value;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public static bool update4<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx4) = value;
        return true;
    }

    /*
    /// <summary>
    /// Pops the global variable from the stack and pushes its value onto the stack.
    /// </summary>
    [MethodImpl(Optimisations.Default)]
    public static bool arg<A>(ref StackFrame frame) =>
        frame.vars.Pop<Global<A>>(out var global) &&
        frame.vars.Push(global.Value(ref frame));

    /// <summary>
    /// Pops the global variable and yields its value and a Global structure that can be used to update it via
    /// the `out` parameters.
    /// </summary>
    [MethodImpl(Optimisations.Default)]
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
    [MethodImpl(Optimisations.Default)]
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
    [MethodImpl(Optimisations.Default)]
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

    /// <summary>
    /// Pops the constant global variable and yields value via the `out` parameter.
    /// </summary>
    [MethodImpl(Optimisations.Default)]
    public static bool constarg1<A>(ref StackFrame frame, out A arg)
        where A : class
    {
        arg = Unsafe.As<object, A>(ref Unsafe.AsRef(in frame.args.ManagedArg1));
        return true;
    }

    /// <summary>
    /// Pops the global variable and yields its value and a Global structure that can be used to update it via
    /// the `out` parameters.
    /// </summary>
    [MethodImpl(Optimisations.Default)]
    public static bool arg1<A>(ref StackFrame frame, out A value)
        where A : unmanaged
    {
        value = Unsafe.As<nint, A>(ref Unsafe.AsRef(in frame.args.UnmanagedArg1));
        return true;
    }

    public static bool update1<A>(ref StackFrame frame, in A value)
        where A : unmanaged
    {
        ref var global = ref frame.globals.AtUnmanaged<A>(frame.args.GlobalIx1);
        global = value;
        return true;
    }*/
}