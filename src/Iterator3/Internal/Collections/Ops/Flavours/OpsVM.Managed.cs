#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
static class OpsVMManaged<A>
    where A : class
{
    static OpsVMManaged()
    {
        unsafe
        {
            OpsVM<A>.run = &Run;
        }
    }
    
    [MethodImpl(Optimisations.Max)]
    public static bool Run(in StackFrame frame, out A head)
    {
        // If there are no tops, then this is an empty stack, i.e. empty iterator
        if (frame.IsVoid)
        {
            head = null!;
            return false;
        }

        // Set initial state  
        var count = frame.OpsRemaining;

        // Read the current instruction
        ref var op = ref Unsafe.AsRef(in frame.CurrentOp);

        //Log.msg("run entry", ref frame);
        
        while(count != 0)
        {
            // Move the program-counter *before* executing the instruction, this allows
            // tests like frame.IsReturn to work properly.
            frame.NextOp();

            // Run the instruction
            var result = op.Invoke(in frame);
            
            op = ref Unsafe.AddByteOffset(ref op, 8);
            count--;
            
            switch (result)
            {
                // Void
                case 0:
                    if (OpsVM.VoidResetToContinuationPoint(in frame))
                    {
                        count = frame.OpsRemaining;
                        op = ref Unsafe.AsRef(in frame.CurrentOp);
                        continue;
                    }
                    else
                    {
                        head = null!;
                        return false;
                    }

                // Continue 
                case 1: 
                    continue;
                
                // Pure 
                case 2:
                    goto pure;
                
                default:
                    throw new InvalidOperationException();
            }
        }        
        
        pure:

        // This is where we end up if we haven't been composed with `Iter.pure`. 
        // So, this is an implicit `Iter.pure`.  It yields what's on the stack
        // and resets the state of the co-routine so it can run again until it
        // stops yielding values.
        PureResetToContinuationPoint(in frame, out head);
        return true;

    }
    
    [MethodImpl(Optimisations.Max)]
    static void PureResetToContinuationPoint(in StackFrame frame, out A head)
    {
        ref readonly var tops = ref frame.tops;
        ref readonly var vars = ref frame.vars;
            
        //Log.function("start-pure", in frame);
            
        // Just go back to the start of the current frame if we have already yielded a value.
        // We get here if a value has already been returned to the caller, and then there were
        // some later operations.
        if(tops.HasYielded)
        {
            frame.ResetFrameManaged(out head);
            //Log.function("frame-reset", in frame);
            return;
        }

        if (!vars.PopManaged(out head, false))
        {
            // Something has gone wrong
            throw new InvalidOperationException("PureResetToContinuationPoint: StackFrame.vars.Pop() failed");
        }

        //Log.value($"yielded: {head}", in frame);
            
        // Pop the current frame off the stack and then checks the new
        // top frame to see if it's a singleton frame.  If it is, then
        // we can keep popping until either we have an empty iterator
        // or we have a yielding frame.
        while (frame.VoidScope() && !tops.HasYielded)
        {
            //Log.stack(in frame);
            // Empty
        }
            
        // At this point we're either at the 0-th frame or a yielding frame
        tops.ClearYields();

        //Log.terminator("end-pure", in frame);
    }
}
