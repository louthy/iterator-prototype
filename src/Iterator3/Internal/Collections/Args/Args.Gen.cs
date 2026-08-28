#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

abstract class ArgsGen<A>
{
    public static ArgsGen<A> Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static ArgsGen()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedArgs<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            var type = typeof(ManagedArgs<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }

    public abstract bool Add(ref Args list, in A value);
    public abstract bool Add(ref Args list, in A value, out ushort index);
}

class ManagedArgs<A> : ArgsGen<A>
    where A : class
{
    static ManagedArgs() =>
        Instance = new ManagedArgs<A>();
    
    public override bool Add(ref Args list, in A value) =>
        list.AddObject(in value);
    
    public override bool Add(ref Args list, in A value, out ushort index) =>
        list.AddObject(in value, out index);
}

class UnmanagedArgs<A> : ArgsGen<A>
    where A : unmanaged
{
    static UnmanagedArgs() =>
        Instance = new UnmanagedArgs<A>();

    public override bool Add(ref Args list, in A value) =>
        list.AddUnmanaged(in value);

    public override bool Add(ref Args list, in A value, out ushort index) =>
        list.AddUnmanaged(in value, out index);
}