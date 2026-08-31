using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
[method: MethodImpl(Optimisations.Default)]
public readonly struct Global<A>(in ushort index)
{
    public readonly ushort Index = index;

    [MethodImpl(Optimisations.Default)]
    internal ref A Value(ref StackFrame frame) =>
        ref frame.globals.At<A>(Index);

    [MethodImpl(Optimisations.Default)]
    internal bool Update(ref StackFrame frame, in A value)
    {
        Value(ref frame) = value;
        return true;
    }
}