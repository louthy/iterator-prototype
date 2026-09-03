using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Iterator3.Internal.Collections;

/// <summary>
/// Manages `Environment.ProcessorCount` pools of boxes.
/// </summary>
/// <remarks>
/// Each processor has a dedicated pool of boxes to stop contention. When `Alloc` is called, the box is
/// allocated from the pool for the current processor. When `Free` is called, the box is returned to the
/// pool for the current processor.
/// </remarks>
static class Boxes
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Box<A> alloc<A>(in A value)
        where A : struct =>
        Boxes<A>.Alloc(in value);
}
