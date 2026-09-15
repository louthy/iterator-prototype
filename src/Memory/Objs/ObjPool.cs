using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
class ObjPool<A>
    where A : class, new()
{
    const int MaxPoolSize = 1024;
    const int InitialBlockSize = 32;

    int size = InitialBlockSize + 1;
    Obj<A>? first;
    Obj<A> free;
    readonly Obj<A> term;
    readonly Entry[] objs;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public ObjPool()
    {
        first = new Obj<A>(this);
        term = free = new Obj<A>(this);
        objs = new Entry[InitialBlockSize];
        for(var i = 0; i < InitialBlockSize; i++)
        {
            objs[i].Obj = new Obj<A>(this);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Obj<A> Alloc()
    {
        var entry = first;
        if (entry == null || entry != Interlocked.CompareExchange(ref first, null, entry))
        {
            return AllocSlow();
        }
        else
        {
            entry.Alloc();
            return entry;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    Obj<A> AllocSlow()
    {
        var bs = objs;
        
        // Look for the first available obj in the array
        for(var i = 0; i < bs.Length; i++)
        {
            var entry = bs[i].Obj;
            if (entry is not null)
            {
                if (entry == Interlocked.CompareExchange(ref bs[i].Obj, null, entry))
                {
                    size--;
                    entry.Alloc();
                    return entry;
                }                
            }
        }
        
        // Look for a free-list element
        while (free != term)
        {
            var entry = free;
            var next  = entry.next;

            if (entry == Interlocked.CompareExchange(ref entry, next, entry))
            {
                entry!.next = null;
                size--;
                entry.Alloc();
                return entry;
            }
        }

        var obj = new Obj<A>(this);
        obj.Alloc();
        return obj;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Free(in Obj<A> obj)
    {
        if (first is null)
        {
            first = obj;
        }
        else
        {
            FreeSlow(obj);
        }
    }

    void FreeSlow(in Obj<A> box)
    {
        var bs = objs;

        for (var i = 0; i < bs.Length; i++)
        {
            if (bs[i].Obj == null)
            {
                size++;
                bs[i].Obj = box;
               return;
            }
        }

        while (size < MaxPoolSize)
        {
            var entry = free;
            box.next = entry;
            if (entry == Interlocked.CompareExchange(ref free, box, entry))
            {
                size++;
                return;
            }
        }
    }

    // Wrapper to avoid array covariance.
    [SkipLocalsInit]
    struct Entry
    {
        public Obj<A>? Obj;
    }
}
