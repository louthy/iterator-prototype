using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public interface IIterator<IA, A>
    where IA : IIterator<IA, A>
{
    public bool TryGetValue(out Nil nil);
    public bool TryGetValue(out A head, out IA tail);
    Iterator<A> Lower { get; }
}