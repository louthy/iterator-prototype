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
    internal static bool VoidResetToContinuationPoint(in StackFrame frame)
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
            frame.tops.ClearYields();
        }

        //Log.warn("end-void (more to go)", in frame);
            
        // If there are scopes remaining, then there are more values to yield...
        return frame.tops.Count > 0;
    }
}
