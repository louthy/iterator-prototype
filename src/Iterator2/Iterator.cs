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
    internal readonly IteratorSource? source;
    internal readonly OpFrame ops;
    internal readonly ObjStack objs;
    internal readonly ByteStack values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool TryGetValue(out A head, out Iterator2<A> tail)
    {
        // Copy
        // Consider better ways, but remember, `ta` might also be `tail`, which means doing `tail = default` to
        // initialise it will overwrite `ta`.
        tail = this;

        ref var source1 = ref Unsafe.AsRef(in tail.source);
        ref var ops1    = ref Unsafe.AsRef(in tail.ops);
        ref var objs1   = ref Unsafe.AsRef(in tail.objs);
        ref var values1 = ref Unsafe.AsRef(in tail.values);
        var     frame   = new StackFrame(ref source1, ref objs1, ref values1);

        while (source1 is not null)
        {
            if(!source1.Run(ref frame))
            {
                // The `Run` method needs to set the subsequent source or set it to `null` so that we 
                // either move onto the next item or return `false`.
                continue;
            }
                    
            if(ops1.Run(ref frame))
            {
                ValueStack<A>.Pop(ref frame, out head);
                return true;
            }
        }
        head = default!;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref Iterator2<A> other)
    {
        other.SetSource(in source);
        ops.CopyTo(ref Unsafe.AsRef(in other.ops));
        objs.CopyTo(ref Unsafe.AsRef(in other.objs));
        values.CopyTo(ref Unsafe.AsRef(in other.values));
    }
}
