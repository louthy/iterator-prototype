using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly partial struct Vars
{
    readonly ObjStack objs;
    readonly ByteStack values;
    
    [MethodImpl(Optimisations.Default)]
    public bool PushStruct<A>(in A value)
        where A : struct =>
        PushManaged(new Box<A>(in value));

    [MethodImpl(Optimisations.Default)]
    public bool PushManaged<A>(in A value)
        where A : class =>
        objs.Push(in value);

    [MethodImpl(Optimisations.Default)]
    public bool PushUnmanaged<A>(in A value)
        where A : unmanaged =>
        values.Push(in value);

    [MethodImpl(Optimisations.Default)]
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

    [MethodImpl(Optimisations.Default)]
    public bool PopManaged<A>(out A value)
        where A : class =>
        objs.Pop(out value);

    [MethodImpl(Optimisations.Default)]
    public bool PopUnmanaged<A>(out A value)
        where A : unmanaged =>
        values.Pop(out value);

    [MethodImpl(Optimisations.Default)]
    public bool PopStruct() =>
        PopManaged();

    [MethodImpl(Optimisations.Default)]
    public bool PopManaged() =>
        objs.Pop();

    [MethodImpl(Optimisations.Default)]
    public bool PopUnmanaged<A>()
        where A : unmanaged =>
        values.Pop<A>();
    
    [MethodImpl(Optimisations.Default)]
    public ref A PeekAtStruct<A>()
        where A : struct
    {
        ref var box = ref objs.PeekAt<Box<A>>();
        return ref Unsafe.AsRef(in box.Value);
    }

    [MethodImpl(Optimisations.Default)]
    public ref A PeekAtManaged<A>()
        where A : class =>
        ref objs.PeekAt<A>();

    [MethodImpl(Optimisations.Default)]
    public ref A PeekAtUnmanaged<A>()
        where A : unmanaged =>
        ref values.PeekAt<A>();

    [MethodImpl(Optimisations.Default)]
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

    [MethodImpl(Optimisations.Default)]
    public bool PeekManaged<A>(out A value)
        where A : class =>
        objs.Peek(out value);

    [MethodImpl(Optimisations.Default)]
    public bool PeekUnmanaged<A>(out A value)
        where A : unmanaged =>
        values.Peek(out value);

    [MethodImpl(Optimisations.Default)]
    public bool SyncTo(ref Tops tops)
    {
        var os      = (uint)(objs.Count   & 0xFF) << 16;
        var vs      = (uint)(values.Count & 0xFF) << 8;
        var current = tops.Current & 0xFF0000FF;
        tops.CurrentRef = current | os | vs;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool SyncFrom(in Tops tops)
    {
        var snapshot = tops.Current & 0x00FFFF00;
        var os       = (int)((snapshot & 0x00FF0000) >> 16);
        var vs       = (int)((snapshot & 0x0000FF00) >> 8);
        
        // TODO: This feels a bit shonky. Arguably the stacks should be torn down without the need for this.
        return objs.PopToTop(os) && 
               values.PopToTop(vs);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Zero() =>
        objs.PopToTop(0) && values.PopToTop(0);
    
    public int ObjsCount => 
        objs.Count;
    
    public int ValuesCount => 
        values.Count;
}
