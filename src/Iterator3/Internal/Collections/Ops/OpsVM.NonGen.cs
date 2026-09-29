#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
static class OpsVM
{
    const int batchSize = 15;
    
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
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    internal static int RunOps(in StackFrame frame)
    {
        ref var op       = ref frame.ops.Block(frame.PC, out var opCount);
        ref var current  = ref frame.tops.CurrentRef;
        var     sizeOfOp = Unsafe.SizeOf<Op>();

        start:

        var batch = opCount > batchSize 
                        ? batchSize 
                        : opCount;
        
        opCount -= batch;

        switch (batch)
        {
            case 0:
                return PullState.Pure;

            case 1:
                current++;
                return op.Invoke(in frame);

            case 2:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 3:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 4:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 5:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 6:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 7:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 8:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 9:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 10:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 11:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 12:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 13:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 14:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                return op.Invoke(in frame);

            case 15:
                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                op.Invoke(in frame);
                op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);

                current++;
                if (opCount == 0)
                {
                    return op.Invoke(in frame);
                }
                else
                {
                    op.Invoke(in frame);
                    op = ref Unsafe.AddByteOffset(ref op, sizeOfOp);
                    break;
                }
        }

        goto start;
    }
}
