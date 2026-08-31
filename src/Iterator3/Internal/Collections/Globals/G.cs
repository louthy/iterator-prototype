using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class G
{
    static PullState yield<A>(ref StackFrame frame, in ushort ix)
    {
        if (frame.vars.Pop<A>(out var value))
        {
            // Fill the yield variable with the output of whatever ran before us
            ref var r = ref frame.globals.At<A>(ix);
            r = value;

            // Flag that this co-routine has yielded something
            frame.tops.CurrentYield++;

            // Start a new co-routine for the value
            frame.StartScope();

            // Push it onto the 'vars' stack in the new co-routine
            frame.vars.Push(r);

            return Pull.@continue(ref frame);
        }
        else
        {
            return Pull.empty(ref frame);
        }
    }
    
    public static delegate*<ref StackFrame, PullState> yield<A>(in ushort index) =>
        index switch
        {
            0  => &yield0<A>,
            1  => &yield1<A>,
            2  => &yield2<A>,
            3  => &yield3<A>,
            4  => &yield4<A>,
            5  => &yield5<A>,
            6  => &yield6<A>,
            7  => &yield7<A>,
            8  => &yield8<A>,
            9  => &yield9<A>,
            10 => &yield10<A>,
            11 => &yield11<A>,
            12 => &yield12<A>,
            13 => &yield13<A>,
            14 => &yield14<A>,
            15 => &yield15<A>,
            16 => &yield16<A>,
            17 => &yield17<A>,
            18 => &yield18<A>,
            19 => &yield19<A>,
            20 => &yield20<A>,
            21 => &yield21<A>,
            22 => &yield22<A>,
            23 => &yield23<A>,
            24 => &yield24<A>,
            25 => &yield25<A>,
            26 => &yield26<A>,
            27 => &yield27<A>,
            28 => &yield28<A>,
            29 => &yield29<A>,
            30 => &yield30<A>,
            31 => &yield31<A>,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };       
       
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield0<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState yield1<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState yield2<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield3<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield4<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState yield5<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState yield6<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield7<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield8<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 8);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield9<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 9);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield10<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 10);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield11<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield12<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield13<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield14<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield15<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield16<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield17<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield18<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield19<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield20<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield21<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield22<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield23<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield24<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield25<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield26<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield27<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield28<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield29<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield30<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState yield31<A>(ref StackFrame frame) => 
        yield<A>(ref frame, 31);
}
