#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS0649 // Field is never assigned to
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Memory;
using IteratorPrototype.Types;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly partial struct Vars
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void PushFlagManaged(bool isCoRoutineArgument)
    {
        // Set the flag for whether this is a coroutine argument
        ref var f = ref Unsafe.Add(ref Unsafe.AsRef(in flag0), top);
        f = Unsafe.As<bool, byte>(ref isCoRoutineArgument);
        
        // Increase top
        ref var t = ref Unsafe.AsRef(in top);
        t++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void PushFlagUnmanaged(bool isCoRoutineArgument)
    {
        // Set the flag for whether this is a coroutine argument
        ref var f = ref Unsafe.Add(ref Unsafe.AsRef(in flag0), top);
        f = (byte)(2 | Unsafe.As<bool, byte>(ref isCoRoutineArgument));
        
        // Increase top
        ref var t = ref Unsafe.AsRef(in top);
        t++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void PopFlag()
    {
        // Decrease top
        ref var t = ref Unsafe.AsRef(in top);
        t--;
    }

    public bool PeekIsCoRoutineArgument
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => top > 0 && (Unsafe.Add(ref Unsafe.AsRef(in flag0), top - 1) & 1) == 1;
    }

    bool PeekIsManaged
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => top > 0 && (Unsafe.Add(ref Unsafe.AsRef(in flag0), top - 1) & 2) == 0;
    }

    bool PeekIsUnmanaged
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => top > 0 && (Unsafe.Add(ref Unsafe.AsRef(in flag0), top - 1) & 2) == 2;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool DupStruct<A>()
        where A : struct =>
        objs.Dup();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool DupManaged<A>() 
        where A : class =>
        objs.Dup();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool DupUnmanaged<A>()
        where A : unmanaged =>
        values.Dup<A>();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool PushStruct<A>(in A value, bool isCoRoutineArgument)
        where A : struct =>
        PushManaged(Box.alloc(in value), isCoRoutineArgument);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PushManaged<A>(in A value, bool isCoRoutineArgument)
        where A : class
    {
        if (objs.Push(in value))
        {
            // Set the flag for whether this is a coroutine argument
            PushFlagManaged(isCoRoutineArgument);
            return true;
        }
        else
        {
            return false;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PushUnmanaged<A>(in A value, bool isCoRoutineArgument)
        where A : unmanaged
    {
        if (values.Push(in value))
        {
            // Set the flag for whether this is a coroutine argument
            PushFlagUnmanaged(isCoRoutineArgument);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool PopStruct<A>(out A value, bool force)
        where A : struct
    {
        if (!force && PeekIsCoRoutineArgument && PeekStruct(out value))
        {
            return true;
        }
        else if (PopManaged<Box<A>>(out var box, force))
        {
            value = box.Value;
            box.Free();
            PopFlag();
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PopManaged<A>(out A value, bool force)
        where A : class
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return PeekManaged(out value);
        }

        objs.Pop(out value);
        PopFlag();
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PopUnmanaged<A>(out A value, bool force)
        where A : unmanaged
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return PeekUnmanaged(out value);
        }

        values.Pop(out value);
        PopFlag();
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PopStruct<A>(bool force)
        where A : struct
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return true;
        }
        else if (PopManaged<Box<A>>(out var box, force))
        {
            box.Free();
            PopFlag();
            return true;
        }
        else
        {
            return false;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PopManaged(bool force)
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return true;
        }
        
        objs.Pop();
        PopFlag();
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PopUnmanaged<A>(bool force)
        where A : unmanaged
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return true;
        }
        
        values.Pop<A>();
        PopFlag();
        return true;
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

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PeekManaged<A>(out A value)
        where A : class =>
        objs.Peek(out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool PeekUnmanaged<A>(out A value)
        where A : unmanaged =>
        values.Peek(out value);

    [MethodImpl(Optimisations.Max)]
    public bool SyncTo(in Tops tops)
    {
        var os      = (uint)(objs.Count   << Tops.ObjsShift)   & Tops.ObjsMask;
        var vs      = (uint)(values.Count << Tops.ValuesShift) & Tops.ValuesMask;
        var t       = (uint)(top          << Tops.VarsShift)   & Tops.VarsMask;
        var current = tops.Current & ~(Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
        tops.CurrentRef = current | os | vs | t;
        return true;
    }

    [MethodImpl(Optimisations.Max)]
    public bool SyncFrom(in Tops tops)
    {
        var snapshot = tops.Current & (Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
        var os       = (int)((snapshot & Tops.ObjsMask)   >> Tops.ObjsShift);
        var vs       = (int)((snapshot & Tops.ValuesMask) >> Tops.ValuesShift);
        var nt       = (int)((snapshot & Tops.VarsMask)   >> Tops.VarsShift);

        // Set the flags top to reflect how many objs and vals we're losing:
        ref var t = ref Unsafe.AsRef(in top);
        t = (byte)nt;
        
        // Reset the tops
        return objs.PopToTop(os) && values.PopToTop(vs);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Zero()
    {
        // Set the flags top to zero.
        ref var t = ref Unsafe.AsRef(in top);
        t = 0;
        
        return objs.PopToTop(0) && values.PopToTop(0);
    }
    public int ObjsCount => 
        objs.Count;
    
    public int ValuesCount => 
        values.Count;

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldManaged<A>(in StackFrame frame)
        where A : class
    {
        //Log.coroutine($"start-yield [managed : {Ty<A>.Pretty}, sizeof: {Unsafe.SizeOf<A>()}]", in frame);
        
        // Set the flag for stating this is a coroutine argument
        ref var f = ref Unsafe.Add(ref Unsafe.AsRef(in frame.vars.flag0), frame.vars.top - 1);
        f = 1;
        
        // Save the current top values for the stack
        ref var topRef   = ref Unsafe.AsRef(in frame.vars.objs.Count);
        var     topValue = topRef;
        
        // Virtually pop off the top value (which is the result of the current co-routine)
        topRef--;
        
        // Start the yield co-routine
        frame.StartYieldScope();
        
        // Virtually re-push the top value (it will become the argument to the co-routine).
        topRef = topValue;

        //Log.coroutine("end-yield", in frame);
        
        return PullState.Continue;        
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldUnmanaged<A>(in StackFrame frame)
        where A : unmanaged
    {
        //Log.coroutine($"start-yield [unmanaged : {Ty<A>.Pretty}, sizeof: {Unsafe.SizeOf<A>()}]", in frame);
        
        // Set the flag for stating this is a coroutine argument
        ref var f = ref Unsafe.Add(ref Unsafe.AsRef(in frame.vars.flag0), frame.vars.top - 1);
        f = 1;
        
        // Save the current top values for the stack
        ref var topRef   = ref Unsafe.AsRef(in frame.vars.values.Count);
        var     topValue = topRef;
        var     sizeOfA  = Unsafe.SizeOf<A>();
        
        // Virtually pop off the top value (which is the result of the current co-routine)
        topRef -= sizeOfA;
        
        // Start the yield co-routine
        frame.StartYieldScope();
        
        // Virtually re-push the top value (it will become the argument to the co-routine).
        topRef = topValue;

        //Log.coroutine("end-yield", in frame);
        
        return PullState.Continue;        
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldStruct<A>(in StackFrame frame)
        where A : struct =>
        yieldManaged<Box<A>>(in frame);
}
