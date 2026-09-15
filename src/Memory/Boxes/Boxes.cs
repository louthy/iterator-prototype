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
/// <typeparam name="A">Box value type</typeparam>
static class Boxes<A>
    where A : struct
{
    const int MaxSupportedProcessorCount = 256;
    static readonly BoxPool<A>[] boxes;

    [MethodImpl(Optimisations.InliningOnly)]
    static Boxes()
    {
        var count = MaxSupportedProcessorCount;
        boxes = new BoxPool<A>[count];
        for(var i = 0; i < count; i++)
        {
            boxes[i] = new BoxPool<A>();
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Box<A> Alloc(in A value)
    {
        var pid = Thread.GetCurrentProcessorId();
        return boxes[pid].Alloc(in value);
    }
}
