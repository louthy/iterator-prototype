using System.Runtime.CompilerServices;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class PullStruct
{
    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(Optimisations.Default)]
    public static bool @return<A>(ref StackFrame frame, in A value) 
        where A : struct =>
        frame.vars.PushStruct(value);

    [MethodImpl(Optimisations.Default)]
    public static bool pop<A>(ref StackFrame frame, out A value)
        where A : struct =>
        frame.vars.PopStruct(out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg1<A>(ref StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx1, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg2<A>(ref StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx2, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg3<A>(ref StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx3, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool arg4<A>(ref StackFrame frame, out A value)  
        where A : struct =>
        frame.globals.AtStruct(frame.args.GlobalIx4, out value);

    [MethodImpl(Optimisations.Default)]
    public static bool update1<A>(ref StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx1) = value;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public static bool update2<A>(ref StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx2) = value;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public static bool update3<A>(ref StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx3) = value;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public static bool update4<A>(ref StackFrame frame, in A value) 
        where A : struct 
    {
        frame.globals.AtStruct<A>(frame.args.GlobalIx4) = value;
        return true;
    }
}