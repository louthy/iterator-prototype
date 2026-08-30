using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static unsafe class G
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState reset<A>(ref StackFrame frame, in ushort ix)
    {
        return frame.globals.ResetAt<A>(ix, out _) 
                   ? Pull.@continue(ref frame) 
                   : Pull.empty(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM<A>(ref StackFrame frame, in ushort ix)
    {
        var g = new Global<A>(in ix);
        return frame.vars.Push(in g) 
                   ? Pull.@continue(ref frame) 
                   : Pull.empty(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull<A>(ref StackFrame frame, in ushort ix)
    {
        ref var r = ref frame.globals.At<A>(ix);
        return frame.vars.Push(in r) 
                   ? Pull.@continue(ref frame) 
                   : Pull.empty(ref frame);
    }

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

    
    public static delegate*<ref StackFrame, PullState> pullM<A>(in ushort index) =>
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
    static PullState pull0<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull1<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull2<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull3<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull4<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull5<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pull6<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull7<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull8<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 8);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull9<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 9);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull10<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 10);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull11<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull12<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull13<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull14<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull15<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull16<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull17<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull18<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull19<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull20<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull21<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull22<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull23<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull24<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull25<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull26<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull27<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull28<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull29<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull30<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pull31<A>(ref StackFrame frame) => 
        pull<A>(ref frame, 31);
    
    
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM0<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 0);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pullM1<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 1);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pullM2<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM3<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 3);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM4<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 4);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pullM5<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 5);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)] 
    static PullState pullM6<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 6);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM7<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 7);
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM8<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 8);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM9<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 9);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM10<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 10);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM11<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 11);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM12<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 12);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM13<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 13);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM14<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 14);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM15<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 15);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM16<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 16);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM17<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 17);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM18<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 18);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM19<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 19);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM20<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 20);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM21<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 21);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM22<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 22);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM23<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 23);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM24<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 24);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM25<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 25);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM26<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 26);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM27<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 27);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM28<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 28);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM29<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 29);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM30<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 30);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static PullState pullM31<A>(ref StackFrame frame) => 
        pullM<A>(ref frame, 31);
    
    
    
    
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
