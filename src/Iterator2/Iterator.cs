using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Source.Factories;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype;


// FACTS:
//
// I need to be able to push an Object and Space onto a stack
//   * Those should be pushed together as a stack-frame.
// A 'program' needs to run on a series of instructions
//   * The program needs to acquire the T, IS, A (and B) values through inheritance
//   * The program will need a program-counter (PC) to know which instruction we're on
//   * The instructions will need a ref stack of stack-frames.  It works on what's on top of the stack.
//   * Each instruction will need to know how to pop arguments off the stack also.

[SkipLocalsInit]
public readonly struct Iterator2<A>
{
    internal readonly OpStack stack;
    
    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(out A head, out Iterator2<A> tail)
    {
        tail = this;                        // Copy
        return tail.MoveNext(out head);
    }
    
    [MethodImpl(Optimisations.Default)]
    public IteratorEnumerator2<A> GetEnumerator() =>
        new (in this);
    
    [MethodImpl(Optimisations.Default)]
    internal bool MoveNext(out A head)
    {
        ref var stack1 = ref Unsafe.AsRef(in stack);
        if (stack1.Run())
        {
            ValueStack<A>.Pop(ref stack1.AtTop, out head);
            return true;
        }
        else
        {
            head = default!;
            return false;
        }
    }

    internal ref IteratorSource<A>? Source
    {
        [MethodImpl(Optimisations.Default)]
        get => ref stack.GetSource<A>();
    }
        
    [MethodImpl(Optimisations.Default)]
    internal void Add(Op<A> op) =>
        stack.Add(op);
}
