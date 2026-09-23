using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit)]
readonly struct Op
{
    [FieldOffset(0)]
    public readonly nint Fun;

    [MethodImpl(Optimisations.Default)]
    public Op(nint fun)
    {
        Fun = fun;
    }
}
