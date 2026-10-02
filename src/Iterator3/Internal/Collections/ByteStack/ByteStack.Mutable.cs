#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = sizeof(int) + ByteStack.Capacity)]
struct ByteStackMutable
{
    [FieldOffset(0)]
    public uint Count;
    
    [FieldOffset(4)]
    public byte Stack;
}
