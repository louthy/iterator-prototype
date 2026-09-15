#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. 
#pragma warning disable CS0169 // Field is never used

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory.Internal;

class LineIndex<LineType, A> : LineIndex<A>
    where LineType : Line<LineType, A>, Tr.Constructor<LineType>
{
    const int Capacity = 64;

    LineRef<A>? free00;
    LineRef<A>? free01;
    LineRef<A>? free02;
    LineRef<A>? free03;
    LineRef<A>? free04;
    LineRef<A>? free05;
    LineRef<A>? free06;
    LineRef<A>? free07;
    LineRef<A>? free08;
    LineRef<A>? free09;
    LineRef<A>? free0A;
    LineRef<A>? free0B;
    LineRef<A>? free0C;
    LineRef<A>? free0D;
    LineRef<A>? free0E;
    LineRef<A>? free0F;
    LineRef<A>? free10;
    LineRef<A>? free11;
    LineRef<A>? free12;
    LineRef<A>? free13;
    LineRef<A>? free14;
    LineRef<A>? free15;
    LineRef<A>? free16;
    LineRef<A>? free17;
    LineRef<A>? free18;
    LineRef<A>? free19;
    LineRef<A>? free1A;
    LineRef<A>? free1B;
    LineRef<A>? free1C;
    LineRef<A>? free1D;
    LineRef<A>? free1E;
    LineRef<A>? free1F;

    LineRef<A>? free20;
    LineRef<A>? free21;
    LineRef<A>? free22;
    LineRef<A>? free23;
    LineRef<A>? free24;
    LineRef<A>? free25;
    LineRef<A>? free26;
    LineRef<A>? free27;
    LineRef<A>? free28;
    LineRef<A>? free29;
    LineRef<A>? free2A;
    LineRef<A>? free2B;
    LineRef<A>? free2C;
    LineRef<A>? free2D;
    LineRef<A>? free2E;
    LineRef<A>? free2F;
    LineRef<A>? free30;
    LineRef<A>? free31;
    LineRef<A>? free32;
    LineRef<A>? free33;
    LineRef<A>? free34;
    LineRef<A>? free35;
    LineRef<A>? free36;
    LineRef<A>? free37;
    LineRef<A>? free38;
    LineRef<A>? free39;
    LineRef<A>? free3A;
    LineRef<A>? free3B;
    LineRef<A>? free3C;
    LineRef<A>? free3D;
    LineRef<A>? free3E;
    LineRef<A>? free3F;

    int allocIndex;
    int freeIndex;
    readonly int processorId;
    
    public LineIndex(int pid)
    {
        processorId = pid;
        
        // Start place to look for places to re-allocate. This means the first allocation will
        // update `freeIndex` to be `0`.  That allows a pool of unallocated entries to follow 
        // the `allocIndex`.
        freeIndex = Capacity - 1;
        
        // Prime the index with lines
        ref var free   = ref free00;
        var     sizeOf = Unsafe.SizeOf<LineRef<A>>();
        
        for(var i = 0; i < Capacity; i++)
        {
            free = new LineRef<A>(new LineId(LineId.StateFlag.Indexed, pid, i), LineType.Construct());
            free = ref Unsafe.AddByteOffset(ref free, sizeOf)!;
        }        
    }

    public override bool Alloc(out LineRef<A> result)
    {
        var     ai   = allocIndex;
        var     step = Unsafe.SizeOf<LineRef<A>>();
        ref var free = ref Unsafe.AddByteOffset(ref free00, ai * step);
        
        for(var i = allocIndex; i < Capacity; i++)
        {
            if (free is not null)
            {
                // Put the alloc search-index 1 past this entry, so we're more likely to find an available entry
                var tai = ai < Capacity - 1 ? i + 1 : 0;
                
                Interlocked.CompareExchange(ref allocIndex, tai, ai);
                
                // Remove from the index and return
                result = free;
                result.SetLineId(new LineId(LineId.StateFlag.Allocated, processorId, i));
                GC.ReRegisterForFinalize(result);
                free = null;
                
                // We update the free-index only if the current free index is the predecessor
                // to this entry.  That means we open up a contiguous free Line.
                if (i == 0)
                {
                    Interlocked.CompareExchange(ref freeIndex, i, Capacity - 1);
                }
                else
                {
                    Interlocked.CompareExchange(ref freeIndex, i, i - 1);
                }

                return true;
            }
            free = ref Unsafe.AddByteOffset(ref free, step);
        }

        // Back to the beginning
        free = ref free00;
        
        for(var i = 0; i < allocIndex; i++)
        {
            if (free is not null)
            {
                // Put the alloc search-index 1 past this Line, so we're more likely to find a free Line
                var tai = ai < Capacity - 1 ? i + 1 : 0;
                Interlocked.CompareExchange(ref allocIndex, tai, ai);
                
                // Remove from the index and return
                result = free;
                result.SetLineId(new LineId(LineId.StateFlag.Allocated, processorId, i));
                GC.ReRegisterForFinalize(result);
                free = null!;
                
                // We update the free-index only if the current free index is the predecessor
                // to this entry.  That means we open up a contiguous free Line.
                if (i == 0)
                {
                    Interlocked.CompareExchange(ref freeIndex, i, Capacity - 1);
                }
                else
                {
                    Interlocked.CompareExchange(ref freeIndex, i, i - 1);
                }
                
                return true;
            }
            free = ref Unsafe.AddByteOffset(ref free, step);
        }
        
        result = null!;
        return false;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override LineRef<A> AlwaysAlloc()
    {
        if (Alloc(out var result))
        {
            return result;
        }
        else
        {
            var line = LineType.Construct();
            return new LineRef<A>(new LineId(LineId.StateFlag.Allocated, processorId, 0xffff), line);
        }
    }

    public override bool Free(LineRef<A> line)
    {
        // If the Page is None or has already been freed, return false
        if (!line.IfAllocatedChangeToIndexed()) return false;
        
        // We don't need any more finalisation if we're in the free-list
        GC.SuppressFinalize(line);
        
        var     ai    = allocIndex;
        var     fi    = freeIndex;
        var     step  = Unsafe.SizeOf<LineRef<A>>();
        var     nstep = -step;
        ref var free  = ref Unsafe.AddByteOffset(ref free00, fi * step);
        
        for(var i = fi; i >= 0; i--)
        {
            if (free is null)
            {
                // Put the free search-index 1 before this Line, so we're more likely to find an alloc Line
                var tfi = fi > 0 ? i - 1 : Capacity - 1;
                Interlocked.CompareExchange(ref freeIndex, tfi, fi);
                
                // Place back in the index
                free = line;
                line.SetLineId(new LineId(LineId.StateFlag.Indexed, processorId, i));
                
                // Remember the last alloc index. It's then easier to find the next alloc Line
                Interlocked.CompareExchange(ref allocIndex, i, ai);
                
                return true;
            }
            free = ref Unsafe.AddByteOffset(ref free, nstep)!;
        }

        free = ref Unsafe.AddByteOffset(ref free00, (Capacity - 1) * step);
        
        for(var i = Capacity - 1; i > fi; i--)
        {
            if (free is null)
            {
                // Put the free search-index 1 before this Line, so we're more likely to find an alloc Line
                var tfi = fi > 0 ? i - 1 : Capacity - 1;
                Interlocked.CompareExchange(ref freeIndex, tfi, fi);
                
                // Place back in the index
                free = line;
                line.SetLineId(new LineId(LineId.StateFlag.Indexed, processorId, i));
                
                // Remember the last alloc index. It's then easier to find the next alloc Line
                Interlocked.CompareExchange(ref allocIndex, i, ai);
                
                return true;
            }
            free = ref Unsafe.AddByteOffset(ref free, nstep)!;
        }
        return false;
    }
}