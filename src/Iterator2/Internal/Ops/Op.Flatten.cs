using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal;

class FlattenOp<A>(Iterator2<Iterator2<A>> tta) : Op<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame stack)
    {
        throw new NotImplementedException();
        return true;
        
        // Process
        //
        // We get a new Iterator2<B>
        // The remaining operations in the current OpFrame need to be run for each item in `tb`, before resetting for the
        // next `A` to give to `f` for subsequent `tb` ops.
        //
        //      How:
        //
        //          - We need future calls to `TryGetItem` to start with the state in `tb`
        //          - We need to put a pin in the current PC, so we can run each item in `tb`
    }
}
