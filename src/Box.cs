using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public sealed class Box<A>
    where A : struct
{
    public readonly A Value;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public Box(in A value) =>
        Value = value;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator A(in Box<A> box) =>
        box.Value;

    public ref A Ref
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.AsRef(in Value);
    }
    
    public ref readonly A ReadonlyRef
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.AsRef(in Value);
    }
}