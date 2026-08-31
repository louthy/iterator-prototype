using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
public static class PullState
{
    public const int Void = 0;
    public const int Continue = 1;
    public const int Pure = 2;
}
