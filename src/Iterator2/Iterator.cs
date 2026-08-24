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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<A> tail)
    {
        // Copy
        // Consider better ways, but remember, `ta` might also be `tail`, which means doing `tail = default` to
        // initialise it will overwrite `ta`.
        tail = this;
        
        ref var stack1 = ref Unsafe.AsRef(in tail.stack);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref Iterator2<A> other)
    {
        // TODO: Decide if a manual copy is faster than a struct assignment.
        other = this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void SetSource(in IteratorSource<A> source) =>
        stack.SetSource(source);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal ref IteratorSource<A>? GetSource() =>
        ref stack.GetSource<A>();    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal void Add(Op<A> op) =>
        stack.Add(op);
}
