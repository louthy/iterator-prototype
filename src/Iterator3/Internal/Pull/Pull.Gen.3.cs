#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

abstract class PullGen<A, B, C>
{
    internal static PullGen<A, B, C> Instance;

    [MethodImpl(Optimisations.Default)]
    static PullGen()
    {
        switch (Ty<A>.Flavour, Ty<B>.Flavour, Ty<C>.Flavour)
        {
            case (TyFlavour.Unmanaged, TyFlavour.Unmanaged, TyFlavour.Managed):
            {
                var type = typeof(UnmanagedUnmanagedManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            case (TyFlavour.Unmanaged, TyFlavour.Managed, TyFlavour.Managed):
            {
                var type = typeof(UnmanagedManagedManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Unmanaged, TyFlavour.Struct, TyFlavour.Managed):
            {
                var type = typeof(UnmanagedStructManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Unmanaged, TyFlavour.Managed):
            {
                var type = typeof(ManagedUnmanagedManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Managed, TyFlavour.Managed):
            {
                var type = typeof(ManagedManagedManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Struct, TyFlavour.Managed):
            {
                var type = typeof(ManagedStructManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Unmanaged, TyFlavour.Managed):
            {
                var type = typeof(StructUnmanagedManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Managed, TyFlavour.Managed):
            {
                var type = typeof(StructManagedManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Struct, TyFlavour.Managed):
            {
                var type = typeof(StructStructManagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            
            case (TyFlavour.Unmanaged, TyFlavour.Unmanaged, TyFlavour.Unmanaged):
            {
                var type = typeof(UnmanagedUnmanagedUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            case (TyFlavour.Unmanaged, TyFlavour.Managed, TyFlavour.Unmanaged):
            {
                var type = typeof(UnmanagedManagedUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Unmanaged, TyFlavour.Struct, TyFlavour.Unmanaged):
            {
                var type = typeof(UnmanagedStructUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Unmanaged, TyFlavour.Unmanaged):
            {
                var type = typeof(ManagedUnmanagedUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Managed, TyFlavour.Unmanaged):
            {
                var type = typeof(ManagedManagedUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Struct, TyFlavour.Unmanaged):
            {
                var type = typeof(ManagedStructUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Unmanaged, TyFlavour.Unmanaged):
            {
                var type = typeof(StructUnmanagedUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Managed, TyFlavour.Unmanaged):
            {
                var type = typeof(StructManagedUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Struct, TyFlavour.Unmanaged):
            {
                var type = typeof(StructStructUnmanagedPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            
            case (TyFlavour.Unmanaged, TyFlavour.Unmanaged, TyFlavour.Struct):
            {
                var type = typeof(UnmanagedUnmanagedStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }

            case (TyFlavour.Unmanaged, TyFlavour.Managed, TyFlavour.Struct):
            {
                var type = typeof(UnmanagedManagedStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Unmanaged, TyFlavour.Struct, TyFlavour.Struct):
            {
                var type = typeof(UnmanagedStructStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Unmanaged, TyFlavour.Struct):
            {
                var type = typeof(ManagedUnmanagedStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Managed, TyFlavour.Struct):
            {
                var type = typeof(ManagedManagedStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Managed, TyFlavour.Struct, TyFlavour.Struct):
            {
                var type = typeof(ManagedStructStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Unmanaged, TyFlavour.Struct):
            {
                var type = typeof(StructUnmanagedStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Managed, TyFlavour.Struct):
            {
                var type = typeof(StructManagedStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            case (TyFlavour.Struct, TyFlavour.Struct, TyFlavour.Struct):
            {
                var type = typeof(StructStructStructPull<,,>).MakeGenericType(typeof(A), typeof(B), typeof(C));
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                break;
            }
            
            
            default:
                throw new Exception("We have a type that apparently isn't managed, unmanaged, or a value-type!");

        }
    }

    public static unsafe IterOp bimap => 
        Instance.BiMapImpl;

    public abstract unsafe IterOp BiMapImpl { get; }
}

class ManagedManagedManagedPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : class
    where C : class
{
    static ManagedManagedManagedPull() =>
        Instance = new ManagedManagedManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedManagedManaged<A, B, C>;
}

class ManagedUnmanagedManagedPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : unmanaged
    where C : class
{
    static ManagedUnmanagedManagedPull() =>
        Instance = new ManagedUnmanagedManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedUnmanagedManaged<A, B, C>;
}

class ManagedStructManagedPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : unmanaged
    where C : class
{
    static ManagedStructManagedPull() =>
        Instance = new ManagedStructManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedStructManaged<A, B, C>;
}


class UnmanagedManagedManagedPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : class
    where C : class
{
    static UnmanagedManagedManagedPull() =>
        Instance = new UnmanagedManagedManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedManagedManaged<A, B, C>;
}

class UnmanagedUnmanagedManagedPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : unmanaged
    where C : class
{
    static UnmanagedUnmanagedManagedPull() =>
        Instance = new UnmanagedUnmanagedManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedUnmanagedManaged<A, B, C>;
}

class UnmanagedStructManagedPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : unmanaged
    where C : class
{
    static UnmanagedStructManagedPull() =>
        Instance = new UnmanagedStructManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedStructManaged<A, B, C>;
}


class StructManagedManagedPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : class
    where C : class
{
    static StructManagedManagedPull() =>
        Instance = new StructManagedManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructManagedManaged<A, B, C>;
}

class StructUnmanagedManagedPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : unmanaged
    where C : class
{
    static StructUnmanagedManagedPull() =>
        Instance = new StructUnmanagedManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructUnmanagedManaged<A, B, C>;
}

class StructStructManagedPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : unmanaged
    where C : class
{
    static StructStructManagedPull() =>
        Instance = new StructStructManagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructStructManaged<A, B, C>;
}




class ManagedManagedUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : class
    where C : unmanaged
{
    static ManagedManagedUnmanagedPull() =>
        Instance = new ManagedManagedUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedManagedUnmanaged<A, B, C>;
}

class ManagedUnmanagedUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : unmanaged
    where C : unmanaged
{
    static ManagedUnmanagedUnmanagedPull() =>
        Instance = new ManagedUnmanagedUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedUnmanagedUnmanaged<A, B, C>;
}

class ManagedStructUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : unmanaged
    where C : unmanaged
{
    static ManagedStructUnmanagedPull() =>
        Instance = new ManagedStructUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedStructUnmanaged<A, B, C>;
}


class UnmanagedManagedUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : class
    where C : unmanaged
{
    static UnmanagedManagedUnmanagedPull() =>
        Instance = new UnmanagedManagedUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedManagedUnmanaged<A, B, C>;
}

class UnmanagedUnmanagedUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : unmanaged
    where C : unmanaged
{
    static UnmanagedUnmanagedUnmanagedPull() =>
        Instance = new UnmanagedUnmanagedUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedUnmanagedUnmanaged<A, B, C>;
}

class UnmanagedStructUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : unmanaged
    where C : unmanaged
{
    static UnmanagedStructUnmanagedPull() =>
        Instance = new UnmanagedStructUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedStructUnmanaged<A, B, C>;
}


class StructManagedUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : class
    where C : unmanaged
{
    static StructManagedUnmanagedPull() =>
        Instance = new StructManagedUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructManagedUnmanaged<A, B, C>;
}

class StructUnmanagedUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : unmanaged
    where C : unmanaged
{
    static StructUnmanagedUnmanagedPull() =>
        Instance = new StructUnmanagedUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructUnmanagedUnmanaged<A, B, C>;
}

class StructStructUnmanagedPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : unmanaged
    where C : unmanaged
{
    static StructStructUnmanagedPull() =>
        Instance = new StructStructUnmanagedPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructStructUnmanaged<A, B, C>;
}




class ManagedManagedStructPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : class
    where C : struct
{
    static ManagedManagedStructPull() =>
        Instance = new ManagedManagedStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedManagedStruct<A, B, C>;
}

class ManagedUnmanagedStructPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : unmanaged
    where C : struct
{
    static ManagedUnmanagedStructPull() =>
        Instance = new ManagedUnmanagedStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedUnmanagedStruct<A, B, C>;
}

class ManagedStructStructPull<A, B, C> : PullGen<A, B, C>
    where A : class
    where B : unmanaged
    where C : struct
{
    static ManagedStructStructPull() =>
        Instance = new ManagedStructStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapManagedStructStruct<A, B, C>;
}


class UnmanagedManagedStructPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : class
    where C : struct
{
    static UnmanagedManagedStructPull() =>
        Instance = new UnmanagedManagedStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedManagedStruct<A, B, C>;
}

class UnmanagedUnmanagedStructPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : unmanaged
    where C : struct
{
    static UnmanagedUnmanagedStructPull() =>
        Instance = new UnmanagedUnmanagedStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedUnmanagedStruct<A, B, C>;
}

class UnmanagedStructStructPull<A, B, C> : PullGen<A, B, C>
    where A : unmanaged
    where B : unmanaged
    where C : struct
{
    static UnmanagedStructStructPull() =>
        Instance = new UnmanagedStructStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapUnmanagedStructStruct<A, B, C>;
}


class StructManagedStructPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : class
    where C : struct
{
    static StructManagedStructPull() =>
        Instance = new StructManagedStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructManagedStruct<A, B, C>;
}

class StructUnmanagedStructPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : unmanaged
    where C : struct
{
    static StructUnmanagedStructPull() =>
        Instance = new StructUnmanagedStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructUnmanagedStruct<A, B, C>;
}

class StructStructStructPull<A, B, C> : PullGen<A, B, C>
    where A : struct
    where B : unmanaged
    where C : struct
{
    static StructStructStructPull() =>
        Instance = new StructStructStructPull<A, B, C>();

    public override unsafe IterOp BiMapImpl =>
        &Pull.bimapStructStructStruct<A, B, C>;
}
