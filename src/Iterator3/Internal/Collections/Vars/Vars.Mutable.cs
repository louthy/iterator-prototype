#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS0649 // Field is never assigned to
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorPrototype.Iterator3.Internal.Memory;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
struct VarsMutable
{
    const int Capacity = 31;
    
    public ObjStack objs;
    public ByteStack values;
    
    // These flags remember if a value is a co-routine argument, or not, and if so, stops it
    // being popped off the stack (when the `force` flag is `false). That means subsequent 
    // loops through an 'iterable' can use the full stack of co-routine arguments.
    public byte flag0, flag1, flag2, flag3, flag4, flag5, flag6, flag7;
    public byte flag8, flag9, flagA, flagB, flagC, flagD, flagE, flagF;
    public byte flag10, flag11, flag12, flag13, flag14, flag15, flag16, flag17;
    public byte flag18, flag19, flag1A, flag1B, flag1C, flag1D, flag1E /*, flag1F -- we're using this byte for `top` */;
    public byte top;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldManaged<A>(in StackFrame frame)
        where A : class
    {
        //Log.coroutine($"start-yield [managed : {Ty<A>.Pretty}, sizeof: {Unsafe.SizeOf<A>()}]", in frame);
        
        // Set the flag for stating this is a coroutine argument
        ref var vars = ref frame.vars.Ref;
        vars.FlagBytes[^1] = 1;
        
        // Save the current top values for the stack
        ref var topRef   = ref Unsafe.AsRef(in vars.objs.Count);
        var     topValue = topRef;
        
        // Virtually pop off the top value (which is the result of the current co-routine)
        topRef--;
        
        // Start the yield co-routine
        frame.StartYieldScope();
        
        // Virtually re-push the top value (it will become the argument to the co-routine).
        topRef = topValue;

        //Log.coroutine("end-yield", in frame);
        
        return PullState.Continue;        
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldUnmanaged<A>(in StackFrame frame)
        where A : unmanaged
    {
        //Log.coroutine($"start-yield [unmanaged : {Ty<A>.Pretty}, sizeof: {Unsafe.SizeOf<A>()}]", in frame);
        
        // Set the flag for stating this is a coroutine argument
        ref var vars = ref frame.vars.Ref;
        vars.FlagBytes[^1] = 1;
        
        // Save the current top values for the stack
        ref var topRef   = ref Unsafe.AsRef(in vars.values.Count);
        var     topValue = topRef;
        var     sizeOfA  = Unsafe.SizeOf<A>();
        
        // Virtually pop off the top value (which is the result of the current co-routine)
        topRef -= sizeOfA;
        
        // Start the yield co-routine
        frame.StartYieldScope();
        
        // Virtually re-push the top value (it will become the argument to the co-routine).
        topRef = topValue;

        //Log.coroutine("end-yield", in frame);
        
        return PullState.Continue;        
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldStruct<A>(in StackFrame frame)
        where A : struct =>
        yieldManaged<Box<A>>(in frame);
}
