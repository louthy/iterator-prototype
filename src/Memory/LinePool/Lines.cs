using System.Runtime.CompilerServices;
using IteratorPrototype.Memory.Internal;

namespace IteratorPrototype.Memory;

/// <summary>
/// Pre-allocates and pools Pages of elements. Enough to be within a single
/// page of memory. The idea is to create N-element Pages where the elements
/// are contiguous in memory and fit within a single page of memory and
/// therefore are 'cache-friendly'.
///
/// Use this to implement collection types that expand.  Allocate the Pages
/// as you need them and allow them to free themselves back to the pool when
/// done.
/// </summary>
/// <typeparam name="A"></typeparam>
[SkipLocalsInit]
static class Lines<A>
{
    /// <summary>
    /// Overhead for the line version number
    /// </summary>
    const int MetaDataOverhead = 4;  
    
    public static readonly int ItemsPerLine;
    
    static readonly LineIndex<A> index;

    static Lines()
    {
        ItemsPerLine = itemsPerLine();

        index = ItemsPerLine switch
                {
                    2  => new LineIndexPool<Line2<A>, A>(),
                    4  => new LineIndexPool<Line4<A>, A>(),
                    6  => new LineIndexPool<Line6<A>, A>(),
                    // TODO : Consider 7 as a valid item-per-line number, this will fit 8 references into a line,
                    // TODO:  otherwise it's only 6, which leaves quite a bit of wasted space per cache-line.
                    8  => new LineIndexPool<Line8<A>, A>(),
                    10 => new LineIndexPool<Line10<A>, A>(),
                    12 => new LineIndexPool<Line12<A>, A>(),
                    14 => new LineIndexPool<Line14<A>, A>(),
                    16 => new LineIndexPool<Line16<A>, A>(),
                    18 => new LineIndexPool<Line18<A>, A>(),
                    20 => new LineIndexPool<Line20<A>, A>(),
                    24 => new LineIndexPool<Line24<A>, A>(),
                    28 => new LineIndexPool<Line28<A>, A>(),
                    32 => new LineIndexPool<Line32<A>, A>(),
                    40 => new LineIndexPool<Line40<A>, A>(),
                    48 => new LineIndexPool<Line48<A>, A>(),
                    64 => new LineIndexPool<Line64<A>, A>()
                };
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool alloc(out LineRef<A> line) => 
        index.Alloc(out line);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static LineRef<A> alwaysAlloc() => 
        index.AlwaysAlloc();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool free(LineRef<A> line) => 
        index.Free(line);

    [MethodImpl(Optimisations.InliningOnly)]
    static int itemsPerLine()
    {
        // Get the size of the OS memory line 
        var lineSize = CacheLine.Size - MetaDataOverhead;
        
        // And the size of each element we're going to store in a Page
        var sizeOfItem = Unsafe.SizeOf<A>();

        // Find the initial items-per-Page
        var ipb = lineSize / sizeOfItem;

        // Make count into a power of 2
        return findAppropriatePageSize(ipb);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    static int findAppropriatePageSize(int size) =>
        size switch
        {
            >= 64 => 64,
            >= 48 => 48,
            >= 40 => 40,
            >= 32 => 32,
            >= 28 => 28,
            >= 24 => 24,
            >= 20 => 20,
            >= 18 => 18,
            >= 16 => 16,
            >= 14 => 14,
            >= 12 => 12,
            >= 10 => 10,
            >= 8  => 8,
            // TODO : Consider 7 as a valid item-per-line number, this will fit 8 references into a line,
            // TODO:  otherwise it's only 6, which leaves quite a bit of wasted space per cache-line.
            >= 6  => 6,
            >= 4  => 4,
            >= 2  => 2,
            _ => throw new InvalidOperationException($"The structure '{typeof(A).Name}' is too large for the "   +
                                                     "Line-pooling system. Efficiency can't be guaranteed, so " +
                                                     "use other methods to manage large value-types")
        };
}