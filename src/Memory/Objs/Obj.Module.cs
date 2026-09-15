using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Memory;

/// <summary>
/// Manages `Environment.ProcessorCount` pools of boxes.
/// </summary>
/// <remarks>
/// Each processor has a dedicated pool of boxes to stop contention. When `Alloc` is called, the box is
/// allocated from the pool for the current processor. When `Free` is called, the box is returned to the
/// pool for the current processor.
/// </remarks>
static class Obj
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Obj<A> alloc<A>()
        where A : class, new() =>
        Objs<A>.Alloc();
}
