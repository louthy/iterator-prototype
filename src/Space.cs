using System.Runtime.InteropServices;

namespace IteratorTest;

// Large struct for IterableK states
[StructLayout(LayoutKind.Explicit, Size = 32)]
public readonly struct Space32;

// Large struct for IterableK states
[StructLayout(LayoutKind.Explicit, Size = 64)]
public readonly struct Space64;

// Large struct for IterableK states
[StructLayout(LayoutKind.Explicit, Size = 128)]
public readonly struct Space128;

// Large struct for IterableK states
[StructLayout(LayoutKind.Explicit, Size = 256)]
public readonly struct Space256;
