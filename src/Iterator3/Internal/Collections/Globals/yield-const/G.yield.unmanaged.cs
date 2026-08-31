using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe partial class GUnmanaged<A>
    where A : unmanaged
{
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst(ref StackFrame frame, in ushort ix)
    {
        // Read the constant value to yield
        ref var r = ref frame.globals.AtUnmanaged<A>(ix);

        // Flag that this co-routine has yielded something
        frame.tops.IncrementYields();

        // Start a new co-routine for the value
        return frame.StartScope() &&

               // Push it onto the 'vars' stack in the new co-routine
               frame.vars.PushUnmanaged(r)
                   ? PullState.Continue
                   : PullState.Void;
    }
    
    public static delegate*<ref StackFrame, PullState> yieldConst(in ushort index) =>
        index switch
        {
            0  => &yieldConst0,
            1  => &yieldConst1,
            2  => &yieldConst2,
            3  => &yieldConst3,
            4  => &yieldConst4,
            5  => &yieldConst5,
            6  => &yieldConst6,
            7  => &yieldConst7,
            8  => &yieldConst8,
            9  => &yieldConst9,
            10 => &yieldConst10,
            11 => &yieldConst11,
            12 => &yieldConst12,
            13 => &yieldConst13,
            14 => &yieldConst14,
            15 => &yieldConst15,
            16 => &yieldConst16,
            17 => &yieldConst17,
            18 => &yieldConst18,
            19 => &yieldConst19,
            20 => &yieldConst20,
            21 => &yieldConst21,
            22 => &yieldConst22,
            23 => &yieldConst23,
            24 => &yieldConst24,
            25 => &yieldConst25,
            26 => &yieldConst26,
            27 => &yieldConst27,
            28 => &yieldConst28,
            29 => &yieldConst29,
            30 => &yieldConst30,
            31 => &yieldConst31,
            _  => throw new ArgumentOutOfRangeException(nameof(index))
        };       
       
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst0(ref StackFrame frame) => 
        yieldConst(ref frame, 0);
    
    [MethodImpl(Optimisations.Default)] 
    static PullState yieldConst1(ref StackFrame frame) => 
        yieldConst(ref frame, 1);
    
    [MethodImpl(Optimisations.Default)] 
    static PullState yieldConst2(ref StackFrame frame) => 
        yieldConst(ref frame, 2);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst3(ref StackFrame frame) => 
        yieldConst(ref frame, 3);

    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst4(ref StackFrame frame) => 
        yieldConst(ref frame, 4);
    
    [MethodImpl(Optimisations.Default)] 
    static PullState yieldConst5(ref StackFrame frame) => 
        yieldConst(ref frame, 5);
    
    [MethodImpl(Optimisations.Default)] 
    static PullState yieldConst6(ref StackFrame frame) => 
        yieldConst(ref frame, 6);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst7(ref StackFrame frame) => 
        yieldConst(ref frame, 7);
    
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst8(ref StackFrame frame) => 
        yieldConst(ref frame, 8);
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst9(ref StackFrame frame) => 
        yieldConst(ref frame, 9);
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst10(ref StackFrame frame) => 
        yieldConst(ref frame, 10);
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst11(ref StackFrame frame) => 
        yieldConst(ref frame, 11);

    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst12(ref StackFrame frame) => 
        yieldConst(ref frame, 12);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst13(ref StackFrame frame) => 
        yieldConst(ref frame, 13);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst14(ref StackFrame frame) => 
        yieldConst(ref frame, 14);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst15(ref StackFrame frame) => 
        yieldConst(ref frame, 15);

    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst16(ref StackFrame frame) => 
        yieldConst(ref frame, 16);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst17(ref StackFrame frame) => 
        yieldConst(ref frame, 17);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst18(ref StackFrame frame) => 
        yieldConst(ref frame, 18);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst19(ref StackFrame frame) => 
        yieldConst(ref frame, 19);

    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst20(ref StackFrame frame) => 
        yieldConst(ref frame, 20);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst21(ref StackFrame frame) => 
        yieldConst(ref frame, 21);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst22(ref StackFrame frame) => 
        yieldConst(ref frame, 22);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst23(ref StackFrame frame) => 
        yieldConst(ref frame, 23);

    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst24(ref StackFrame frame) => 
        yieldConst(ref frame, 24);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst25(ref StackFrame frame) => 
        yieldConst(ref frame, 25);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst26(ref StackFrame frame) => 
        yieldConst(ref frame, 26);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst27(ref StackFrame frame) => 
        yieldConst(ref frame, 27);

    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst28(ref StackFrame frame) => 
        yieldConst(ref frame, 28);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst29(ref StackFrame frame) => 
        yieldConst(ref frame, 29);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst30(ref StackFrame frame) => 
        yieldConst(ref frame, 30);
    
    [MethodImpl(Optimisations.Default)]
    static PullState yieldConst31(ref StackFrame frame) => 
        yieldConst(ref frame, 31);
}
