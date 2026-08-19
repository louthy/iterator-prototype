using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype;

// Large struct for Iterable states
[StructLayout(LayoutKind.Explicit, Size = 32)]
public readonly struct Space32;

// Large struct for Iterable states
[StructLayout(LayoutKind.Explicit, Size = 64)]
public readonly struct Space64;

// Large struct for Iterable states
[StructLayout(LayoutKind.Explicit, Size = 128)]
public readonly struct Space128
{
    public A As<A>() where A : unmanaged
    {
        var self = this;
        return Unsafe.As<Space128, A>(ref self);
    }
}

// Large struct for Iterable states
[StructLayout(LayoutKind.Explicit, Size = 256)]
public readonly struct Space256;
