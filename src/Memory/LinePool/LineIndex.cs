using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
abstract class LineIndex<A>
{
    public abstract LineRef<A> AlwaysAlloc();
    public abstract bool Alloc(out LineRef<A> result);
    public abstract bool Free(LineRef<A> Page);
}
