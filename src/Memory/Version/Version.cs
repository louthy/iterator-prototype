using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[method: MethodImpl(Optimisations.InliningOnly)]
[StructLayout(LayoutKind.Explicit, Size = 4)]
public readonly struct Version(int value) :
    IAdditiveIdentity<Version, Version>,
    IAdditionOperators<Version, Version, Version>,
    IAdditionOperators<Version, int, Version>,
    IComparisonOperators<Version, Version, bool>,
    IComparisonOperators<Version, int, bool>,
    IEquatable<Version>,
    IComparable<Version>
{
    [FieldOffset(0)]
    readonly int Value = value;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Equals(Version other) =>
        Value == other.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Equals(object? obj) =>
        obj is Version other && Equals(other);

    [MethodImpl(Optimisations.InliningOnly)]
    public override int GetHashCode() =>
        Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator ==(Version left, Version right) =>
        left.Equals(right);

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator !=(Version left, Version right) =>
        !left.Equals(right);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >(Version left, Version right) =>
        left.Value > right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <(Version left, Version right) =>
        left.Value < right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >=(Version left, Version right) =>
        left.Value >= right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <=(Version left, Version right) =>
        left.Value <= right.Value;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static Version operator +(Version left, Version right) =>
        new(left.Value + right.Value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static Version operator +(Version left, int right) =>
        new(left.Value + right);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator ==(Version left, int right) =>
        left.Value == right;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator !=(Version left, int right) =>
        left.Value != right;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >(Version left, int right) =>
        left.Value > right;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >=(Version left, int right) =>
        left.Value >= right;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <(Version left, int right) =>
        left.Value < right;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <=(Version left, int right) =>
        left.Value <= right;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator ==(int left, Version right) =>
        left == right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator !=(int left, Version right) =>
        left != right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >(int left, Version right) =>
        left > right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator >=(int left, Version right) =>
        left >= right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <(int left, Version right) =>
        left < right.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static bool operator <=(int left, Version right) =>
        left <= right.Value;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Version(int value) =>
        new(value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Version(uint value) =>
        new((int)value);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator int(Version value) =>
        value.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator uint(Version value) =>
        (uint)value.Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public int CompareTo(Version other) =>
        Value.CompareTo(other.Value);

    public static Version AdditiveIdentity
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => default; 
    }

    public static Version Zero
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => default; 
    }

    public override string ToString() => 
        Value.ToString();
    
}
