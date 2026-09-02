using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GStruct<A>
    where A : struct
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int push(ref StackFrame frame, in ushort ix)
    {
        // Get a reference to the global
        ref var r = ref frame.globals.AtStruct<A>(ix);
        
        // Pop the value from the stack
        if (frame.vars.PopStruct<A>(out var x))
        {
            // Set the global to be what was on the top of the stack
            r = x;
            return PullState.Continue;
        }
        else
        {
            return PullState.Void;
        }
    }

    public static delegate*<ref StackFrame, int> push(in ushort index) =>
        index switch
        {
            0  => &push0,
            1  => &push1,
            2  => &push2,
            3  => &push3,
            4  => &push4,
            5  => &push5,
            6  => &push6,
            7  => &push7,
            8  => &push8,
            9  => &push9,
            10 => &push10,
            11 => &push11,
            12 => &push12,
            13 => &push13,
            14 => &push14,
            15 => &push15,
            16 => &push16,
            17 => &push17,
            18 => &push18,
            19 => &push19,
            20 => &push20,
            21 => &push21,
            22 => &push22,
            23 => &push23,
            24 => &push24,
            25 => &push25,
            26 => &push26,
            27 => &push27,
            28 => &push28,
            29 => &push29,
            30 => &push30,
            31 => &push31,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };       
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push0(ref StackFrame frame) => 
        push(ref frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push1(ref StackFrame frame) => 
        push(ref frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push2(ref StackFrame frame) => 
        push(ref frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push3(ref StackFrame frame) => 
        push(ref frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push4(ref StackFrame frame) => 
        push(ref frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push5(ref StackFrame frame) => 
        push(ref frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push6(ref StackFrame frame) => 
        push(ref frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push7(ref StackFrame frame) => 
        push(ref frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push8(ref StackFrame frame) => 
        push(ref frame, 8);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push9(ref StackFrame frame) => 
        push(ref frame, 9);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push10(ref StackFrame frame) => 
        push(ref frame, 10);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push11(ref StackFrame frame) => 
        push(ref frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push12(ref StackFrame frame) => 
        push(ref frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push13(ref StackFrame frame) => 
        push(ref frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push14(ref StackFrame frame) => 
        push(ref frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push15(ref StackFrame frame) => 
        push(ref frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push16(ref StackFrame frame) => 
        push(ref frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push17(ref StackFrame frame) => 
        push(ref frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push18(ref StackFrame frame) => 
        push(ref frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push19(ref StackFrame frame) => 
        push(ref frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push20(ref StackFrame frame) => 
        push(ref frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push21(ref StackFrame frame) => 
        push(ref frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push22(ref StackFrame frame) => 
        push(ref frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push23(ref StackFrame frame) => 
        push(ref frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push24(ref StackFrame frame) => 
        push(ref frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push25(ref StackFrame frame) => 
        push(ref frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push26(ref StackFrame frame) => 
        push(ref frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push27(ref StackFrame frame) => 
        push(ref frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push28(ref StackFrame frame) => 
        push(ref frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push29(ref StackFrame frame) => 
        push(ref frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push30(ref StackFrame frame) => 
        push(ref frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push31(ref StackFrame frame) => 
        push(ref frame, 31);
}
