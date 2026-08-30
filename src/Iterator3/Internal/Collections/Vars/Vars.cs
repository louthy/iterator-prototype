using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly partial struct Vars
{
    readonly ObjStack objs;
    readonly ByteStack values;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushStruct<A>(in A value)
        where A : struct =>
        PushManaged(new Box<A>(in value));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushManaged<A>(in A value)
        where A : class =>
        objs.Push(in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PushUnmanaged<A>(in A value)
        where A : unmanaged =>
        values.Push(in value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopStruct<A>(out A value)
        where A : struct
    {
        if (PopManaged<Box<A>>(out var box))
        {
            value = box.Value;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopManaged<A>(out A value)
        where A : class =>
        objs.Pop(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopUnmanaged<A>(out A value)
        where A : unmanaged =>
        values.Pop(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopStruct() =>
        PopManaged();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopManaged() =>
        objs.Pop();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PopUnmanaged<A>()
        where A : unmanaged =>
        values.Pop<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A PeekAtStruct<A>()
        where A : struct
    {
        ref var box = ref objs.PeekAt<Box<A>>();
        return ref Unsafe.AsRef(in box.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A PeekAtManaged<A>()
        where A : class =>
        ref objs.PeekAt<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref A PeekAtUnmanaged<A>()
        where A : unmanaged =>
        ref values.PeekAt<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PeekStruct<A>(out A value)
        where A : struct
    {
        if (objs.Peek<Box<A>>(out var box))
        {
            value = box.Value;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PeekManaged<A>(out A value)
        where A : class =>
        objs.Peek(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool PeekUnmanaged<A>(out A value)
        where A : unmanaged =>
        values.Peek(out value);

    public State Snapshot
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => new((byte)objs.Count, (byte)values.Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Reset(State snapshot) =>
        // TODO: This feels a bit shonky. Arguably the stacks should be torn down without the need for this.
        objs.PopToTop(snapshot.ObjectsTop) &&
        values.PopToTop(snapshot.ValuesTop);
}
