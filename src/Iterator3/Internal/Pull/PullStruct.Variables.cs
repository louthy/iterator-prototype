using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Types;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class PullStruct
{
    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool @return<A>(in StackFrame frame, in A value) 
        where A : struct
    {
        var r = frame.vars.PushStruct(value, false);
        //Log.terminator($"return {value} : {Ty<A>.Pretty}", in frame);
        return r;        
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool pop<A>(in StackFrame frame, out A value)
        where A : struct
    {
        var r = frame.vars.PopStruct(out value, false);
        //Log.value($"pop {value} : {Ty<A>.Pretty}", in frame);
        return r;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg1<A>(in StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx1, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg2<A>(in StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx2, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg3<A>(in StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx3, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool arg4<A>(in StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx4, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg1<A>(in StackFrame frame)  
        where A : struct =>
        ref frame.globals.AtStruct<A>(frame.args.GlobalIx1);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg2<A>(in StackFrame frame)  
        where A : struct =>
        ref frame.globals.AtStruct<A>(frame.args.GlobalIx2);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg3<A>(in StackFrame frame)  
        where A : struct =>
        ref frame.globals.AtStruct<A>(frame.args.GlobalIx3);

    [MethodImpl(Optimisations.InliningOnly)]
    public static ref A arg4<A>(in StackFrame frame)  
        where A : struct =>
        ref frame.globals.AtStruct<A>(frame.args.GlobalIx4);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update1<A>(in StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx1) = value;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update2<A>(in StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx2) = value;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update3<A>(in StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx3) = value;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool update4<A>(in StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx4) = value;
        return true;
    }
}