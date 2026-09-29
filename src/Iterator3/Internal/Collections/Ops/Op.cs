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
readonly struct Op
{
    readonly nint Fun;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Op(nint fun, OpReturn @return)
    {
        Fun = fun | (byte)@return;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Invoke(in StackFrame frame)
    {
        unsafe
        {
            return ((IterOp)(Fun & ~7))(in frame);
        }
    }

    public OpReturn Return
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (OpReturn)(Fun & 7);
    }
}
