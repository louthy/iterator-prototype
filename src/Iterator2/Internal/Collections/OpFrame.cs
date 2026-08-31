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

    [MethodImpl(Optimisations.Default)]
    public bool Run(ref StackFrame stack) =>
        source!.Run(ref stack) && ops.Run(ref stack);
    
    [MethodImpl(Optimisations.Default)]
    public void SetSource(in IteratorSource? src)
    { 
        ref var src1 = ref Unsafe.AsRef(in source);
        src1 = src;
    }
        
    [MethodImpl(Optimisations.Default)]
    public ref IteratorSource<A>? GetSource<A>() =>
        ref Unsafe.As<IteratorSource?, IteratorSource<A>?>(ref Unsafe.AsRef(in source));

    [MethodImpl(Optimisations.Default)]
    public void Add(in Op op) =>
        ops.Add(in op);

    [MethodImpl(Optimisations.Default)]
    public void Clear()
    {
        // Clear GC references
        ops.Clear();
        objs.Clear();
        ref var src = ref Unsafe.AsRef(in source);
        src = null;
    }
    
    [MethodImpl(Optimisations.Default)]
    public void CopyTo(ref OpFrame dest)
    {
        // TODO: Decide if a manual copy is faster than a struct assignment.
        //dest = this;

        ref var dsrc = ref Unsafe.AsRef(in dest.source);
        dsrc = source;
        
        if(ops.Count != 0) ops.CopyTo(ref Unsafe.AsRef(in dest.ops));
        if(objs.Top  != 0) objs.CopyTo(ref Unsafe.AsRef(in dest.objs));
        if(objs.Top  != 0) values.CopyTo(ref Unsafe.AsRef(in dest.values));
    }
}
