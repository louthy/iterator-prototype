using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

enum OpReturn : byte
{
    Default = 0,
    CanVoid = 1,
    CoRoutine = 2
    
    // Can't use a number bigger than 7 here because we're using the
    // lower bits of the function address for storage
}

[SkipLocalsInit]
[StructLayout(LayoutKind.Sequential)]
[method: MethodImpl(Optimisations.Max)]
readonly struct Op(nint fun, OpReturn @return)
{
    readonly nint Fun = fun | (byte)@return;
    readonly OpReturn @return;

    [MethodImpl(Optimisations.Max)]
    public int Invoke(in StackFrame frame)
    {
        unsafe
        {
            return ((IterOp)(Fun & ~7))(in frame);
        }
    }
}
