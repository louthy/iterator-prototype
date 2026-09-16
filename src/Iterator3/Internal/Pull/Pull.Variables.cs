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
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool @return<A>(ref StackFrame frame, in A value)
    {
        //Log.value($"return: {value}", ref frame);
        return frame.vars.Push(value);
    }

    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool @return<A, B>(ref StackFrame frame, in A value1, in B value2)
    {
        //Log.value($"return: {value}", ref frame);
        return frame.vars.Push(value1, value2);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool dup<A>(ref StackFrame frame) =>
        frame.vars.Dup<A>();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pop<A>(ref StackFrame frame, out A value) =>
        frame.vars.Pop(out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pop<A, B>(ref StackFrame frame, out A value1, out B value2) =>
        frame.vars.Pop(out value1, out value2);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pop<A>(ref StackFrame frame) =>
        frame.vars.Pop<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pop<A, B>(ref StackFrame frame) =>
        frame.vars.Pop<A, B>();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool peek<A>(ref StackFrame frame, out A value) =>
        frame.vars.Peek(out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool peek<A, B>(ref StackFrame frame, out A value1, out B value2) =>
        frame.vars.Peek(out value1, out value2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool push<A>(ref StackFrame frame, in A value) =>
        frame.vars.Push(in value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool push<A, B>(ref StackFrame frame, in A value1, in B value2) =>
        frame.vars.Push(in value1, in value2);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx1, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx2, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx3, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(ref StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx4, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update1<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx1) = value;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update2<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx2) = value;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update3<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx3) = value;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update4<A>(ref StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx4) = value;
        return true;
    }
}