using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class G1
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg(in StackFrame frame, ushort ix)
    {
        Unsafe.AsRef(in frame.args.GlobalIx1) = ix;
        return PullState.Continue;
    }
    
    public static IterOp arg(ushort index) =>
        index switch
        {
            0  => &arg0,
            1  => &arg1,
            2  => &arg2,
            3  => &arg3,
            4  => &arg4,
            5  => &arg5,
            6  => &arg6,
            7  => &arg7,
            8  => &arg8,
            9  => &arg9,
            10 => &arg10,
            11 => &arg11,
            12 => &arg12,
            13 => &arg13,
            14 => &arg14,
            15 => &arg15,
            16 => &arg16,
            17 => &arg17,
            18 => &arg18,
            19 => &arg19,
            20 => &arg20,
            21 => &arg21,
            22 => &arg22,
            23 => &arg23,
            24 => &arg24,
            25 => &arg25,
            26 => &arg26,
            27 => &arg27,
            28 => &arg28,
            29 => &arg29,
            30 => &arg30,
            31 => &arg31,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };
       
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg0(in StackFrame frame) => 
        arg(in frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg1(in StackFrame frame) => 
        arg(in frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg2(in StackFrame frame) => 
        arg(in frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg3(in StackFrame frame) => 
        arg(in frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg4(in StackFrame frame) => 
        arg(in frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg5(in StackFrame frame) => 
        arg(in frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg6(in StackFrame frame) => 
        arg(in frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg7(in StackFrame frame) => 
        arg(in frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg8(in StackFrame frame) => 
        arg(in frame, 8);
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg9(in StackFrame frame) => 
        arg(in frame, 9);
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg10(in StackFrame frame) => 
        arg(in frame, 10);
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg11(in StackFrame frame) => 
        arg(in frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg12(in StackFrame frame) => 
        arg(in frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg13(in StackFrame frame) => 
        arg(in frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg14(in StackFrame frame) => 
        arg(in frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg15(in StackFrame frame) => 
        arg(in frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg16(in StackFrame frame) => 
        arg(in frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg17(in StackFrame frame) => 
        arg(in frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg18(in StackFrame frame) => 
        arg(in frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg19(in StackFrame frame) => 
        arg(in frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg20(in StackFrame frame) => 
        arg(in frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg21(in StackFrame frame) => 
        arg(in frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg22(in StackFrame frame) => 
        arg(in frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg23(in StackFrame frame) => 
        arg(in frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg24(in StackFrame frame) => 
        arg(in frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg25(in StackFrame frame) => 
        arg(in frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg26(in StackFrame frame) => 
        arg(in frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg27(in StackFrame frame) => 
        arg(in frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg28(in StackFrame frame) => 
        arg(in frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg29(in StackFrame frame) => 
        arg(in frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg30(in StackFrame frame) => 
        arg(in frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg31(in StackFrame frame) => 
        arg(in frame, 31);
}
