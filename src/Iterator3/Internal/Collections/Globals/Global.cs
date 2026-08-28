using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
[method: MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
public readonly struct Global<A>(in ushort index)
{
    public readonly ushort Index = index;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal ref A Value(ref StackFrame frame) =>
        ref frame.globals.At<A>(Index);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal bool Update(ref StackFrame frame, in A value)
    {
        Value(ref frame) = value;
        return true;
    }
}