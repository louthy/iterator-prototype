using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype.Internal.Collections;

/// <summary>
/// OpFrame is a sequence of Op objects. Basically a highly optimised list.
/// Not a stack like the other related types.
/// </summary>
[SkipLocalsInit]
readonly struct OpFrame
{
    public readonly IteratorSource? source;
    public readonly Ops ops; 
    public readonly ObjStack objs;
    public readonly ByteStack values;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run()
    {
        ref var frame = ref Unsafe.AsRef(in this);
        return (source?.Run(ref frame) ?? false) && ops.Run(ref frame);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void SetSource(in IteratorSource? src)
    { 
        ref var src1 = ref Unsafe.AsRef(in source);
        src1 = src;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref IteratorSource<A>? GetSource<A>() =>
        ref Unsafe.As<IteratorSource?, IteratorSource<A>?>(ref Unsafe.AsRef(in source));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Add(in Op op) =>
        ops.Add(in op);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Clear()
    {
        // Clear GC references
        ops.Clear();
        objs.Clear();
        ref var src = ref Unsafe.AsRef(in source);
        src = null;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref OpFrame dest)
    {
        // TODO: Decide if a manual copy is faster than a struct assignment.
        dest = this;
    }
}
