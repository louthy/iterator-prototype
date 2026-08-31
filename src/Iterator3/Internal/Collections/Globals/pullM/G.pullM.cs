using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class G
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM<A>(ref StackFrame frame, in ushort ix)
    {
        var g = new Global<A>(in ix);
        return frame.vars.PushUnmanaged(in g) 
                   ? PullState.Continue 
                   : PullState.Void;
    }
        
    public static delegate*<ref StackFrame, int> pullM<A>(in ushort index) =>
        index switch
        {
            0  => &pullM0<A>,
            1  => &pullM1<A>,
            2  => &pullM2<A>,
            3  => &pullM3<A>,
            4  => &pullM4<A>,
            5  => &pullM5<A>,
            6  => &pullM6<A>,
            7  => &pullM7<A>,
            8  => &pullM8<A>,
            9  => &pullM9<A>,
            10 => &pullM10<A>,
            11 => &pullM11<A>,
            12 => &pullM12<A>,
            13 => &pullM13<A>,
            14 => &pullM14<A>,
            15 => &pullM15<A>,
            16 => &pullM16<A>,
            17 => &pullM17<A>,
            18 => &pullM18<A>,
            19 => &pullM19<A>,
            20 => &pullM20<A>,
            21 => &pullM21<A>,
            22 => &pullM22<A>,
            23 => &pullM23<A>,
            24 => &pullM24<A>,
            25 => &pullM25<A>,
            26 => &pullM26<A>,
            27 => &pullM27<A>,
            28 => &pullM28<A>,
            29 => &pullM29<A>,
            30 => &pullM30<A>,
            31 => &pullM31<A>,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM0<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int pullM1<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int pullM2<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM3<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM4<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int pullM5<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static int pullM6<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM7<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM8<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 8);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM9<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 9);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM10<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 10);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM11<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM12<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM13<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM14<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM15<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM16<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM17<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM18<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM19<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM20<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM21<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM22<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM23<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM24<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM25<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM26<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM27<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM28<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM29<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM30<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int pullM31<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 31);
}
