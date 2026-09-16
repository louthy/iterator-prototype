#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

abstract class PullGen<A, B>
{
    internal static PullGen<A, B> Instance;

    [MethodImpl(Optimisations.Default)]
    static PullGen()
    {
        switch (Ty<A>.Flavour, Ty<B>.Flavour)
        {
            case (TyFlavour.Unmanaged, TyFlavour.Unmanaged):
            {
                var type = typeof(UnmanagedUnmanagedPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            case (TyFlavour.Unmanaged, TyFlavour.Managed):
            {
                var type = typeof(UnmanagedManagedPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Unmanaged, TyFlavour.Struct):
            {
                var type = typeof(UnmanagedStructPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Unmanaged):
            {
                var type = typeof(ManagedUnmanagedPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Managed):
            {
                var type = typeof(ManagedManagedPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Struct):
            {
                var type = typeof(ManagedStructPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Unmanaged):
            {
                var type = typeof(StructUnmanagedPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Managed):
            {
                var type = typeof(StructManagedPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Struct):
            {
                var type = typeof(StructStructPull<,>).MakeGenericType(typeof(A), typeof(B));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            default:
                throw new Exception("We have a type {typeof(Ty).Name} that apparently isn't managed, unmanaged, or a value-type!");

        }
    }

    public static unsafe IterOp map => 
        Instance.MapImpl;

    public abstract unsafe IterOp MapImpl { get; }
}

class ManagedManagedPull<A, B> : PullGen<A, B>
    where A : class
    where B : class
{
    static ManagedManagedPull() =>
        Instance = new ManagedManagedPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapManagedManaged<A, B>;
}

class ManagedUnmanagedPull<A, B> : PullGen<A, B>
    where A : class
    where B : unmanaged
{
    static ManagedUnmanagedPull() =>
        Instance = new ManagedUnmanagedPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapManagedUnmanaged<A, B>;
}

class ManagedStructPull<A, B> : PullGen<A, B>
    where A : class
    where B : unmanaged
{
    static ManagedStructPull() =>
        Instance = new ManagedStructPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapManagedStruct<A, B>;
}


class UnmanagedManagedPull<A, B> : PullGen<A, B>
    where A : unmanaged
    where B : class
{
    static UnmanagedManagedPull() =>
        Instance = new UnmanagedManagedPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapUnmanagedManaged<A, B>;
}

class UnmanagedUnmanagedPull<A, B> : PullGen<A, B>
    where A : unmanaged
    where B : unmanaged
{
    static UnmanagedUnmanagedPull() =>
        Instance = new UnmanagedUnmanagedPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapUnmanagedUnmanaged<A, B>;
}

class UnmanagedStructPull<A, B> : PullGen<A, B>
    where A : unmanaged
    where B : unmanaged
{
    static UnmanagedStructPull() =>
        Instance = new UnmanagedStructPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapUnmanagedStruct<A, B>;
}


class StructManagedPull<A, B> : PullGen<A, B>
    where A : struct
    where B : class
{
    static StructManagedPull() =>
        Instance = new StructManagedPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapStructManaged<A, B>;
}

class StructUnmanagedPull<A, B> : PullGen<A, B>
    where A : struct
    where B : unmanaged
{
    static StructUnmanagedPull() =>
        Instance = new StructUnmanagedPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapStructUnmanaged<A, B>;
}

class StructStructPull<A, B> : PullGen<A, B>
    where A : struct
    where B : unmanaged
{
    static StructStructPull() =>
        Instance = new StructStructPull<A, B>();

    public override unsafe IterOp MapImpl =>
        &Pull.mapStructStruct<A, B>;
}
