using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Memory;

/// <summary>
/// A pool of boxes.  
/// </summary>
/// <remarks>
/// A box can store a `struct` that is composed of managed fields. Unmanaged structs are stored
/// in the iterator state in raw byte form.
/// </remarks>
/// <remarks>
/// The pool is a singly linked free-list. It starts with `BlockSize` boxes and will grow by
/// `BlockSize` boxes each time it runs out.
/// </remarks>
/// <typeparam name="A">Type of struct that will be stored in the box</typeparam>
[SkipLocalsInit]
class BoxPool<A>
    where A : struct
{
    const int MaxPoolSize = 1024;
    const int InitialBlockSize = 32;

    int size = InitialBlockSize + 1;
    Box<A>? first;
    Box<A> free;
    readonly Box<A> term;
    readonly Entry[] boxes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public BoxPool()
    {
        first = new Box<A>(this);
        term = free = new Box<A>(this);
        boxes = new Entry[InitialBlockSize];
        for(var i = 0; i < InitialBlockSize; i++)
        {
            boxes[i].Box = new Box<A>(this);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public Box<A> Alloc(in A value)
    {
        var entry = first;
        if (entry == null || entry != Interlocked.CompareExchange(ref first, null, entry))
        {
            return AllocSlow(in value);
        }
        else
        {
            entry.Alloc(in value);
            return entry;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    Box<A> AllocSlow(in A value)
    {
        var bs = boxes;
        
        // Look for the first available box in the array
        for(var i = 0; i < bs.Length; i++)
        {
            var entry = bs[i].Box;
            if (entry is not null)
            {
                if (entry == Interlocked.CompareExchange(ref bs[i].Box, null, entry))
                {
                    size--;
                    entry.Alloc(in value);
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
                entry.Alloc(in value);
                return entry;
            }
        }

        var box = new Box<A>(this);
        box.Alloc(in value);
        return box;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Free(in Box<A> box)
    {
        if (first is null)
        {
            first = box;
        }
        else
        {
            FreeSlow(box);
        }
    }

    void FreeSlow(in Box<A> box)
    {
        var bs = boxes;

        for (var i = 0; i < bs.Length; i++)
        {
            if (bs[i].Box == null)
            {
                size++;
                bs[i].Box = box;
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

    /// <summary>
    /// Wrapper to avoid array covariance.
    /// </summary>
    [SkipLocalsInit]
    struct Entry
    {
        public Box<A>? Box;
    }
}
