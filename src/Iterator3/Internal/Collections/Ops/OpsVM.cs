#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
static class OpsVM
{
    [MethodImpl(Optimisations.Max)]
    public static bool Run<A>(in StackFrame frame, out A head)
    {
        // If there are no tops, then this is an empty stack, i.e. empty iterator
        if (frame.IsVoid)
        {
            head = default!;
            return false;
        }

        // Cache a reference to the tops
        ref readonly var tops = ref frame.tops;

        // Set initial state  
        var count = frame.OpsRemaining;

        //Log.msg("run entry", ref frame);
        
        while(count != 0)
        {
            // Read the current instruction
            var op = frame.CurrentOp;

            // Move the program-counter *before* executing the instruction, this allows
            // tests like frame.IsReturn to work properly.
            frame.NextOp();

            // Run the instruction
            var result = op.Invoke(in frame);
            count--;
            
            switch (result)
            {
                // Void
                case 0:
                    if (VoidResetToContinuationPoint(in frame))
                    {
                        count = frame.OpsRemaining;
                        continue;
                    }
                    else
                    {
                        head = default!;
                        return false;
                    }

                // Continue 
                case 1: 
                    continue;
                
                // Pure 
                case 2:
                    PureResetToContinuationPoint(in frame, out head);
                    return true;
                
                default:
                    throw new InvalidOperationException();
            }
        }        
        
        /*// If there are no tops, then this is an empty stack, i.e. empty iterator
        if (frame.IsVoid)
        {
            head = default!;
            return false;
        }

        //Log.msg("run entry", in frame);

        for (var count = frame.OpsRemaining; count != 0; count--)
        {
            // Read the current instruction
            var op = frame.CurrentOp;

            // Move the program-counter *before* executing the instruction, this allows
            // tests like frame.IsReturn to work properly.
            frame.NextOp();

            // Run the instruction
            var result = op.Invoke(in frame);

            switch (result)
            {
                // Void
                case 0:
                    if (!VoidResetToContinuationPoint(in frame))
                    {
                        head = default!;
                        return false;
                    }
                    else
                    {
                        count = frame.OpsRemaining;
                        continue;
                    }

                // Continue 
                case 1:
                    continue;

                // Pure 
                case 2:
                    PureResetToContinuationPoint(in frame, out head);
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }*/

        // This is where we end up if we haven't been composed with `Iter.pure`. 
        // So, this is an implicit `Iter.pure`.  It yields what's on the stack
        // and resets the state of the co-routine so it can run again until it
        // stops yielding values.
        PureResetToContinuationPoint(in frame, out head);
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    static bool VoidResetToContinuationPoint(in StackFrame frame)
    {
        //Log.function("start-void", in frame);
            
        // Remove the current scope.
        // This is the most basic process of leaving a scope with no value: we must step up one scope level.
        frame.VoidScope();
            
        //Log.function("popped the voided scope", in frame);
            
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
            //Log.stack(in frame);
            // Empty
        }
            
        // Leave if the iterator is now empty
        if (frame.tops.Count == 0)
        {
            //Log.terminator("end-void (empty)", in frame);
            return false;
        }
            
        // Clear the yield flag.  We do this because anything that yields creates a subroutine. We've just
        // popped the singleton subroutine(s), so this is the flag we need to clear in our generator's scope
        // to say that this generator has no more values to yield.
        if (frame.tops.HasYielded)
        {
            frame.tops.DecrementYields();
        }

        //Log.warn("end-void (more to go)", in frame);
            
        // If there are scopes remaining, then there are more values to yield...
        return frame.tops.Count > 0;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    static void PureResetToContinuationPoint<A>(in StackFrame frame, out A head)
    {
        ref readonly var tops = ref frame.tops;
        ref readonly var vars = ref frame.vars;
            
        //Log.function("start-pure", in frame);
            
        // Just go back to the start of the current frame if we have already yielded a value.
        // We get here if a value has already been returned to the caller, and then there were
        // some later operations.
        if(tops.HasYielded)
        {
            frame.ResetFrame(out head);
            //Log.function("frame-reset", in frame);
            return;
        }

        if (!vars.Pop(out head, false))
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
        if(tops.HasYielded)
        {
            // Unmark this frame as yielding
            tops.DecrementYields();
        }

        //Log.terminator("end-pure", in frame);
    }
}
