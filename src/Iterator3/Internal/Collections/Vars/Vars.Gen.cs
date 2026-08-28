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

    public abstract bool PopImpl(ref Vars frame, out A top);
    public abstract bool PopImpl(ref Vars frame);
    public abstract bool PrependImpl(ref Vars frame, in A top);
    public abstract bool PushImpl(ref Vars frame, in A top);
    public abstract bool PeekImpl(ref Vars frame, out A top);    
}

class ManagedVars<A> : VarsGen<A>
    where A : class
{
    static ManagedVars() =>
        Instance = new ManagedVars<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack, out A top) =>
        stack.objs.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack) =>
        stack.objs.Pop();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars stack, in A top) =>
        stack.objs.Push(top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref Vars stack, in A top) =>
        stack.objs.Prepend(top);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars stack, out A top) =>
        stack.objs.Peek(out top);
}

class StructVars<A> : VarsGen<A>
    where A : struct
{
    // TODO: Consider if we can do something cunning to allocate and free the Box type
    //       Probably from an object-pool, but also see there's a way of freeing the objects
    //       on Pop operations.
    //
    // TODO: The major issue would be somebody doing default(ObjStack) to wipe the objects.
    
    static StructVars() =>
        Instance = new StructVars<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack, out A top)
    {
        if (stack.objs.Pop<Box<A>>(out var box))
        {
            top = box.Value;
            return true;
        }
        else
        {
            top = default!;
            return false;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack) =>
        stack.objs.Pop();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars stack, in A top) =>
        stack.objs.Push(new Box<A>(top));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref Vars stack, in A top) =>
        stack.objs.Prepend(new Box<A>(top));

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars stack, out A top)
    {
        if (stack.objs.Peek<Box<A>>(out var box))
        {
            top = box.Value;
            return true;
        }
        else
        {
            top = default!;
            return false;
        }
    }
}

class UnmanagedVars<A> : VarsGen<A>
    where A : unmanaged
{
    static UnmanagedVars() =>
        Instance = new UnmanagedVars<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack, out A top) =>
        stack.values.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref Vars stack) =>
        stack.values.Pop<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref Vars stack, in A top) =>
        stack.values.Push(top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref Vars stack, in A top) =>
        stack.values.Prepend(top);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref Vars stack, out A top) =>
        stack.values.Peek(out top);
}