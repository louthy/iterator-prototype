using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.VM;

namespace IteratorPrototype.Internal;

readonly ref struct StackFrame
{
    public readonly ref IteratorVM VM;
    public readonly ref OpStack Ops;
    public readonly ref ObjStack Objs;
    public readonly ref ByteStack Values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref IteratorVM vm, ref OpStack ops, ref ObjStack objs, ref ByteStack values)
    {
        VM = ref vm;
        Ops = ref ops;
        Objs = ref objs;
        Values = ref values;
    }
}
