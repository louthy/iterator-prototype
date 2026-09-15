using System.Runtime.CompilerServices;

using IteratorPrototype.Memory.Internal;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
sealed class LineIndexPool<LineType, A> : LineIndex<A>
    where LineType : Line<LineType, A>, Tr.Constructor<LineType>
{
    readonly int bucketCount;
    readonly LineIndex<LineType, A>[] index;

    public LineIndexPool()
    {
        bucketCount = Math.Min(256, Environment.ProcessorCount);
        index = new LineIndex<LineType, A>[bucketCount];
        for (var pid = 0; pid < bucketCount; pid++)
        {
            index[pid] = new LineIndex<LineType, A>(pid);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Alloc(out LineRef<A> result) =>
        index[Environment.CurrentManagedThreadId % bucketCount].Alloc(out result);

    [MethodImpl(Optimisations.InliningOnly)]
    public override LineRef<A> AlwaysAlloc() =>
        index[Environment.CurrentManagedThreadId % bucketCount].AlwaysAlloc();

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Free(LineRef<A> line) =>
        index[line.LineId.ProcessorIndex].Free(line);
}
