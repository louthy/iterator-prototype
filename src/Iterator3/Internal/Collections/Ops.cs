#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly unsafe struct Ops
{
    [SkipLocalsInit]
    readonly struct Op
    {
        public readonly nint Fun;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Op(nint fun)
        {
            Fun = fun;
        }
    }
    
    public const int Capacity = 32;
    public readonly int Count;
    readonly Op Fun00;
    readonly Op Fun01;
    readonly Op Fun02;
    readonly Op Fun03;
    readonly Op Fun04;
    readonly Op Fun05;
    readonly Op Fun06;
    readonly Op Fun07;
    readonly Op Fun08;
    readonly Op Fun09;
    readonly Op Fun0A;
    readonly Op Fun0B;
    readonly Op Fun0C;
    readonly Op Fun0D;
    readonly Op Fun0E;
    readonly Op Fun0F;
    readonly Op Fun10;
    readonly Op Fun11;
    readonly Op Fun12;
    readonly Op Fun13;
    readonly Op Fun14;
    readonly Op Fun15;
    readonly Op Fun16;
    readonly Op Fun17;
    readonly Op Fun18;
    readonly Op Fun19;
    readonly Op Fun1A;
    readonly Op Fun1B;
    readonly Op Fun1C;
    readonly Op Fun1D;
    readonly Op Fun1E;
    readonly Op Fun1F;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Add(in delegate*<ref StackFrame, PullState> f)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), count);
        entry = new Op((nint)f);
        count++;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Prepend(in delegate*<ref StackFrame, PullState> f)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);

        ref var start = ref Unsafe.AsRef(in Fun00);
        ref var next = ref Unsafe.Add(ref start, 1);
        
        Unsafe.CopyBlock(
            ref Unsafe.As<Op, byte>(ref next), 
            ref Unsafe.As<Op, byte>(ref start), 
            (uint)(Unsafe.SizeOf<Op>() * count));
        
        start = new Op((nint)f);
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
        
        // Reset the yield flag for the current frame.   
        frame.tops.CurrentYield = 0;
        
        while(true)
        {
            if (frame.IsReturn)
            {
                // This is where we end up if we haven't been composed with `Iter.pure`. 
                // So, this is an implicit `Iter.pure`.  It yields what's on the stack
                // and resets the state of the co-routine so it can run again until it
                // stops yielding values.
                PureResetToContinuationPoint(ref frame, out head);
                return true;
            }
            
            // Read the current instruction
            ref var ptr = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), pc);
            var     op  = (delegate*<ref StackFrame, PullState>)ptr.Fun;

            // Move the program-counter *before* executing the instruction, this allows
            // tests like frame.IsReturn to work properly.
            pc++;

            // Run the instruction
            var r = op(ref frame);
            
            switch (r.Value)
            {
                // Void
                case 0:
                    if(!VoidResetToContinuationPoint(ref frame))
                    {
                        head = default!;
                        return false;
                    }
                    continue;

                // Continue 
                case 1: 
                    continue;
                
                // Pure 
                case 2:
                    return PureResetToContinuationPoint(ref frame, out head);
                
                default:
                    throw new InvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        static bool VoidResetToContinuationPoint(ref StackFrame frame)
        {
            ref var hasYielded = ref frame.tops.CurrentYield;

            // Remove the current scope
            frame.VoidScope();
            
            // Clear the yield flag.  We do this because anything that yields
            // creates a subroutine. We've just popped the subroutine scope, so
            // this is the flag we need to clear, because nothing was yielded.
            hasYielded = 0;
 
            // Now we can search the call stack, looking for a non-singleton frame.
            // We keep doing this until we find a non-singleton that we can loop; or
            // end up with an empty iterator, which means we're done.
            while (hasYielded == 0 && frame.VoidScope())
            {
                /* empty on purpose */
            }
            return frame.tops.Count > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        static bool PureResetToContinuationPoint(ref StackFrame frame, out A head)
        {
            ref var hasYielded = ref frame.tops.CurrentYield;
 
            // Just go back to the start of the current frame
            if(hasYielded > 0)
            {
                frame.ResetFrame(out head);
                return true;
            }

            if (!frame.vars.Pop(out head))
            {
                // Something has gone wrong
                throw new InvalidOperationException("PureResetToContinuationPoint: StackFrame.vars.Pop() failed");
            }
            
            // Pop the current frame off the stack and then checks the new
            // top frame to see if it's a singleton frame.  If it is, then
            // we can keep popping until either we have an empty iterator
            // or we have a yielding frame.
            while (frame.VoidScope() && hasYielded == 0)
            {
                /* empty on purpose */
            }
            return true;
        }
    }
}
