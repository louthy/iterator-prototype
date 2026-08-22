// ReSharper disable UnassignedField.Local
#pragma warning disable CS0169 // Field is never used

using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class MiniStack
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static MiniStack<A> singleton<A>(in A item) => 
        new() { item0 = item, Top = 1 };
}
