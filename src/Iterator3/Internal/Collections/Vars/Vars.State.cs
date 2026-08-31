using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

namespace IteratorPrototype.Iterator3.Internal.Collections;

readonly partial struct Vars
{
    [SkipLocalsInit]
    [method: MethodImpl(Optimisations.Default)]
    public readonly struct State(in uint bits)
    {
        public readonly uint Bits = bits;
    }
}
