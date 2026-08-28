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
        else
        {
            var type = typeof(ManagedGlobals<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }

    public abstract bool Add(ref Globals list, in A value);
    public abstract bool Add(ref Globals list, in A value, out ushort index);
}

class ManagedGlobals<A> : GlobalsGen<A>
    where A : class
{
    static ManagedGlobals() =>
        Instance = new ManagedGlobals<A>();
    
    public override bool Add(ref Globals list, in A value) =>
        list.AddObject(in value);
    
    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddObject(in value, out index);
}

class UnmanagedGlobals<A> : GlobalsGen<A>
    where A : unmanaged
{
    static UnmanagedGlobals() =>
        Instance = new UnmanagedGlobals<A>();

    public override bool Add(ref Globals list, in A value) =>
        list.AddUnmanaged(in value);

    public override bool Add(ref Globals list, in A value, out ushort index) =>
        list.AddUnmanaged(in value, out index);
}