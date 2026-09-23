using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit)]
[method: MethodImpl(Optimisations.Max)]
readonly struct Op(nint fun)
{
    [FieldOffset(0)]
    readonly nint Fun = fun;

    [MethodImpl(Optimisations.Max)]
    public unsafe Op(IterOp fun) : this((nint)fun)
    {
    }

    [MethodImpl(Optimisations.Max)]
    public int Invoke(in StackFrame frame)
    {
        unsafe
        {
            return ((IterOp)Fun)(in frame);
        }
    }
}
