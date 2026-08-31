using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GUnmanaged<A>
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset(ref StackFrame frame, in ushort ix) =>
        frame.globals.ResetAtUnmanaged<A>(ix)
            ? PullState.Continue
            : PullState.Void;
    
    public static delegate*<ref StackFrame, int> reset(in ushort index) =>
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset0(ref StackFrame frame) => 
        reset(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int reset1(ref StackFrame frame) => 
        reset(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int reset2(ref StackFrame frame) => 
        reset(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset3(ref StackFrame frame) => 
        reset(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset4(ref StackFrame frame) => 
        reset(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int reset5(ref StackFrame frame) => 
        reset(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int reset6(ref StackFrame frame) => 
        reset(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset7(ref StackFrame frame) => 
        reset(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset8(ref StackFrame frame) => 
        reset(ref frame, 8);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset9(ref StackFrame frame) => 
        reset(ref frame, 9);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset10(ref StackFrame frame) => 
        reset(ref frame, 10);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset11(ref StackFrame frame) => 
        reset(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset12(ref StackFrame frame) => 
        reset(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset13(ref StackFrame frame) => 
        reset(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset14(ref StackFrame frame) => 
        reset(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset15(ref StackFrame frame) => 
        reset(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset16(ref StackFrame frame) => 
        reset(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset17(ref StackFrame frame) => 
        reset(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset18(ref StackFrame frame) => 
        reset(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset19(ref StackFrame frame) => 
        reset(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset20(ref StackFrame frame) => 
        reset(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset21(ref StackFrame frame) => 
        reset(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset22(ref StackFrame frame) => 
        reset(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset23(ref StackFrame frame) => 
        reset(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset24(ref StackFrame frame) => 
        reset(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset25(ref StackFrame frame) => 
        reset(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset26(ref StackFrame frame) => 
        reset(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset27(ref StackFrame frame) => 
        reset(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset28(ref StackFrame frame) => 
        reset(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset29(ref StackFrame frame) => 
        reset(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset30(ref StackFrame frame) => 
        reset(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int reset31(ref StackFrame frame) => 
        reset(ref frame, 31);
}
