#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly unsafe struct Ops
{
    public const int Capacity = 32;
    public readonly int Count;
    public readonly nint Ptr00;
    public readonly nint Ptr01;
    public readonly nint Ptr02;
    public readonly nint Ptr03;
    public readonly nint Ptr04;
    public readonly nint Ptr05;
    public readonly nint Ptr06;
    public readonly nint Ptr07;
    public readonly nint Ptr08;
    public readonly nint Ptr09;
    public readonly nint Ptr0A;
    public readonly nint Ptr0B;
    public readonly nint Ptr0C;
    public readonly nint Ptr0D;
    public readonly nint Ptr0E;
    public readonly nint Ptr0F;
    public readonly nint Ptr10;
    public readonly nint Ptr11;
    public readonly nint Ptr12;
    public readonly nint Ptr13;
    public readonly nint Ptr14;
    public readonly nint Ptr15;
    public readonly nint Ptr16;
    public readonly nint Ptr17;
    public readonly nint Ptr18;
    public readonly nint Ptr19;
    public readonly nint Ptr1A;
    public readonly nint Ptr1B;
    public readonly nint Ptr1C;
    public readonly nint Ptr1D;
    public readonly nint Ptr1E;
    public readonly nint Ptr1F;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Add(in delegate*<ref StackFrame, PullState> value)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Ptr00), count);
        entry = (nint)value;
        count++;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Prepend(in delegate*<ref StackFrame, PullState> value)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);

        ref var start = ref Unsafe.AsRef(in Ptr00);
        ref var next = ref Unsafe.Add(ref start, 1);
        
        Unsafe.CopyBlock(
            ref Unsafe.As<nint, byte>(ref next), 
            ref Unsafe.As<nint, byte>(ref start), 
            (uint)(Unsafe.SizeOf<nint>() * count));
        
        start = (nint)value;
        count++;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run<A>(ref StackFrame frame, out A head)
    {
        // If there are no tops, then this is an empty stack, i.e. empty iterator
        if (frame.IsVoid)
        {
            head = default!;
            return false;
        }
        
        // Reference the top program-counter
        ref var pc = ref frame.tops.CurrentPC;

        // If we're at the end of the ops, then jump up the stack to a previous 
        // program-counter, so other values can be yielded.
        while (pc >= Count)
        {
            frame.Pop();
            if (frame.IsVoid)
            {
                head = default!;
                return false;
            }
        }
        
        while(true)
        {
            if (pc == Count)
            {
                // Pop the result of the co-routine. We will return that to the caller.
                frame.vars.Pop(out head);
                
                // Return to the start of this co-routine
                // Note, we don't pop the rest of the state because we only want to return to 
                // parent co-routines once all the values have been yielded.  This can only
                // be known by the yielding functions (they should return `Void` when done)
                frame.tops.PopPC();
                
                return true;
            }
            
            // Read the current instruction
            ref var ptr = ref Unsafe.Add(ref Unsafe.AsRef(in Ptr00), pc);
            var     op  = (delegate*<ref StackFrame, PullState>)ptr;

            // Move the program-counter *before* executing the instruction, this allows
            // tests like frame.IsReturn to work properly.
            pc++;

            // Run the instruction
            var r = op(ref frame);
            
            switch (r.Value)
            {
                // Void
                case 0:
                    
                    // This is a return from a co-routine, so we need to pop everything.
                    frame.tops.Pop();
                    head = default!;
                    return false;
                
                // Continue 
                case 1: 
                    continue;
                
                // Pure 
                case 2:
                    frame.vars.Pop(out head);
                    return true;
                
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
