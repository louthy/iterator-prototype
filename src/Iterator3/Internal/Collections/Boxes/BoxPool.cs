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
    const int BlockSize = 64;
    
    volatile int locked;
    readonly Box<A> last;
    Box<A> head;

    [MethodImpl(Optimisations.InliningOnly)]
    internal BoxPool()
    {
        last = new Box<A>(this, null);
        head = last;
        
        for(var i = 0; i < BlockSize; i++)
        {
            head = new Box<A>(this, head);
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    static Box<A> CreateNewBlock(Box<A> head)
    {
        for (var i = 0; i < BlockSize; i++)
        {
            head = new Box<A>(head.pool, head);
        }
        return head;
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
                    head = CreateNewBlock(head);
                }
                
                var box = head;
                head = head.next!; 
                locked = 0;
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
