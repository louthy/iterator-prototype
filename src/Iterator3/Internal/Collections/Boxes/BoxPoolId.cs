using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[StructLayout(LayoutKind.Explicit, Size = sizeof(uint))]
public readonly struct BoxPoolId : IEquatable<BoxPoolId>
{
    [FieldOffset(0)]
    readonly int id;

    [MethodImpl(Optimisations.InliningOnly)]
    internal BoxPoolId(int id) =>
        this.id = id;

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Equals(BoxPoolId other) =>
        id == other.id;

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Equals(object? obj) =>
        obj is BoxPoolId other && Equals(other);

    [MethodImpl(Optimisations.InliningOnly)]
    public override int GetHashCode() =>
        id;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"box-pool({id})";
}