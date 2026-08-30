using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

namespace IteratorPrototype.Iterator3.Internal.Collections;

readonly partial struct Vars
{
    [StructLayout(LayoutKind.Explicit, Size = sizeof(byte) * 2)]
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly struct State(in byte objectsTop, in byte valuesTop)
    {
        [FieldOffset(0)]
        public readonly byte ObjectsTop = objectsTop;
        
        [FieldOffset(1)]
        public readonly byte ValuesTop = valuesTop;
    }
}
