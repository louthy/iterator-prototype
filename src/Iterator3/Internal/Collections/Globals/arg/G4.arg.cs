using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class G4<A>
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg(ref StackFrame frame, in ushort ix)
    {
        Unsafe.AsRef(in frame.args.GlobalIx4) = ix;
        return PullState.Continue;
    }
    
    public static IterOp arg(in ushort index) =>
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
    static int arg0(ref StackFrame frame) => 
        arg(ref frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg1(ref StackFrame frame) => 
        arg(ref frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg2(ref StackFrame frame) => 
        arg(ref frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg3(ref StackFrame frame) => 
        arg(ref frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg4(ref StackFrame frame) => 
        arg(ref frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg5(ref StackFrame frame) => 
        arg(ref frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int arg6(ref StackFrame frame) => 
        arg(ref frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg7(ref StackFrame frame) => 
        arg(ref frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg8(ref StackFrame frame) => 
        arg(ref frame, 8);
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg9(ref StackFrame frame) => 
        arg(ref frame, 9);
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg10(ref StackFrame frame) => 
        arg(ref frame, 10);
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg11(ref StackFrame frame) => 
        arg(ref frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg12(ref StackFrame frame) => 
        arg(ref frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg13(ref StackFrame frame) => 
        arg(ref frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg14(ref StackFrame frame) => 
        arg(ref frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg15(ref StackFrame frame) => 
        arg(ref frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg16(ref StackFrame frame) => 
        arg(ref frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg17(ref StackFrame frame) => 
        arg(ref frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg18(ref StackFrame frame) => 
        arg(ref frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg19(ref StackFrame frame) => 
        arg(ref frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg20(ref StackFrame frame) => 
        arg(ref frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg21(ref StackFrame frame) => 
        arg(ref frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg22(ref StackFrame frame) => 
        arg(ref frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg23(ref StackFrame frame) => 
        arg(ref frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg24(ref StackFrame frame) => 
        arg(ref frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg25(ref StackFrame frame) => 
        arg(ref frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg26(ref StackFrame frame) => 
        arg(ref frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg27(ref StackFrame frame) => 
        arg(ref frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg28(ref StackFrame frame) => 
        arg(ref frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg29(ref StackFrame frame) => 
        arg(ref frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg30(ref StackFrame frame) => 
        arg(ref frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int arg31(ref StackFrame frame) => 
        arg(ref frame, 31);
}
