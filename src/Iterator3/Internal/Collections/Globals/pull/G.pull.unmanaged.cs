using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GUnmanaged<A>
    where A : unmanaged
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull(ref StackFrame frame, in ushort ix)
    {
        ref var r = ref frame.globals.AtUnmanaged<A>(ix);
        return frame.vars.PushUnmanaged(in r)
                   ? PullState.Continue
                   : PullState.Void;
    }
    
    public static IterOp pull(in ushort index) =>
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
    static int pull0(ref StackFrame frame) => 
        pull(ref frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull1(ref StackFrame frame) => 
        pull(ref frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull2(ref StackFrame frame) => 
        pull(ref frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull3(ref StackFrame frame) => 
        pull(ref frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull4(ref StackFrame frame) => 
        pull(ref frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull5(ref StackFrame frame) => 
        pull(ref frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int pull6(ref StackFrame frame) => 
        pull(ref frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull7(ref StackFrame frame) => 
        pull(ref frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull8(ref StackFrame frame) => 
        pull(ref frame, 8);
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull9(ref StackFrame frame) => 
        pull(ref frame, 9);
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull10(ref StackFrame frame) => 
        pull(ref frame, 10);
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull11(ref StackFrame frame) => 
        pull(ref frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull12(ref StackFrame frame) => 
        pull(ref frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull13(ref StackFrame frame) => 
        pull(ref frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull14(ref StackFrame frame) => 
        pull(ref frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull15(ref StackFrame frame) => 
        pull(ref frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull16(ref StackFrame frame) => 
        pull(ref frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull17(ref StackFrame frame) => 
        pull(ref frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull18(ref StackFrame frame) => 
        pull(ref frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull19(ref StackFrame frame) => 
        pull(ref frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull20(ref StackFrame frame) => 
        pull(ref frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull21(ref StackFrame frame) => 
        pull(ref frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull22(ref StackFrame frame) => 
        pull(ref frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull23(ref StackFrame frame) => 
        pull(ref frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull24(ref StackFrame frame) => 
        pull(ref frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull25(ref StackFrame frame) => 
        pull(ref frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull26(ref StackFrame frame) => 
        pull(ref frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull27(ref StackFrame frame) => 
        pull(ref frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull28(ref StackFrame frame) => 
        pull(ref frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull29(ref StackFrame frame) => 
        pull(ref frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull30(ref StackFrame frame) => 
        pull(ref frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int pull31(ref StackFrame frame) => 
        pull(ref frame, 31);
}
