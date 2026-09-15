using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
abstract class PageIndex<A>
{
    public abstract PageRef<A> AlwaysAlloc();
    public abstract bool Alloc(out PageRef<A> result);
    public abstract bool Free(PageRef<A> Page);
}
