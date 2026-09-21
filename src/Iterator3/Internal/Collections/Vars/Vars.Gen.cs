#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
using IteratorPrototype.Types;

namespace IteratorPrototype.Iterator3.Internal.Collections;

abstract class VarsGen<A>
{
    public static VarsGen<A> Instance;

    [MethodImpl(Optimisations.Default)]
    static VarsGen()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedVars<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else if (Ty<A>.IsValue)
        {
            var type = typeof(StructVars<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            
        }
        else if(Ty<A>.IsManaged)
        {
            var type = typeof(ManagedVars<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            throw new Exception($"We have a type {Ty<A>.Pretty} that apparently isn't managed, unmanaged, or a value-type!");
        }
    }

    public static unsafe IterOp yield => 
        Instance.Yield;

    public abstract unsafe IterOp Yield { get; }
    public abstract bool DupImpl(ref Vars vars);    
    public abstract bool PopImpl(ref Vars vars, out A value, bool force);
    public abstract bool PopImpl(ref Vars vars, bool force);
    public abstract bool PushImpl(ref Vars vars, in A value, bool isCoRoutineArgument);
    public abstract bool PeekImpl(ref Vars vars, out A value);    
    public abstract ref A PeekAtImpl(ref Vars vars);    
}

class ManagedVars<A> : VarsGen<A>
    where A : class
{
    static ManagedVars() =>
        Instance = new ManagedVars<A>();

    public override unsafe IterOp Yield =>
        &Vars.yieldManaged<A>;
    
    [MethodImpl(Optimisations.Default)]
    public override bool DupImpl(ref Vars vars) =>
        vars.DupManaged<A>();
    
    [MethodImpl(Optimisations.Default)]
    public override bool PopImpl(ref Vars vars, out A value, bool force) =>
        vars.PopManaged(out value, force);
    
    [MethodImpl(Optimisations.Default)]
    public override bool PopImpl(ref Vars vars, bool force) =>
        vars.PopManaged(force);
    
    [MethodImpl(Optimisations.Default)]
    public override bool PushImpl(ref Vars vars, in A value, bool isCoRoutineArgument) =>
        vars.PushManaged(value, isCoRoutineArgument);

    [MethodImpl(Optimisations.Default)]
    public override bool PeekImpl(ref Vars vars, out A value) =>
        vars.PeekManaged(out value);

    [MethodImpl(Optimisations.Default)]
    public override ref A PeekAtImpl(ref Vars vars) =>
        ref vars.PeekAtManaged<A>();
}

class StructVars<A> : VarsGen<A>
    where A : struct
{
    static StructVars() =>
        Instance = new StructVars<A>();

    public override unsafe IterOp Yield =>
        &Vars.yieldStruct<A>;
    
    [MethodImpl(Optimisations.Default)]
    public override bool DupImpl(ref Vars vars) =>
        vars.DupStruct<A>();

    [MethodImpl(Optimisations.Default)]
    public override bool PopImpl(ref Vars vars, out A value, bool force) =>
        vars.PopStruct(out value, force);
    
    [MethodImpl(Optimisations.Default)]
    public override bool PopImpl(ref Vars vars, bool force) =>
        vars.PopStruct<A>(force);
    
    [MethodImpl(Optimisations.Default)]
    public override bool PushImpl(ref Vars vars, in A value, bool isCoRoutineArgument) =>
        vars.PushStruct(value, isCoRoutineArgument);

    [MethodImpl(Optimisations.Default)]
    public override bool PeekImpl(ref Vars vars, out A value) =>
        vars.PeekStruct(out value);

    [MethodImpl(Optimisations.Default)]
    public override ref A PeekAtImpl(ref Vars vars) =>
        ref vars.PeekAtStruct<A>();
}

class UnmanagedVars<A> : VarsGen<A>
    where A : unmanaged
{
    static UnmanagedVars() =>
        Instance = new UnmanagedVars<A>();

    public override unsafe IterOp Yield =>
        &Vars.yieldUnmanaged<A>;

    [MethodImpl(Optimisations.Default)]
    public override bool DupImpl(ref Vars vars) =>
        vars.DupUnmanaged<A>();
    
    [MethodImpl(Optimisations.Default)]
    public override bool PopImpl(ref Vars vars, out A value, bool force) =>
        vars.PopUnmanaged(out value, force);
    
    [MethodImpl(Optimisations.Default)]
    public override bool PopImpl(ref Vars vars, bool force) =>
        vars.PopUnmanaged<A>(force);
    
    [MethodImpl(Optimisations.Default)]
    public override bool PushImpl(ref Vars vars, in A value, bool isCoRoutineArgument) =>
        vars.PushUnmanaged(value, isCoRoutineArgument);

    [MethodImpl(Optimisations.Default)]
    public override bool PeekImpl(ref Vars vars, out A value) =>
        vars.PeekUnmanaged(out value);

    [MethodImpl(Optimisations.Default)]
    public override ref A PeekAtImpl(ref Vars vars) =>
        ref vars.PeekAtUnmanaged<A>();
}