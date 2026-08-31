using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class G
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push<A>(ref StackFrame frame, in ushort ix)
    {
        // Get a reference to the global
        ref var r = ref frame.globals.At<A>(ix);
        
        // Pop the value from the stack
        if (frame.vars.Pop<A>(out var x))
        {
            // Set the global to be what was on the top of the stack
            r = x;
            return Pull.@continue(ref frame);
        }
        else
        {
            return Pull.empty(ref frame);
        }
    }

    public static delegate*<ref StackFrame, PullState> push<A>(in ushort index) =>
        index switch
        {
            0  => &push0<A>,
            1  => &push1<A>,
            2  => &push2<A>,
            3  => &push3<A>,
            4  => &push4<A>,
            5  => &push5<A>,
            6  => &push6<A>,
            7  => &push7<A>,
            8  => &push8<A>,
            9  => &push9<A>,
            10 => &push10<A>,
            11 => &push11<A>,
            12 => &push12<A>,
            13 => &push13<A>,
            14 => &push14<A>,
            15 => &push15<A>,
            16 => &push16<A>,
            17 => &push17<A>,
            18 => &push18<A>,
            19 => &push19<A>,
            20 => &push20<A>,
            21 => &push21<A>,
            22 => &push22<A>,
            23 => &push23<A>,
            24 => &push24<A>,
            25 => &push25<A>,
            26 => &push26<A>,
            27 => &push27<A>,
            28 => &push28<A>,
            29 => &push29<A>,
            30 => &push30<A>,
            31 => &push31<A>,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };       
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push0<A>(ref StackFrame frame) => 
        push<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState push1<A>(ref StackFrame frame) => 
        push<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState push2<A>(ref StackFrame frame) => 
        push<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push3<A>(ref StackFrame frame) => 
        push<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push4<A>(ref StackFrame frame) => 
        push<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState push5<A>(ref StackFrame frame) => 
        push<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState push6<A>(ref StackFrame frame) => 
        push<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push7<A>(ref StackFrame frame) => 
        push<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push8<A>(ref StackFrame frame) => 
        push<A>(ref frame, 8);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push9<A>(ref StackFrame frame) => 
        push<A>(ref frame, 9);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push10<A>(ref StackFrame frame) => 
        push<A>(ref frame, 10);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push11<A>(ref StackFrame frame) => 
        push<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push12<A>(ref StackFrame frame) => 
        push<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push13<A>(ref StackFrame frame) => 
        push<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push14<A>(ref StackFrame frame) => 
        push<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push15<A>(ref StackFrame frame) => 
        push<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push16<A>(ref StackFrame frame) => 
        push<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push17<A>(ref StackFrame frame) => 
        push<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push18<A>(ref StackFrame frame) => 
        push<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push19<A>(ref StackFrame frame) => 
        push<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push20<A>(ref StackFrame frame) => 
        push<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push21<A>(ref StackFrame frame) => 
        push<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push22<A>(ref StackFrame frame) => 
        push<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push23<A>(ref StackFrame frame) => 
        push<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push24<A>(ref StackFrame frame) => 
        push<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push25<A>(ref StackFrame frame) => 
        push<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push26<A>(ref StackFrame frame) => 
        push<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push27<A>(ref StackFrame frame) => 
        push<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push28<A>(ref StackFrame frame) => 
        push<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push29<A>(ref StackFrame frame) => 
        push<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push30<A>(ref StackFrame frame) => 
        push<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState push31<A>(ref StackFrame frame) => 
        push<A>(ref frame, 31);
}
