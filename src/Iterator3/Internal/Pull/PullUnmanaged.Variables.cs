using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Types;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class PullUnmanaged
{
    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public static void @return<A>(in StackFrame frame, in A value)
        where A : unmanaged
    {
        frame.vars.PushUnmanaged(value, false);
        //Log.terminator($"return {value} : {Ty<A>.Pretty}", in frame);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A>(in StackFrame frame, out A value)
        where A : unmanaged
    {
        frame.vars.PopUnmanaged(out value, false);
        //Log.value($"pop {value} : {Ty<A>.Pretty}", in frame);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg1<A>(in StackFrame frame, out A value)  
        where A : unmanaged =>
        frame.globals.AtUnmanaged(frame.args.GlobalIx1, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg2<A>(in StackFrame frame, out A value)  
        where A : unmanaged =>
        frame.globals.AtUnmanaged(frame.args.GlobalIx2, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg3<A>(in StackFrame frame, out A value)  
        where A : unmanaged =>
        frame.globals.AtUnmanaged(frame.args.GlobalIx3, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg4<A>(in StackFrame frame, out A value)  
        where A : unmanaged =>
        frame.globals.AtUnmanaged(frame.args.GlobalIx4, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg1<A>(in StackFrame frame)  
        where A : unmanaged =>
        ref frame.globals.AtUnmanaged<A>(frame.args.GlobalIx1);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg2<A>(in StackFrame frame)  
        where A : unmanaged =>
        ref frame.globals.AtUnmanaged<A>(frame.args.GlobalIx2);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg3<A>(in StackFrame frame)  
        where A : unmanaged =>
        ref frame.globals.AtUnmanaged<A>(frame.args.GlobalIx3);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg4<A>(in StackFrame frame)  
        where A : unmanaged =>
        ref frame.globals.AtUnmanaged<A>(frame.args.GlobalIx4);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update1<A>(in StackFrame frame, in A value) 
        where A : unmanaged 
    {
        frame.globals.AtUnmanaged<A>(frame.args.GlobalIx1) = value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update2<A>(in StackFrame frame, in A value) 
        where A : unmanaged 
    {
        frame.globals.AtUnmanaged<A>(frame.args.GlobalIx2) = value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update3<A>(in StackFrame frame, in A value) 
        where A : unmanaged 
    {
        frame.globals.AtUnmanaged<A>(frame.args.GlobalIx3) = value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update4<A>(in StackFrame frame, in A value) 
        where A : unmanaged 
    {
        frame.globals.AtUnmanaged<A>(frame.args.GlobalIx4) = value;
    }
}