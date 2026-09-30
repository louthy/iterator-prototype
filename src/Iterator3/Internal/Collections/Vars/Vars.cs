#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS0649 // Field is never assigned to
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorPrototype.Memory;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct Vars
{
    const int Capacity = 31;
    
    readonly ObjStack objs;
    readonly ByteStack values;
    
    // These flags remember if a value is a co-routine argument, or not, and if so, stops it
    // being popped off the stack (when the `force` flag is `false). That means subsequent 
    // loops through an 'iterable' can use the full stack of co-routine arguments.
    readonly byte flag0, flag1, flag2, flag3, flag4, flag5, flag6, flag7;
    readonly byte flag8, flag9, flagA, flagB, flagC, flagD, flagE, flagF;
    readonly byte flag10, flag11, flag12, flag13, flag14, flag15, flag16, flag17;
    readonly byte flag18, flag19, flag1A, flag1B, flag1C, flag1D, flag1E /*, flag1F -- we're using this byte for `top` */;
    readonly byte top;

    public ReadOnlySpan<bool> Flags
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<byte, bool>(ref Unsafe.AsRef(in flag0)), top);
    }
    
    ReadOnlySpan<byte> FlagBytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in flag0), top);
    }

    ReadOnlySpan<bool> AllFlags
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<byte, bool>(ref Unsafe.AsRef(in flag0)), Capacity);
    }    
    
    ReadOnlySpan<byte> AllFlagBytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in flag0), Capacity);
    }
    
    public int ObjsCount => 
        objs.Count;
    
    public int ValuesCount => 
        values.Count;

    public bool PeekIsCoRoutineArgument
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get
        {
            var s = FlagBytes;
            return s.Length > 0 && (s[^1] & 1) == 1;
        }
    }

    bool PeekIsManaged
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get
        {
            var s = FlagBytes;
            return s.Length > 0 && (s[^1] & 2) == 0;
        }
    }

    bool PeekIsUnmanaged
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get
        {
            var s = FlagBytes;
            return s.Length > 0 && (s[^1] & 2) == 2;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void SyncTo(ref TopsMutable tops)
    {
        var os      = (uint)(objs.Count   << Tops.ObjsShift)   & Tops.ObjsMask;
        var vs      = (uint)(values.Count << Tops.ValuesShift) & Tops.ValuesMask;
        var t       = (uint)(top          << Tops.VarsShift)   & Tops.VarsMask;
        var current = tops.Current                             & ~(Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
        tops.Current = current | os | vs | t;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public ref A PeekAtStruct<A>()
        where A : struct =>
        ref objs.PeekAt<Box<A>>().Ref;

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A PeekAtManaged<A>()
        where A : class =>
        ref objs.PeekAt<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A PeekAtUnmanaged<A>()
        where A : unmanaged =>
        ref values.PeekAt<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public void PeekStruct<A>(out A value)
        where A : struct
    {
        objs.Peek<Box<A>>(out var box);
        value = box.Value;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PeekManaged<A>(out A value)
        where A : class =>
        objs.Peek(out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public void PeekUnmanaged<A>(out A value)
        where A : unmanaged =>
        values.Peek(out value);    
}
