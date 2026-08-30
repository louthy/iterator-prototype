#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

abstract class VarsGen<A>
{
    public static VarsGen<A> Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
            throw new Exception("We have a type {typeof(Ty).Name} that apparently isn't managed, unmanaged, or a value-type!");
        }
    }

    public abstract bool PopImpl(ref Vars vars, out A value);
    public abstract bool PopImpl(ref Vars vars);
    public abstract bool PushImpl(ref Vars vars, in A value);
    public abstract bool PeekImpl(ref Vars vars, out A value);    
    public abstract ref A PeekAtImpl(ref Vars vars);    
}

class ManagedVars<A> : VarsGen<A>
    where A : class
{
    static ManagedVars() =>
        Instance = new ManagedVars<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars vars, out A value) =>
        vars.PopManaged(out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars vars) =>
        vars.PopManaged();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars vars, in A value) =>
        vars.PushManaged(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars vars, out A value) =>
        vars.PeekManaged(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override ref A PeekAtImpl(ref Vars vars) =>
        ref vars.PeekAtManaged<A>();
}

class StructVars<A> : VarsGen<A>
    where A : struct
{
    static StructVars() =>
        Instance = new StructVars<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars vars, out A value) =>
        vars.PopStruct(out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars vars) =>
        vars.PopStruct();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars vars, in A value) =>
        vars.PushStruct(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars vars, out A value) =>
        vars.PeekStruct(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override ref A PeekAtImpl(ref Vars vars) =>
        ref vars.PeekAtStruct<A>();
}

class UnmanagedVars<A> : VarsGen<A>
    where A : unmanaged
{
    static UnmanagedVars() =>
        Instance = new UnmanagedVars<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars vars, out A value) =>
        vars.PopUnmanaged(out value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars vars) =>
        vars.PopUnmanaged<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars vars, in A value) =>
        vars.PushUnmanaged(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars vars, out A value) =>
        vars.PeekUnmanaged(out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override ref A PeekAtImpl(ref Vars vars) =>
        ref vars.PeekAtUnmanaged<A>();
}