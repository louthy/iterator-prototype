using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GUnmanaged<A>
    where A : unmanaged
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset(in StackFrame frame, in ushort ix) =>
        frame.globals.ResetAtUnmanaged<A>(ix)
            ? PullState.Continue
            : PullState.Void;
    
    public static IterOp reset(in ushort index) =>
        index switch
        {
            0  => &reset0,
            1  => &reset1,
            2  => &reset2,
            3  => &reset3,
            4  => &reset4,
            5  => &reset5,
            6  => &reset6,
            7  => &reset7,
            8  => &reset8,
            9  => &reset9,
            10 => &reset10,
            11 => &reset11,
            12 => &reset12,
            13 => &reset13,
            14 => &reset14,
            15 => &reset15,
            16 => &reset16,
            17 => &reset17,
            18 => &reset18,
            19 => &reset19,
            20 => &reset20,
            21 => &reset21,
            22 => &reset22,
            23 => &reset23,
            24 => &reset24,
            25 => &reset25,
            26 => &reset26,
            27 => &reset27,
            28 => &reset28,
            29 => &reset29,
            30 => &reset30,
            31 => &reset31,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset0(in StackFrame frame) => 
        reset(in frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int reset1(in StackFrame frame) => 
        reset(in frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int reset2(in StackFrame frame) => 
        reset(in frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset3(in StackFrame frame) => 
        reset(in frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset4(in StackFrame frame) => 
        reset(in frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int reset5(in StackFrame frame) => 
        reset(in frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int reset6(in StackFrame frame) => 
        reset(in frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset7(in StackFrame frame) => 
        reset(in frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset8(in StackFrame frame) => 
        reset(in frame, 8);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset9(in StackFrame frame) => 
        reset(in frame, 9);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset10(in StackFrame frame) => 
        reset(in frame, 10);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset11(in StackFrame frame) => 
        reset(in frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset12(in StackFrame frame) => 
        reset(in frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset13(in StackFrame frame) => 
        reset(in frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset14(in StackFrame frame) => 
        reset(in frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset15(in StackFrame frame) => 
        reset(in frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset16(in StackFrame frame) => 
        reset(in frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset17(in StackFrame frame) => 
        reset(in frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset18(in StackFrame frame) => 
        reset(in frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset19(in StackFrame frame) => 
        reset(in frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset20(in StackFrame frame) => 
        reset(in frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset21(in StackFrame frame) => 
        reset(in frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset22(in StackFrame frame) => 
        reset(in frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset23(in StackFrame frame) => 
        reset(in frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset24(in StackFrame frame) => 
        reset(in frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset25(in StackFrame frame) => 
        reset(in frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset26(in StackFrame frame) => 
        reset(in frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset27(in StackFrame frame) => 
        reset(in frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset28(in StackFrame frame) => 
        reset(in frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset29(in StackFrame frame) => 
        reset(in frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset30(in StackFrame frame) => 
        reset(in frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int reset31(in StackFrame frame) => 
        reset(in frame, 31);
}
