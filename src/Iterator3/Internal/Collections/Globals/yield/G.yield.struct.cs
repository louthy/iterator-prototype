using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GStruct<A>
    where A : struct
{
    public static delegate*<ref StackFrame, int> yield(in ushort index) =>
        index switch
        {
            0  => &yield0,
            1  => &yield1,
            2  => &yield2,
            3  => &yield3,
            4  => &yield4,
            5  => &yield5,
            6  => &yield6,
            7  => &yield7,
            8  => &yield8,
            9  => &yield9,
            10 => &yield10,
            11 => &yield11,
            12 => &yield12,
            13 => &yield13,
            14 => &yield14,
            15 => &yield15,
            16 => &yield16,
            17 => &yield17,
            18 => &yield18,
            19 => &yield19,
            20 => &yield20,
            21 => &yield21,
            22 => &yield22,
            23 => &yield23,
            24 => &yield24,
            25 => &yield25,
            26 => &yield26,
            27 => &yield27,
            28 => &yield28,
            29 => &yield29,
            30 => &yield30,
            31 => &yield31,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };       
       
    [MethodImpl(Optimisations.Default)]
    static int yield0(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 0);
    
    [MethodImpl(Optimisations.Default)] 
    static int yield1(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 1);
    
    [MethodImpl(Optimisations.Default)] 
    static int yield2(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 2);
    
    [MethodImpl(Optimisations.Default)]
    static int yield3(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 3);

    
    [MethodImpl(Optimisations.Default)]
    static int yield4(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 4);
    
    [MethodImpl(Optimisations.Default)] 
    static int yield5(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 5);
    
    [MethodImpl(Optimisations.Default)] 
    static int yield6(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 6);
    
    [MethodImpl(Optimisations.Default)]
    static int yield7(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 7);
    
    
    [MethodImpl(Optimisations.Default)]
    static int yield8(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 8);
    [MethodImpl(Optimisations.Default)]
    static int yield9(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 9);
    [MethodImpl(Optimisations.Default)]
    static int yield10(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 10);
    [MethodImpl(Optimisations.Default)]
    static int yield11(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 11);

    
    [MethodImpl(Optimisations.Default)]
    static int yield12(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 12);
    
    [MethodImpl(Optimisations.Default)]
    static int yield13(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 13);
    
    [MethodImpl(Optimisations.Default)]
    static int yield14(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 14);
    
    [MethodImpl(Optimisations.Default)]
    static int yield15(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 15);

    
    [MethodImpl(Optimisations.Default)]
    static int yield16(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 16);
    
    [MethodImpl(Optimisations.Default)]
    static int yield17(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 17);
    
    [MethodImpl(Optimisations.Default)]
    static int yield18(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 18);
    
    [MethodImpl(Optimisations.Default)]
    static int yield19(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 19);

    
    [MethodImpl(Optimisations.Default)]
    static int yield20(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 20);
    
    [MethodImpl(Optimisations.Default)]
    static int yield21(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 21);
    
    [MethodImpl(Optimisations.Default)]
    static int yield22(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 22);
    
    [MethodImpl(Optimisations.Default)]
    static int yield23(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 23);

    
    [MethodImpl(Optimisations.Default)]
    static int yield24(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 24);
    
    [MethodImpl(Optimisations.Default)]
    static int yield25(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 25);
    
    [MethodImpl(Optimisations.Default)]
    static int yield26(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 26);
    
    [MethodImpl(Optimisations.Default)]
    static int yield27(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 27);

    
    [MethodImpl(Optimisations.Default)]
    static int yield28(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 28);
    
    [MethodImpl(Optimisations.Default)]
    static int yield29(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 29);
    
    [MethodImpl(Optimisations.Default)]
    static int yield30(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 30);
    
    [MethodImpl(Optimisations.Default)]
    static int yield31(ref StackFrame frame) => 
        frame.vars.YieldStruct<A>(ref frame, 31);
}
