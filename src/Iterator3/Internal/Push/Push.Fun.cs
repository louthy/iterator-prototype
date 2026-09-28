#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using IteratorPrototype.Types;

namespace IteratorPrototype.Iterator3;

static unsafe partial class Push
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(in StackFrame frame, in IterOp f, int varBytes, int varObjs, OpReturn @return) =>
        frame.Add(f, varBytes, varObjs, @return);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun(in StackFrame frame, in IterOp f, OpReturn @return) =>
        frame.Add(f, 0, 0, @return);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        Ty<A>.VarSizes switch
        {
            var (aBytes, aObjs) => 
                fun(in frame, 
                    f, 
                    aBytes, 
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
                    bBytes - aBytes, 
                    bObjs - aObjs, 
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs)) =>
                fun(in frame, 
                    f, 
                    cBytes - (aBytes + bBytes), 
                    cObjs - (aObjs + bObjs),
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs)) =>
                fun(in frame, 
                    f, 
                    dBytes - (aBytes + bBytes + cBytes), 
                    dObjs - (aObjs + bObjs + cObjs),
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs)) =>
                fun(in frame, 
                    f,
                    eBytes - (aBytes + bBytes + cBytes + dBytes), 
                    eObjs - (aObjs + bObjs + cObjs + dObjs),
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E, F>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs)) =>
                fun(in frame, 
                    f,
                    fBytes - (aBytes + bBytes + cBytes + dBytes + eBytes), 
                    fObjs  - (aObjs  + bObjs  + cObjs  + dObjs + eObjs),
                    @return)
        };

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool fun<A, B, C, D, E, F, G>(in StackFrame frame, in IterOp f, OpReturn @return) =>
        (Ty<A>.VarSizes, Ty<B>.VarSizes, Ty<C>.VarSizes, Ty<D>.VarSizes, Ty<E>.VarSizes, Ty<F>.VarSizes, Ty<G>.VarSizes) switch
        {
            var ((aBytes, aObjs), (bBytes, bObjs), (cBytes, cObjs), (dBytes, dObjs), (eBytes, eObjs), (fBytes, fObjs), (gBytes, gObjs)) =>
                fun(in frame, 
                    f,
                    gBytes - (aBytes + bBytes + cBytes + dBytes + eBytes + fBytes), 
                    gObjs  - (aObjs  + bObjs  + cObjs  + dObjs  + eObjs + fObjs),
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
                    hBytes - (aBytes + bBytes + cBytes + dBytes + eBytes + fBytes + gBytes),
                    hObjs  - (aObjs  + bObjs  + cObjs  + dObjs  + eObjs  + fObjs  + gObjs),
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
                    aBytes + bBytes, aObjs + bObjs, 
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
                    bBytes + cBytes - aBytes,
                    bObjs  + cObjs  - aObjs,
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
                    cBytes + dBytes - (aBytes + bBytes),
                    cObjs  + dObjs  - (aObjs  + bObjs),
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
                    dBytes + eBytes - (aBytes + bBytes + cBytes),
                    dObjs  + eObjs  - (aObjs  + bObjs  + cObjs),
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
                    eBytes + fBytes - (aBytes + bBytes + cBytes + dBytes),
                    eObjs  + fObjs  - (aObjs  + bObjs  + cObjs  + dObjs),
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
                    aBytes + bBytes + cBytes,
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
                    bBytes + cBytes + dBytes - aBytes,
                    bObjs  + cObjs  + dObjs  - aObjs,
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
                    cBytes + dBytes + eBytes - (aBytes + bBytes),
                    cObjs  + dObjs  + eObjs  - (aObjs  + bObjs),
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
                    dBytes + eBytes + fBytes - (aBytes + bBytes + cBytes),
                    dObjs  + eObjs  + fObjs  - (aObjs  + bObjs  + cObjs),
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
                    eBytes + fBytes + gBytes - (aBytes + bBytes + cBytes + dBytes),
                    eObjs  + fObjs  + gObjs  - (aObjs  + bObjs  + cObjs  + dObjs),
                    @return)
        };
}
