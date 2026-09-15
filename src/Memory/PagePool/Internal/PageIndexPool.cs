using System.Runtime.CompilerServices;

using IteratorPrototype.Memory.Internal;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
sealed class PageIndexPool<PageType, A> : PageIndex<A>
    where PageType : Page<PageType, A>, Tr.Constructor<PageType>
{
    readonly int bucketCount;
    readonly PageIndex<PageType, A>[] index;

    public PageIndexPool()
    {
        bucketCount = Math.Min(256, Environment.ProcessorCount);
        index = new PageIndex<PageType, A>[bucketCount];
        for (var pid = 0; pid < bucketCount; pid++)
        {
            index[pid] = new PageIndex<PageType, A>(pid);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Alloc(out PageRef<A> result) =>
        index[Environment.CurrentManagedThreadId % bucketCount].Alloc(out result);

    [MethodImpl(Optimisations.InliningOnly)]
    public override PageRef<A> AlwaysAlloc() =>
        index[Environment.CurrentManagedThreadId % bucketCount].AlwaysAlloc();

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Free(PageRef<A> page) =>
        index[page.PageId.ProcessorIndex].Free(page);
}
