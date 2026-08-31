using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GStruct<A>
    where A : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield(ref StackFrame frame, in ushort ix)
    {
        if (frame.vars.PopStruct<A>(out var value))
        {
            // Fill the yield variable with the output of whatever ran before us
            ref var r = ref frame.globals.AtStruct<A>(ix);
            r = value;

            // Flag that this co-routine has yielded something
            frame.tops.CurrentYield++;

            // Start a new co-routine for the value
            frame.StartScope();

            // Push it onto the 'vars' stack in the new co-routine
            frame.vars.PushStruct(r);

            return PullState.Continue;
        }
        else
        {
            return PullState.Void;
        }
    }
    
    public static delegate*<ref StackFrame, PullState> yield(in ushort index) =>
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
       
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield0(ref StackFrame frame) => 
        yield(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)] 
    static PullState yield1(ref StackFrame frame) => 
        yield(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)] 
    static PullState yield2(ref StackFrame frame) => 
        yield(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield3(ref StackFrame frame) => 
        yield(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield4(ref StackFrame frame) => 
        yield(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)] 
    static PullState yield5(ref StackFrame frame) => 
        yield(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)] 
    static PullState yield6(ref StackFrame frame) => 
        yield(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield7(ref StackFrame frame) => 
        yield(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield8(ref StackFrame frame) => 
        yield(ref frame, 8);
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield9(ref StackFrame frame) => 
        yield(ref frame, 9);
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield10(ref StackFrame frame) => 
        yield(ref frame, 10);
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield11(ref StackFrame frame) => 
        yield(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield12(ref StackFrame frame) => 
        yield(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield13(ref StackFrame frame) => 
        yield(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield14(ref StackFrame frame) => 
        yield(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield15(ref StackFrame frame) => 
        yield(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield16(ref StackFrame frame) => 
        yield(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield17(ref StackFrame frame) => 
        yield(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield18(ref StackFrame frame) => 
        yield(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield19(ref StackFrame frame) => 
        yield(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield20(ref StackFrame frame) => 
        yield(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield21(ref StackFrame frame) => 
        yield(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield22(ref StackFrame frame) => 
        yield(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield23(ref StackFrame frame) => 
        yield(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield24(ref StackFrame frame) => 
        yield(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield25(ref StackFrame frame) => 
        yield(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield26(ref StackFrame frame) => 
        yield(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield27(ref StackFrame frame) => 
        yield(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield28(ref StackFrame frame) => 
        yield(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield29(ref StackFrame frame) => 
        yield(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield30(ref StackFrame frame) => 
        yield(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static PullState yield31(ref StackFrame frame) => 
        yield(ref frame, 31);
}
