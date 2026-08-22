// ReSharper disable UnassignedField.Local
#pragma warning disable CS0169 // Field is never used

using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public struct MiniStack<A>
{
    internal A item0;
    A item1;
    A item2;
    A item3;
    public int Top;
    public int Flags;
}
