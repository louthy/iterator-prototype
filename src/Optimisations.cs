using System.Runtime.CompilerServices;

namespace IteratorPrototype;

public static class Optimisations
{
    public const MethodImplOptions None = default;
    
    public const MethodImplOptions Default = None;

    public const MethodImplOptions Max =
        MethodImplOptions.AggressiveInlining |
        MethodImplOptions.AggressiveOptimization;

    public const MethodImplOptions InliningOnly =
        MethodImplOptions.AggressiveInlining |
        MethodImplOptions.AggressiveOptimization;

    public const MethodImplOptions Agro =
        MethodImplOptions.AggressiveOptimization;
}