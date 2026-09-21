using System.Runtime.CompilerServices;
using IteratorPrototype.Memory.Internal;
using IteratorPrototype.Types;

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
static class Pages<A>
{
    /// <summary>
    /// It's one cache-line, so it's a good offset.
    /// sizeof(MetaData32) + sizeof(MetaDataVersion) + additional capacity for later.
    /// </summary>
    const int MetaDataOverhead = 64;  
    
    public static readonly uint ItemsPerPage;
    
    static readonly PageIndex<A> index;

    static Pages()
    {
        ItemsPerPage = itemsPerPage();

        index = ItemsPerPage switch
                {
                    4    => new PageIndexPool<Page4<A>, A>(),
                    8    => new PageIndexPool<Page8<A>, A>(),
                    16   => new PageIndexPool<Page16<A>, A>(),
                    32   => new PageIndexPool<Page32<A>, A>(),
                    64   => new PageIndexPool<Page64<A>, A>(),
                    128  => new PageIndexPool<Page128<A>, A>(),
                    256  => new PageIndexPool<Page256<A>, A>(),
                    504  => new PageIndexPool<Page504<A>, A>(),
                    512  => new PageIndexPool<Page512<A>, A>(),
                    1008 => new PageIndexPool<Page1008<A>, A>(),
                    _    => new PageIndexPool<Page1024<A>, A>()
                };
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool alloc(out PageRef<A> page) => 
        index.Alloc(out page);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static PageRef<A> alwaysAlloc() => 
        index.AlwaysAlloc();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool free(PageRef<A> page) => 
        index.Free(page);

    [MethodImpl(Optimisations.InliningOnly)]
    static uint  itemsPerPage()
    {
        // Get the size of the OS memory page 
        var pageSize = Environment.SystemPageSize - MetaDataOverhead;
        
        // And the size of each element we're going to store in a Page
        var sizeOfItem = Unsafe.SizeOf<A>();

        // Find the initial items-per-Page
        var ipb = pageSize / sizeOfItem;

        // Make count into a power of 2
        return findAppropriatePageSize(ipb);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    static uint  findAppropriatePageSize(int size) =>
        size switch
        {
            >= 1024 => 1024,
            >= 1008 => 1008,
            >= 768  => 768,
            >= 512  => 512,
            >= 504  => 504,
            >= 256  => 256,
            >= 128  => 128,
            >= 64   => 64,
            >= 32   => 32,
            >= 16   => 16,
            >= 8    => 8,
            >= 4    => 4,
            _ => throw new InvalidOperationException($"The structure '{Ty<A>.Pretty}' is too large for the "    +
                                                     "Page-pooling system. Efficiency can't be guaranteed, so " +
                                                     "use other methods to manage large value-types")
        };
}