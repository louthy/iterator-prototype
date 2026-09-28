using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using IteratorPrototype.Types;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public static void @return<A>(in StackFrame frame, in A value)
    {
        frame.vars.Push(value, false);
        //Log.terminator($"return {value} : {Ty<A>.Pretty}", in frame);
    }

    /// <summary>
    /// Pushes the return value to the stack
    /// </summary>
    [MethodImpl(Optimisations.InliningOnly)]
    public static void @return<A, B>(in StackFrame frame, in A value1, in B value2)
    {
        frame.vars.Push(value1, value2, false);
        //Log.terminator($"return ({value1}, {value2}) : ({Ty<A>.Pretty}, {Ty<B>.Pretty})", in frame);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void dup<A>(in StackFrame frame) =>
        frame.vars.Dup<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A>(in StackFrame frame, out A value)
    {
        frame.vars.Pop(out value, false);
        //Log.value($"pop {value} : {Ty<A>.Pretty}", in frame);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B, C>(in StackFrame frame, out A value1, out B value2, out C value3)
    {
        frame.vars.Pop(out value1, out value2, out value3);
        //Log.value($"pop ({value1}, {value2}, {value3}) : ({Ty<A>.Pretty}, {Ty<B>.Pretty}, {Ty<C>.Pretty})", in frame);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B, C, D>(in StackFrame frame, out A value1, out B value2, out C value3, out D value4) =>
        frame.vars.Pop(out value1, out value2, out value3, out value4);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B, C, D, E>(in StackFrame frame, out A value1, out B value2, out C value3, out D value4, out E value5) =>
        frame.vars.Pop(out value1, out value2, out value3, out value4, out value5);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B, C, D, E, F>(in StackFrame frame, out A value1, out B value2, out C value3, out D value4, out E value5, out F value6) =>
        frame.vars.Pop(out value1, out value2, out value3, out value4, out value5, out value6);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B, C, D, E, F, G>(in StackFrame frame, out A value1, out B value2, out C value3, out D value4, out E value5, out F value6, out G value7) =>
        frame.vars.Pop(out value1, out value2, out value3, out value4, out value5, out value6, out value7);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B>(in StackFrame frame, out A value1, out B value2)
    {
        frame.vars.Pop(out value1, out value2);
        //Log.value($"pop ({value1}, {value2}) : ({Ty<A>.Pretty}, {Ty<B>.Pretty})", in frame);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A>(in StackFrame frame) =>
        frame.vars.Pop<A>(false);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void pop<A, B>(in StackFrame frame) =>
        frame.vars.Pop<A, B>(false);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static void peek<A>(in StackFrame frame, out A value) =>
        frame.vars.Peek(out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static void peek<A, B>(in StackFrame frame, out A value1, out B value2) =>
        frame.vars.Peek(out value1, out value2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static void push<A>(in StackFrame frame, in A value) =>
        frame.vars.Push(in value, false);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static void push<A, B>(in StackFrame frame, in A value1, in B value2) =>
        frame.vars.Push(in value1, in value2, false);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg1<A>(in StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx1, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg2<A>(in StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx2, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg3<A>(in StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx3, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void arg4<A>(in StackFrame frame, out A value) =>
        frame.globals.At(frame.args.GlobalIx4, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update1<A>(in StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx1) = value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update2<A>(in StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx2) = value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update3<A>(in StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx3) = value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static void update4<A>(in StackFrame frame, in A value)
    {
        frame.globals.At<A>(frame.args.GlobalIx4) = value;
    }
}