using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype.Internal;

readonly ref struct StackFrame
{
    public readonly ref IteratorSource? Source;
    public readonly ref OpStack Ops;
    public readonly ref ObjStack Objs;
    public readonly ref ByteStack Values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref IteratorSource? source, ref OpStack ops, ref ObjStack objs, ref ByteStack values)
    {
        Source = ref source;
        Ops = ref ops;
        Objs = ref objs;
        Values = ref values;
    }
}
