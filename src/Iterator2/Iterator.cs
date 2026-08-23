using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
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
    internal readonly OpStack ops;
    internal readonly ObjStack objs;
    internal readonly ByteStack values;
}
