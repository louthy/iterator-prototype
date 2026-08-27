using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
public readonly struct PullState : IEquatable<PullState>, IEquatable<int>
{
    public readonly int Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    PullState(int value) =>
        Value = value;
    
    public static readonly PullState Void = new (0);
    public static readonly PullState Continue = new (1);
    public static readonly PullState Pure = new(2);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator false(PullState s) =>
        s.Value == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator true(PullState s) =>
        s.Value > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals(PullState other) =>
        Value == other.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals(int other) =>
        Value == other;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals(object? obj) =>
        obj is PullState other && Equals(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override int GetHashCode() =>
        Value;
}
