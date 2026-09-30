#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
static class OpsVMUnmanaged<A>
    where A : unmanaged
{
    static OpsVMUnmanaged()
    {
        unsafe
        {
            OpsVM<A>.run = &Run;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool Run(in StackFrame frame, out A head)
    {
        // If there are no tops, then this is an empty stack, i.e. empty iterator
        if (frame.IsVoid)
        {
            head = default;
            return false;
        }

        // If it's not runnable, then that means trying to run it will overflow our stacks
        if (!frame.IsRunnable)
        {
            // TODO: Fall back to a slower backup implementation
            throw new NotImplementedException("The iterator expression is too large to run in the VM");
        }

        start:
        
        switch (OpsVM.RunOps(in frame))
        {
            // Void
            case 0:
                if (OpsVM.VoidResetToContinuationPoint(in frame))
                {
                    goto start;
                }
                else
                {
                    head = default;
                    return false;
                }

            // Continue 
            case 1:
                goto start;

            // Pure 
            case 2:
                goto pure;
            
            default:
                throw new InvalidOperationException();
        }
        
        pure:

        PureResetToContinuationPoint(in frame, out head);
        return true;        
    }

    [MethodImpl(Optimisations.InliningOnly)]
    static void PureResetToContinuationPoint(in StackFrame frame, out A head)
    {
        ref var          tops = ref frame.tops;
        ref readonly var vars = ref frame.vars;
            
        //Log.function("start-pure", in frame);
            
        // Just go back to the start of the current frame if we have already yielded a value.
        // We get here if a value has already been returned to the caller, and then there were
        // some later operations.
        if(tops.HasYielded)
        {
            frame.ResetFrameUnmanaged(out head);
            //Log.function("frame-reset", in frame);
            return;
        }

        vars.PopUnmanaged(out head, false);

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
