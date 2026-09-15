#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. 
#pragma warning disable CS0169 // Field is never used

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Memory.Internal;

class PageIndex<PageType, A> : PageIndex<A>
    where PageType : Page<PageType, A>, Tr.Constructor<PageType>
{
    const int Capacity = 64;

    PageRef<A>? free00;
    PageRef<A>? free01;
    PageRef<A>? free02;
    PageRef<A>? free03;
    PageRef<A>? free04;
    PageRef<A>? free05;
    PageRef<A>? free06;
    PageRef<A>? free07;
    PageRef<A>? free08;
    PageRef<A>? free09;
    PageRef<A>? free0A;
    PageRef<A>? free0B;
    PageRef<A>? free0C;
    PageRef<A>? free0D;
    PageRef<A>? free0E;
    PageRef<A>? free0F;
    PageRef<A>? free10;
    PageRef<A>? free11;
    PageRef<A>? free12;
    PageRef<A>? free13;
    PageRef<A>? free14;
    PageRef<A>? free15;
    PageRef<A>? free16;
    PageRef<A>? free17;
    PageRef<A>? free18;
    PageRef<A>? free19;
    PageRef<A>? free1A;
    PageRef<A>? free1B;
    PageRef<A>? free1C;
    PageRef<A>? free1D;
    PageRef<A>? free1E;
    PageRef<A>? free1F;

    PageRef<A>? free20;
    PageRef<A>? free21;
    PageRef<A>? free22;
    PageRef<A>? free23;
    PageRef<A>? free24;
    PageRef<A>? free25;
    PageRef<A>? free26;
    PageRef<A>? free27;
    PageRef<A>? free28;
    PageRef<A>? free29;
    PageRef<A>? free2A;
    PageRef<A>? free2B;
    PageRef<A>? free2C;
    PageRef<A>? free2D;
    PageRef<A>? free2E;
    PageRef<A>? free2F;
    PageRef<A>? free30;
    PageRef<A>? free31;
    PageRef<A>? free32;
    PageRef<A>? free33;
    PageRef<A>? free34;
    PageRef<A>? free35;
    PageRef<A>? free36;
    PageRef<A>? free37;
    PageRef<A>? free38;
    PageRef<A>? free39;
    PageRef<A>? free3A;
    PageRef<A>? free3B;
    PageRef<A>? free3C;
    PageRef<A>? free3D;
    PageRef<A>? free3E;
    PageRef<A>? free3F;

    int top;
    readonly int processorId;
    
    public PageIndex(int pid)
    {
        processorId = pid;
        top = 0;
        
        // Prime the index with Pages
        ref var free   = ref free00;
        var     sizeOf = Unsafe.SizeOf<PageRef<A>>();
        
        for(var i = 0; i < Capacity; i++)
        {
            free = new PageRef<A>(new PageId(PageId.StateFlag.Indexed, pid, i), PageType.Construct());
            free = ref Unsafe.AddByteOffset(ref free, sizeOf)!;
        }        
    }

    public override bool Alloc(out PageRef<A> result)
    {
        SpinWait sw = default;
        while (top < Capacity)
        {
            var     fi   = top;
            ref var free = ref Unsafe.AddByteOffset(ref free00, fi * Unsafe.SizeOf<PageRef<A>>());
            result = free!;

            if (Interlocked.CompareExchange(ref top, fi + 1, fi) == fi)
            {
                // This changes the free-slot to `null` only if nothing else has since freed a page and put a concrete
                // value in it. We don't really care if it fails, because that still clears the reference of what we're
                // about to return, meaning the returned page has no roots other than the returned reference.
                Interlocked.CompareExchange(ref free, null, result);

                result.SetPageId(new PageId(PageId.StateFlag.Allocated, processorId, fi));
                result.Page.Version = default;
                result.Page.MetaData = default;
                //GC.ReRegisterForFinalize(result);
                return true;
            }

            sw.SpinOnce();
        }
        result = null!;
        return false;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override PageRef<A> AlwaysAlloc()
    {
        if (Alloc(out var result))
        {
            return result;
        }
        else
        {
            var page = PageType.Construct();
            return new PageRef<A>(new PageId(PageId.StateFlag.Allocated, processorId, 0xffff), page);
        }
    }

    public override bool Free(PageRef<A> page)
    {
        // If the Page is None or has already been freed, return false
        if (!page.IfAllocatedChangeToIndexed()) return false;
        
        // We don't need any more finalisation if we're in the free-list
        //GC.SuppressFinalize(page);
        
        SpinWait sw = default;
        while (top > 0)
        {
            var     fi   = top;
            var     tfi  = fi - 1;
            ref var free = ref Unsafe.AddByteOffset(ref free00, tfi * Unsafe.SizeOf<PageRef<A>>());

            if (Interlocked.CompareExchange(ref free, page, null) == null &&
                Interlocked.CompareExchange(ref top, tfi, fi)     == fi)
            {
                page.SetPageId(new PageId(PageId.StateFlag.Indexed, processorId, fi));
                return true;
            }

            sw.SpinOnce();
        }

        return false;        
    }
}