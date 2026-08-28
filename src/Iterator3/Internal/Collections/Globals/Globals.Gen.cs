#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

abstract class GlobalsGen<A>
{
    public static GlobalsGen<A> Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

    public abstract bool At(ref Globals list, in ushort ix, out A value);
    public abstract ref A At(ref Globals list, ushort ix);
    
    public abstract bool Add(ref Globals list, in A value);
    public abstract bool Add(ref Globals list, in A value, out ushort index);
}

class ManagedGlobals<A> : GlobalsGen<A>
    where A : class
{
    static ManagedGlobals() =>
        Instance = new ManagedGlobals<A>();

    public override bool At(ref Globals list, in ushort ix, out A value) =>
        list.AtManaged(in ix, out value);
    
    public override ref A At(ref Globals list, ushort ix)=>
        ref list.AtManaged<A>(ix);
    
    public override bool Add(ref Globals list, in A value) =>
        list.AddManaged(in value);
    
    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddManaged(in value, out index);
}

class StructGlobals<A> : GlobalsGen<A>
    where A : struct
{
    static StructGlobals() =>
        Instance = new StructGlobals<A>();

    public override bool At(ref Globals list, in ushort ix, out A value) =>
        list.AtStruct(in ix, out value);
    
    public override ref A At(ref Globals list, ushort ix)=>
        ref list.AtStruct<A>(ix);
    
    public override bool Add(ref Globals list, in A value) =>
        list.AddStruct(in value);
    
    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddStruct(in value, out index);
}

class UnmanagedGlobals<A> : GlobalsGen<A>
    where A : unmanaged
{
    static UnmanagedGlobals() =>
        Instance = new UnmanagedGlobals<A>();

    public override bool At(ref Globals list, in ushort ix, out A value) =>
        list.AtUnmanaged(in ix, out value);
    
    public override ref A At(ref Globals list, ushort ix)=>
        ref list.AtUnmanaged<A>(ix);

    public override bool Add(ref Globals list, in A value) =>
        list.AddUnmanaged(in value);

    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddUnmanaged(in value, out index);
}