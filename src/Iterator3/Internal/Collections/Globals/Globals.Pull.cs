using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe class G
{
    public static delegate*<ref StackFrame, PullState> pull<A>(in ushort index) =>
        index switch
        {
            0  => &pull0<A>,
            1  => &pull1<A>,
            2  => &pull2<A>,
            3  => &pull3<A>,
            4  => &pull4<A>,
            5  => &pull5<A>,
            6  => &pull6<A>,
            7  => &pull7<A>,
            8  => &pull8<A>,
            9  => &pull9<A>,
            10 => &pull10<A>,
            11 => &pull11<A>,
            12 => &pull12<A>,
            13 => &pull13<A>,
            14 => &pull14<A>,
            15 => &pull15<A>,
            16 => &pull16<A>,
            17 => &pull17<A>,
            18 => &pull18<A>,
            19 => &pull19<A>,
            20 => &pull20<A>,
            21 => &pull21<A>,
            22 => &pull22<A>,
            23 => &pull23<A>,
            24 => &pull24<A>,
            25 => &pull25<A>,
            26 => &pull26<A>,
            27 => &pull27<A>,
            28 => &pull28<A>,
            29 => &pull29<A>,
            30 => &pull30<A>,
            31 => &pull31<A>,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull<A>(ref StackFrame frame, in ushort ix)
    {
        var g = new Global<A>(in ix);
        return frame.vars.Push(in g)
                    ? Pull.@continue(ref frame)
                    : Pull.empty(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull0<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    public static PullState pull1<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    public static PullState pull2<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull3<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull4<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    public static PullState pull5<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    public static PullState pull6<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull7<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull8<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 8);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull9<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 9);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull10<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 10);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull11<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull12<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull13<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull14<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull15<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull16<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull17<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull18<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull19<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull20<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull21<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull22<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull23<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull24<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull25<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull26<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull27<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull28<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull29<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull30<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PullState pull31<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 31);
}
