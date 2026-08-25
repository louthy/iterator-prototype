using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;

namespace IteratorPrototype.Internal;

[SkipLocalsInit]
readonly ref struct StackFrame
{
    public readonly ref OpStack stack;
    public readonly ref OpFrame frame;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public StackFrame(ref OpStack stack, ref OpFrame frame)
    {
        this.stack = ref stack;
        this.frame = ref frame;
    }
}
