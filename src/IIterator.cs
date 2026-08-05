using System.Runtime.CompilerServices;

namespace IteratorTest;

public interface IIterator<IA, A>
    where IA : IIterator<IA, A>
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(out A head, out IA tail);
}