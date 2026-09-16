#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
    
namespace IteratorPrototype.Iterator3.Internal.Collections;

abstract class GlobalsGen<A>
{
    public static GlobalsGen<A> Instance;

    [MethodImpl(Optimisations.Default)]
    static GlobalsGen()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedGlobals<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else if (Ty<A>.IsValue)
        {
            var type = typeof(StructGlobals<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else if (Ty<A>.IsManaged)
        {
            var type = typeof(ManagedGlobals<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            throw new Exception("We have a type {typeof(Ty).Name} that apparently isn't managed, unmanaged, or a value-type!");
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe IterOp yield(in ushort index) => 
        Instance.Yield(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe IterOp yieldConst(in ushort index) => 
        Instance.YieldConst(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe IterOp pull(in ushort index) => 
        Instance.Pull(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe IterOp push(in ushort index) => 
        Instance.Push(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public static unsafe IterOp reset(in ushort index) => 
        Instance.Reset(in index);

    public abstract unsafe IterOp Yield(in ushort index);
    public abstract unsafe IterOp YieldConst(in ushort index);
    public abstract unsafe IterOp Pull(in ushort index);
    public abstract unsafe IterOp Push(in ushort index);
    public abstract unsafe IterOp Reset(in ushort index);

    public abstract bool At(ref Globals list, in ushort ix, out A value);
    public abstract ref A At(ref Globals list, ushort ix);
    public abstract bool DeclaredAt(ref Globals list, in ushort ix, out A value);
    public abstract ref A DeclaredAt(ref Globals list, ushort ix);
    public abstract bool ResetAt(ref Globals list, in ushort ix, out A value);
    public abstract bool ResetAt(ref Globals list, in ushort ix);
    public abstract bool Add(ref Globals list, in A value);
    public abstract bool Add(ref Globals list, in A value, out ushort index);
    public abstract bool AtEnd(ref Globals list, in ushort ix, out Global<A> global);

}

class ManagedGlobals<A> : GlobalsGen<A>
    where A : class
{
    static ManagedGlobals() =>
        Instance = new ManagedGlobals<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Yield(in ushort index) =>
        GManaged<A>.yield(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp YieldConst(in ushort index) =>
        GManaged<A>.yieldConst(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Pull(in ushort index) =>
        GManaged<A>.pull(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Push(in ushort index) =>
        GManaged<A>.push(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Reset(in ushort index) =>
        GManaged<A>.reset(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool At(ref Globals list, in ushort ix, out A value) =>
        list.AtManaged(in ix, out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override ref A At(ref Globals list, ushort ix)=>
        ref list.AtManaged<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool DeclaredAt(ref Globals list, in ushort ix, out A value) =>
        list.DeclaredAtManaged(ix, out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override ref A DeclaredAt(ref Globals list, ushort ix) =>
        ref list.DeclaredAtManaged<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool ResetAt(ref Globals list, in ushort ix, out A value) =>
        list.ResetAtManaged(ix, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool ResetAt(ref Globals list, in ushort ix) =>
        list.ResetAtManaged<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Add(ref Globals list, in A value) =>
        list.AddManaged(in value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddManaged(in value, out index);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override bool AtEnd(ref Globals list, in ushort ix, out Global<A> global) =>
        list.AtEndManaged(ix, out global);
}

class UnmanagedGlobals<A> : GlobalsGen<A>
    where A : unmanaged
{
    static UnmanagedGlobals() =>
        Instance = new UnmanagedGlobals<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Yield(in ushort index) =>
        GUnmanaged<A>.yield(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp YieldConst(in ushort index) =>
        GUnmanaged<A>.yieldConst(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Pull(in ushort index) =>
        GUnmanaged<A>.pull(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Push(in ushort index) =>
        GUnmanaged<A>.push(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Reset(in ushort index) =>
        GUnmanaged<A>.reset(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool At(ref Globals list, in ushort ix, out A value) =>
        list.AtUnmanaged(in ix, out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override ref A At(ref Globals list, ushort ix)=>
        ref list.AtUnmanaged<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool DeclaredAt(ref Globals list, in ushort ix, out A value) =>
        list.DeclaredAtUnmanaged(ix, out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override ref A DeclaredAt(ref Globals list, ushort ix) =>
        ref list.DeclaredAtUnmanaged<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool ResetAt(ref Globals list, in ushort ix, out A value) =>
        list.ResetAtUnmanaged(ix, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool ResetAt(ref Globals list, in ushort ix) =>
        list.ResetAtUnmanaged<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Add(ref Globals list, in A value) =>
        list.AddUnmanaged(in value);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddUnmanaged(in value, out index);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override bool AtEnd(ref Globals list, in ushort ix, out Global<A> global) =>
        list.AtEndUnmanaged(ix, out global);
}

class StructGlobals<A> : GlobalsGen<A>
    where A : struct
{
    static StructGlobals() =>
        Instance = new StructGlobals<A>();

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Yield(in ushort index) =>
        GStruct<A>.yield(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp YieldConst(in ushort index) =>
        GStruct<A>.yieldConst(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Pull(in ushort index) =>
        GStruct<A>.pull(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Push(in ushort index) =>
        GStruct<A>.push(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override unsafe IterOp Reset(in ushort index) =>
        GStruct<A>.reset(in index);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool At(ref Globals list, in ushort ix, out A value) =>
        list.AtStruct(in ix, out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override ref A At(ref Globals list, ushort ix)=>
        ref list.AtStruct<A>(ix);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override bool DeclaredAt(ref Globals list, in ushort ix, out A value) =>
        list.DeclaredAtStruct(ix, out value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override ref A DeclaredAt(ref Globals list, ushort ix) =>
        ref list.DeclaredAtStruct<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool ResetAt(ref Globals list, in ushort ix, out A value) =>
        list.ResetAtStruct(ix, out value);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool ResetAt(ref Globals list, in ushort ix) =>
        list.ResetAtStruct<A>(ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Add(ref Globals list, in A value) =>
        list.AddStruct(in value);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddStruct(in value, out index);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public override bool AtEnd(ref Globals list, in ushort ix, out Global<A> global) =>
        list.AtEndStruct(ix, out global);
}
