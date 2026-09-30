#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS0649 // Field is never assigned to
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorPrototype.Iterator3.Internal.Memory;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
struct VarsMutable
{
    const int Capacity = 31;
    
    ObjStack objs;
    ByteStack values;
    
    // These flags remember if a value is a co-routine argument, or not, and if so, stops it
    // being popped off the stack (when the `force` flag is `false). That means subsequent 
    // loops through an 'iterable' can use the full stack of co-routine arguments.
    byte flag0, flag1, flag2, flag3, flag4, flag5, flag6, flag7;
    byte flag8, flag9, flagA, flagB, flagC, flagD, flagE, flagF;
    byte flag10, flag11, flag12, flag13, flag14, flag15, flag16, flag17;
    byte flag18, flag19, flag1A, flag1B, flag1C, flag1D, flag1E /*, flag1F -- we're using this byte for `top` */;
    byte top;
    
    public Span<bool> Flags
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, bool>(ref Unsafe.AsRef(in flag0)), top);
    }
    
    public Span<byte> FlagBytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in flag0), top);
    }

    public Span<bool> AllFlags
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, bool>(ref Unsafe.AsRef(in flag0)), Capacity);
    }    
    
    public Span<byte> AllFlagBytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in flag0), Capacity);
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

    public bool PeekIsManaged
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get
        {
            var s = FlagBytes;
            return s.Length > 0 && (s[^1] & 2) == 0;
        }
    }

    public bool PeekIsUnmanaged
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get
        {
            var s = FlagBytes;
            return s.Length > 0 && (s[^1] & 2) == 2;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PushFlagManaged(bool isCoRoutineArgument)
    {
        // Set the flag for whether this is a coroutine argument
        top++;
        if(top == 0) throw new InvalidOperationException("HOW?");
        Flags[^1] = isCoRoutineArgument;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PushFlagUnmanaged(bool isCoRoutineArgument)
    {
        // Set the flag for whether this is a coroutine argument
        top++;
        FlagBytes[^1] = (byte)(2 | Unsafe.As<bool, byte>(ref isCoRoutineArgument)); 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PopFlag()
    {
        top--;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void DupStruct<A>()
        where A : struct =>
        objs.Dup();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void DupManaged<A>() 
        where A : class =>
        objs.Dup();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void DupUnmanaged<A>()
        where A : unmanaged =>
        values.Dup<A>();
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void PushStruct<A>(in A value, bool isCoRoutineArgument)
        where A : struct =>
        PushManaged(Box.alloc(in value), isCoRoutineArgument);

    [MethodImpl(Optimisations.InliningOnly)]
    public void PushManaged<A>(in A value, bool isCoRoutineArgument)
        where A : class
    {
        objs.Push(in value);
        PushFlagManaged(isCoRoutineArgument);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PushUnmanaged<A>(in A value, bool isCoRoutineArgument)
        where A : unmanaged
    {
        values.Push(in value);
        PushFlagUnmanaged(isCoRoutineArgument);
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public void PopStruct<A>(out A value, bool force)
        where A : struct
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            PeekStruct(out value);
        }
        else
        {
            PopManaged<Box<A>>(out var box, force);
            value = box.Value;
            box.Free();
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PopManaged<A>(out A value, bool force)
        where A : class
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            PeekManaged(out value);
            return;
        }

        objs.Pop(out value);
        PopFlag();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PopUnmanaged<A>(out A value, bool force)
        where A : unmanaged
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            PeekUnmanaged(out value);
            return;
        }

        values.Pop(out value);
        PopFlag();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PopStruct<A>(bool force)
        where A : struct
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return;
        }

        PopManaged<Box<A>>(out var box, force);
        box.Free();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PopManaged(bool force)
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return;
        }
        
        objs.Pop();
        PopFlag();
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void PopUnmanaged<A>(bool force)
        where A : unmanaged
    {
        if (!force && PeekIsCoRoutineArgument)
        {
            return ;
        }
        
        values.Pop<A>();
        PopFlag();
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

    [MethodImpl(Optimisations.InliningOnly)]
    public void SyncTo(ref TopsMutable tops1)
    {
        var os      = (uint)(objs.Count   << Tops.ObjsShift)   & Tops.ObjsMask;
        var vs      = (uint)(values.Count << Tops.ValuesShift) & Tops.ValuesMask;
        var t       = (uint)(top          << Tops.VarsShift)   & Tops.VarsMask;
        var current = tops1.Current & ~(Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
        tops1.Current = current | os | vs | t;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public void SyncFrom(in Tops tops)
    {
        var snapshot = tops.Current & (Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
        var os       = (int)((snapshot & Tops.ObjsMask)   >> Tops.ObjsShift);
        var vs       = (int)((snapshot & Tops.ValuesMask) >> Tops.ValuesShift);
        var nt       = (int)((snapshot & Tops.VarsMask)   >> Tops.VarsShift);

        // Set the flags top to reflect how many objs and vals we're losing:
        top = (byte)nt;
        
        // Reset the tops
        objs.PopToTop(os);
        values.PopToTop(vs);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Zero()
    {
        // Set the flags top to zero.
        top = 0;
        
        return objs.PopToTop(0) && values.PopToTop(0);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static int yieldManaged<A>(in StackFrame frame)
        where A : class
    {
        //Log.coroutine($"start-yield [managed : {Ty<A>.Pretty}, sizeof: {Unsafe.SizeOf<A>()}]", in frame);
        
        // Set the flag for stating this is a coroutine argument
        ref var vars = ref frame.vars.Ref;
        vars.FlagBytes[^1] = 1;
        
        // Save the current top values for the stack
        ref var topRef   = ref Unsafe.AsRef(in vars.objs.Count);
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
        ref var vars = ref frame.vars.Ref;
        vars.FlagBytes[^1] = 1;
        
        // Save the current top values for the stack
        ref var topRef   = ref Unsafe.AsRef(in vars.values.Count);
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
