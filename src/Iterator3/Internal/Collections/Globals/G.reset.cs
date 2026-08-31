using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class G
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset<A>(ref StackFrame frame, in ushort ix)
    {
        return frame.globals.ResetAt<A>(ix, out _) 
                   ? Pull.@continue(ref frame) 
                   : Pull.empty(ref frame);
    }
    
    public static delegate*<ref StackFrame, PullState> reset<A>(in ushort index) =>
        index switch
        {
            0  => &reset0<A>,
            1  => &reset1<A>,
            2  => &reset2<A>,
            3  => &reset3<A>,
            4  => &reset4<A>,
            5  => &reset5<A>,
            6  => &reset6<A>,
            7  => &reset7<A>,
            8  => &reset8<A>,
            9  => &reset9<A>,
            10 => &reset10<A>,
            11 => &reset11<A>,
            12 => &reset12<A>,
            13 => &reset13<A>,
            14 => &reset14<A>,
            15 => &reset15<A>,
            16 => &reset16<A>,
            17 => &reset17<A>,
            18 => &reset18<A>,
            19 => &reset19<A>,
            20 => &reset20<A>,
            21 => &reset21<A>,
            22 => &reset22<A>,
            23 => &reset23<A>,
            24 => &reset24<A>,
            25 => &reset25<A>,
            26 => &reset26<A>,
            27 => &reset27<A>,
            28 => &reset28<A>,
            29 => &reset29<A>,
            30 => &reset30<A>,
            31 => &reset31<A>,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset0<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState reset1<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState reset2<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset3<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset4<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState reset5<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState reset6<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset7<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset8<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 8);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset9<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 9);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset10<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 10);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset11<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset12<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset13<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset14<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset15<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset16<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset17<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset18<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset19<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset20<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset21<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset22<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset23<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset24<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset25<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset26<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset27<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset28<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset29<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset30<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset31<A>(ref StackFrame frame) => 
        reset<A>(ref frame, 31);
}
