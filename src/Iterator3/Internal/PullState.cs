using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal;

[SkipLocalsInit]
public readonly struct PullState : IEquatable<PullState>, IEquatable<int>
{
    public readonly int Value;

    [MethodImpl(Optimisations.Default)]
    PullState(int value) =>
        Value = value;
    
    public static readonly PullState Void = new (0);
    public static readonly PullState Continue = new (1);
    public static readonly PullState Pure = new(2);
    public static readonly PullState End = new (3);

    [MethodImpl(Optimisations.Default)]
    public static implicit operator PullState(bool flag) =>
        flag
            ? Continue
            : Void;

    [MethodImpl(Optimisations.Default)]
    public static implicit operator bool(PullState s) =>
        s.Value == 1;

    [MethodImpl(Optimisations.Default)]
    public static bool operator false(PullState s) =>
        s.Value == 0;

    [MethodImpl(Optimisations.Default)]
    public static bool operator true(PullState s) =>
        s.Value > 0;

    [MethodImpl(Optimisations.Default)]
    public bool Equals(PullState other) =>
        Value == other.Value;

    [MethodImpl(Optimisations.Default)]
    public bool Equals(int other) =>
        Value == other;

    [MethodImpl(Optimisations.Default)]
    public override bool Equals(object? obj) =>
        obj is PullState other && Equals(other);

    [MethodImpl(Optimisations.Default)]
    public override int GetHashCode() =>
        Value;
}
