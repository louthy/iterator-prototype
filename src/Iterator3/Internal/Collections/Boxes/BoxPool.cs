using System.Runtime.CompilerServices;
// ReSharper disable ParameterHidesMember

namespace IteratorPrototype.Iterator3.Internal.Collections;

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
class BoxPool<A>
    where A : struct
{
    const int InitialBlockSize = 64;
    const int MaxPoolSize = 1 << 24;

    readonly Box<A> last;
    Box<A> head;
    int count;

    volatile int locked;

    /// <summary>
    /// Pool identifier.
    /// </summary>
    /// <remarks>
    /// The bottom 24 bits of a pool identifier are zeroed.  This is so we can create
    /// identifiers for the boxes and then OR them with the pool identifier, making a
    /// lookup identifier.
    /// </remarks>
    public readonly BoxPoolId Id;
    
    [MethodImpl(Optimisations.InliningOnly)]
    internal BoxPool(BoxPoolId id)
    {
        Id = id;
        
        // Create a 'root' terminating item that will never yield and will only be
        // used to test for the end of the list.
        last = new Box<A>(this, null);
        
        // Point the head at the last terminating item. This makes an empty linked-list.
        head = last;
        
        for(uint i = 0; i < InitialBlockSize; i++)
        {
            head = new Box<A>(this, head);
        }
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    void CreateNewBlock()
    {
        // We already have `count` boxes, so adding `count` more doubles the collection.
        var ncount = count << 1;
        var nhead  = head;
        for (var i = count; i < ncount; i++)
        {
            nhead = new Box<A>(this, nhead);
        }
        
        // Switch to the new index and head
        head = nhead;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Box<A> Alloc(in A value)
    {
        SpinWait sw = default;
        while (true)
        {
            if (Interlocked.CompareExchange(ref locked, 1, 0) == 0)
            {
                // Are we out of boxes?
                if (ReferenceEquals(head, last))
                {
                    // Create a new block of `BlockSize` boxes
                    CreateNewBlock();
                }
                
                // Allocate
                var box = head;
                
                // Remove from the free-list
                head = head.next!;
                
                // Unlock the allocator
                locked = 0;
                
                // Initialise the boxed value
                box.OnAlloc(in value);
                return box;
            }
            sw.SpinOnce();
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void Free(in Box<A> box)
    {
        SpinWait sw = default;
        while (true)
        {
            if (Interlocked.CompareExchange(ref locked, 1, 0) == 0)
            {
                box.OnFree(head);
                locked = 0;
                return;
            }
            sw.SpinOnce();
        }        
    }
}
