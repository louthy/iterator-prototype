using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class Box<A>
    where A : struct
{
    public readonly A Value;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Box(in A value) =>
        Value = value;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator A(in Box<A> box) =>
        box.Value;

    public ref A Ref
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Unsafe.AsRef(in Value);
    }
    
    public ref readonly A ReadonlyRef
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Unsafe.AsRef(in Value);
    }
}