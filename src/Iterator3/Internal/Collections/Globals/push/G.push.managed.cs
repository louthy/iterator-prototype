using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GManaged<A>
    where A : class
{
    [MethodImpl(Optimisations.InliningOnly)]
    static int push(in StackFrame frame, ushort ix)
    {
        // Get a reference to the global
        ref var r = ref frame.globals.AtManaged<A>(ix);
        
        // Pop the value from the stack
        if (frame.vars.PopManaged<A>(out var x, false))
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

    public static IterOp push(ushort index) =>
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
    static int push0(in StackFrame frame) => 
        push(in frame, 0);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push1(in StackFrame frame) => 
        push(in frame, 1);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push2(in StackFrame frame) => 
        push(in frame, 2);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push3(in StackFrame frame) => 
        push(in frame, 3);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push4(in StackFrame frame) => 
        push(in frame, 4);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push5(in StackFrame frame) => 
        push(in frame, 5);
    
    [MethodImpl(Optimisations.InliningOnly)] 
    static int push6(in StackFrame frame) => 
        push(in frame, 6);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push7(in StackFrame frame) => 
        push(in frame, 7);
    
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push8(in StackFrame frame) => 
        push(in frame, 8);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push9(in StackFrame frame) => 
        push(in frame, 9);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push10(in StackFrame frame) => 
        push(in frame, 10);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push11(in StackFrame frame) => 
        push(in frame, 11);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push12(in StackFrame frame) => 
        push(in frame, 12);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push13(in StackFrame frame) => 
        push(in frame, 13);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push14(in StackFrame frame) => 
        push(in frame, 14);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push15(in StackFrame frame) => 
        push(in frame, 15);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push16(in StackFrame frame) => 
        push(in frame, 16);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push17(in StackFrame frame) => 
        push(in frame, 17);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push18(in StackFrame frame) => 
        push(in frame, 18);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push19(in StackFrame frame) => 
        push(in frame, 19);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push20(in StackFrame frame) => 
        push(in frame, 20);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push21(in StackFrame frame) => 
        push(in frame, 21);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push22(in StackFrame frame) => 
        push(in frame, 22);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push23(in StackFrame frame) => 
        push(in frame, 23);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push24(in StackFrame frame) => 
        push(in frame, 24);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push25(in StackFrame frame) => 
        push(in frame, 25);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push26(in StackFrame frame) => 
        push(in frame, 26);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push27(in StackFrame frame) => 
        push(in frame, 27);

    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push28(in StackFrame frame) => 
        push(in frame, 28);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push29(in StackFrame frame) => 
        push(in frame, 29);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push30(in StackFrame frame) => 
        push(in frame, 30);
    
    [MethodImpl(Optimisations.InliningOnly)]
    static int push31(in StackFrame frame) => 
        push(in frame, 31);
}
