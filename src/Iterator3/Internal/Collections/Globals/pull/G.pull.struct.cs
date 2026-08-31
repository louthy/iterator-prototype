using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GStruct<A>
    where A : struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull(ref StackFrame frame, in ushort ix)
    {
        ref var r = ref frame.globals.AtStruct<A>(ix);
        return frame.vars.PushStruct(in r)
                   ? PullState.Continue
                   : PullState.Void;
    }
    
    public static delegate*<ref StackFrame, PullState> pull(in ushort index) =>
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
       
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull0(ref StackFrame frame) => 
        pull(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull1(ref StackFrame frame) => 
        pull(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull2(ref StackFrame frame) => 
        pull(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull3(ref StackFrame frame) => 
        pull(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull4(ref StackFrame frame) => 
        pull(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull5(ref StackFrame frame) => 
        pull(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull6(ref StackFrame frame) => 
        pull(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull7(ref StackFrame frame) => 
        pull(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull8(ref StackFrame frame) => 
        pull(ref frame, 8);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull9(ref StackFrame frame) => 
        pull(ref frame, 9);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull10(ref StackFrame frame) => 
        pull(ref frame, 10);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull11(ref StackFrame frame) => 
        pull(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull12(ref StackFrame frame) => 
        pull(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull13(ref StackFrame frame) => 
        pull(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull14(ref StackFrame frame) => 
        pull(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull15(ref StackFrame frame) => 
        pull(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull16(ref StackFrame frame) => 
        pull(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull17(ref StackFrame frame) => 
        pull(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull18(ref StackFrame frame) => 
        pull(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull19(ref StackFrame frame) => 
        pull(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull20(ref StackFrame frame) => 
        pull(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull21(ref StackFrame frame) => 
        pull(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull22(ref StackFrame frame) => 
        pull(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull23(ref StackFrame frame) => 
        pull(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull24(ref StackFrame frame) => 
        pull(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull25(ref StackFrame frame) => 
        pull(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull26(ref StackFrame frame) => 
        pull(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull27(ref StackFrame frame) => 
        pull(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull28(ref StackFrame frame) => 
        pull(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull29(ref StackFrame frame) => 
        pull(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull30(ref StackFrame frame) => 
        pull(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull31(ref StackFrame frame) => 
        pull(ref frame, 31);
}
