using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GUnmanaged<A>
    where A : unmanaged
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull(in StackFrame frame, ushort ix)
    {
        ref var r = ref frame.globals.AtUnmanaged<A>(ix);
        return frame.vars.PushUnmanaged(in r, false)
                   ? PullState.Continue
                   : PullState.Void;
    }
    
    public static IterOp pull(ushort index) =>
        index switch
        {
            0  => &pull0,
            1  => &pull1,
            2  => &pull2,
            3  => &pull3,
            4  => &pull4,
            5  => &pull5,
            6  => &pull6,
            7  => &pull7,
            8  => &pull8,
            9  => &pull9,
            10 => &pull10,
            11 => &pull11,
            12 => &pull12,
            13 => &pull13,
            14 => &pull14,
            15 => &pull15,
            16 => &pull16,
            17 => &pull17,
            18 => &pull18,
            19 => &pull19,
            20 => &pull20,
            21 => &pull21,
            22 => &pull22,
            23 => &pull23,
            24 => &pull24,
            25 => &pull25,
            26 => &pull26,
            27 => &pull27,
            28 => &pull28,
            29 => &pull29,
            30 => &pull30,
            31 => &pull31,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };
       
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull0(in StackFrame frame) => 
        pull(in frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull1(in StackFrame frame) => 
        pull(in frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull2(in StackFrame frame) => 
        pull(in frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull3(in StackFrame frame) => 
        pull(in frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull4(in StackFrame frame) => 
        pull(in frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull5(in StackFrame frame) => 
        pull(in frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull6(in StackFrame frame) => 
        pull(in frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull7(in StackFrame frame) => 
        pull(in frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull8(in StackFrame frame) => 
        pull(in frame, 8);
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull9(in StackFrame frame) => 
        pull(in frame, 9);
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull10(in StackFrame frame) => 
        pull(in frame, 10);
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull11(in StackFrame frame) => 
        pull(in frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull12(in StackFrame frame) => 
        pull(in frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull13(in StackFrame frame) => 
        pull(in frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull14(in StackFrame frame) => 
        pull(in frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull15(in StackFrame frame) => 
        pull(in frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull16(in StackFrame frame) => 
        pull(in frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull17(in StackFrame frame) => 
        pull(in frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull18(in StackFrame frame) => 
        pull(in frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull19(in StackFrame frame) => 
        pull(in frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull20(in StackFrame frame) => 
        pull(in frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull21(in StackFrame frame) => 
        pull(in frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull22(in StackFrame frame) => 
        pull(in frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull23(in StackFrame frame) => 
        pull(in frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull24(in StackFrame frame) => 
        pull(in frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull25(in StackFrame frame) => 
        pull(in frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull26(in StackFrame frame) => 
        pull(in frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull27(in StackFrame frame) => 
        pull(in frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull28(in StackFrame frame) => 
        pull(in frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull29(in StackFrame frame) => 
        pull(in frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull30(in StackFrame frame) => 
        pull(in frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull31(in StackFrame frame) => 
        pull(in frame, 31);
}
