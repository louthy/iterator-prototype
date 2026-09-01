#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly unsafe struct Ops
{
    [SkipLocalsInit]
    [StructLayout(LayoutKind.Explicit)]
    readonly struct Op
    {
        [FieldOffset(0)]
        public readonly nint Fun;

        [MethodImpl(Optimisations.Default)]
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
    
    [MethodImpl(Optimisations.Default)]
    public bool Add(in delegate*<ref StackFrame, int> f)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), count);
        entry = new Op((nint)f);
        count++;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Prepend(in delegate*<ref StackFrame, int> f)
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

    [MethodImpl(Optimisations.Max)]
    public bool Run<A>(ref StackFrame frame, out A head)
    {
        // If there are no tops, then this is an empty stack, i.e. empty iterator
        if (frame.IsVoid)
        {
            head = default!;
            return false;
        }

        // Cache a reference to the tops
        ref var tops = ref frame.tops;

        // Set initial state  
        var     pc      = tops.PC;
        ref var ptr     = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), pc);
        var     count   = frame.ops.Count - pc;
        ref var current = ref frame.tops.CurrentRef;
 
        while(true)
        {
            if (count == 0)
            {
                // This is where we end up if we haven't been composed with `Iter.pure`. 
                // So, this is an implicit `Iter.pure`.  It yields what's on the stack
                // and resets the state of the co-routine so it can run again until it
                // stops yielding values.
                PureResetToContinuationPoint(ref frame, out head);
                return true;
            }
        
            // Read the current instruction
            //ref var ptr = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), pc);
            var op = (delegate*<ref StackFrame, int>)ptr.Fun;

            // Move the program-counter *before* executing the instruction, this allows
            // tests like frame.IsReturn to work properly.
            //tops.IncrementPC();
            current += 1;

            // Run the instruction
            var result = op(ref frame);

            // Next instruction
            ptr = ref Unsafe.Add(ref ptr, 1);
            count--;
            
            switch (result)
            {
                // Void
                case 0:
                    if(!VoidResetToContinuationPoint(ref frame))
                    {
                        head = default!;
                        return false;
                    }
                    else
                    {
                        pc = tops.PC;
                        ptr = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), pc);
                        count = frame.ops.Count - pc;
                        continue;
                    }

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

        [MethodImpl(Optimisations.InliningOnly)]
        static bool VoidResetToContinuationPoint(ref StackFrame frame)
        {
            // Remove the current scope.
            // This is the most basic process of leaving a scope with no value: we must step up one scope level.
            frame.VoidScope();
            
            // Leave if the iterator is now empty
            if (frame.tops.Count == 0)
            {
                return false;
            }
            
            // We now need to skip any singleton scopes (ones that don't yield).  Because these didn't generate
            // the value that caused us to get here in the first place.  We're working backwards to find the scope
            // that generates values (because it might have more to yield).
            while (frame.tops.IsSingleton && frame.VoidScope())
            {
                // Empty
            }
            
            // Leave if the iterator is now empty
            if (frame.tops.Count == 0)
            {
                return false;
            }
            
            // Clear the yield flag.  We do this because anything that yields creates a subroutine. We've just
            // popped the singleton subroutine(s), so this is the flag we need to clear in our generator's scope
            // to say that this generator has no more values to yield.
            frame.tops.DecrementYields();
            
            // If there are scopes remaining, then there are more values to yield...
            return frame.tops.Count > 0;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        static bool PureResetToContinuationPoint(ref StackFrame frame, out A head)
        {
            ref var tops = ref frame.tops;
            ref var vars = ref frame.vars;
            
            // Just go back to the start of the current frame
            if(tops.HasYielded)
            {
                frame.ResetFrame(out head);
                return true;
            }

            if (!vars.Pop(out head))
            {
                // Something has gone wrong
                throw new InvalidOperationException("PureResetToContinuationPoint: StackFrame.vars.Pop() failed");
            }
            
            // Pop the current frame off the stack and then checks the new
            // top frame to see if it's a singleton frame.  If it is, then
            // we can keep popping until either we have an empty iterator
            // or we have a yielding frame.
            while (frame.VoidScope() && !tops.HasYielded)
            {
                // Empty
            }
            
            // At this point we're either at the 0-th frame or a yielding frame
            if(tops.HasYielded) tops.DecrementYields();
            
            return true;
        }
    }
}
