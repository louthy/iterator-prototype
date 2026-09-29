#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using IteratorPrototype.Types;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Insert
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(
        in StackFrame frame, 
        IterOp f, 
        int varBytesIn, 
        int varBytesOut, 
        int varObjsIn, 
        int varObjsOut, 
        OpReturn @return) =>
        frame.Prepend(f, varBytesIn, varBytesOut, varObjsIn, varObjsOut, @return);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(in StackFrame frame, in IterOp f, OpReturn @return) =>
        frame.Prepend(f, 0, 0, 0, 0, @return);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        Ty<A>.VarSizes switch
        {
            var (aBytes, aObjs) => 
                fun(in frame, 
                    f, 
                    0,
                    aBytes,
                    0,
                    aObjs, 
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs)) =>
                fun(in frame, 
                    f, 
                    aBytes, bBytes, 
                    aObjs, bObjs, 
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs)) =>
                fun(in frame, 
                    f, 
                    aBytes + bBytes, cBytes, 
                    aObjs  + bObjs, cObjs,
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs)) =>
                fun(in frame, 
                    f, 
                    aBytes + bBytes + cBytes, dBytes, 
                    aObjs  + bObjs  + cObjs, dObjs,
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs)) =>
                fun(in frame, 
                    f,
                    aBytes + bBytes + cBytes + dBytes, eBytes, 
                    aObjs  + bObjs  + cObjs  + dObjs, eObjs,
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E, F>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs)) =>
                fun(in frame, 
                    f,
                    aBytes + bBytes + cBytes + dBytes + eBytes, fBytes, 
                    aObjs  + bObjs  + cObjs  + dObjs  + eObjs, fObjs,
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E, F, G>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes, Ty<G>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs), (gBytes, gObjs)) =>
                fun(in frame, 
                    f,
                    aBytes + bBytes + cBytes + dBytes + eBytes + fBytes, gBytes, 
                    aObjs  + bObjs  + cObjs  + dObjs  + eObjs  + fObjs, gObjs,
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E, F, G, H>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes, Ty<G>.VarSizes,
         Ty<H>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs), (gBytes, gObjs), (hBytes, hObjs)) =>
                fun(in frame,
                    f,
                    aBytes + bBytes + cBytes + dBytes + eBytes + fBytes + gBytes, hBytes,
                    aObjs  + bObjs  + cObjs  + dObjs  + eObjs  + fObjs  + gObjs, hObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun2<A, B>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs)) =>
                fun(in frame, 
                    f,
                    0,
                    aBytes + bBytes,
                    0,
                    aObjs + bObjs, 
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun2<A, B, C>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs)) =>
                fun(in frame,
                    f,
                    aBytes, bBytes + cBytes,
                    aObjs, bObjs   + cObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun2<A, B, C, D>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs)) =>
                fun(in frame,
                    f,
                    aBytes + bBytes, cBytes + dBytes,
                    aObjs  + bObjs, cObjs  + dObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun2<A, B, C, D, E>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs)) =>
                fun(in frame,
                    f,
                    aBytes + bBytes + cBytes, dBytes + eBytes,
                    aObjs  + bObjs  + cObjs, dObjs   + eObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun2<A, B, C, D, E, F>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs))
                =>
                fun(in frame,
                    f,
                    aBytes + bBytes + cBytes + dBytes, eBytes + fBytes,
                    aObjs  + bObjs  + cObjs  + dObjs, eObjs   + fObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun3<A, B, C>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs)) =>
                fun(in frame,
                    f,
                    0,
                    aBytes + bBytes + cBytes,
                    0,
                    aObjs  + bObjs  + cObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun3<A, B, C, D>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs)) =>
                fun(in frame,
                    f,
                    aBytes, bBytes + cBytes + dBytes,
                    aObjs, bObjs   + cObjs  + dObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun3<A, B, C, D, E>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs)) =>
                fun(in frame,
                    f,
                    aBytes + bBytes, cBytes + dBytes + eBytes,
                    aObjs  + bObjs, cObjs   + dObjs  + eObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun3<A, B, C, D, E, F>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs))
                =>
                fun(in frame,
                    f,
                    aBytes + bBytes + cBytes, dBytes + eBytes + fBytes,
                    aObjs  + bObjs  + cObjs, dObjs   + eObjs  + fObjs,
                    @return)
        };

    // Returns 2 values (two pushes to the stack)
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun3<A, B, C, D, E, F, G>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes,
         Ty<G>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs), (gBytes, gObjs)) =>
                fun(in frame,
                    f,
                    aBytes + bBytes + cBytes + dBytes, eBytes + fBytes + gBytes,
                    aObjs  + bObjs  + cObjs  + dObjs, eObjs   + fObjs  + gObjs,
                    @return)
        };
}
