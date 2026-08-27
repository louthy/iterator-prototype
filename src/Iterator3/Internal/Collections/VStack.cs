#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly struct VStack
{
    internal readonly ObjStack objs;
    internal readonly ByteStack values;
    public static readonly VStack Empty = new ();
}

static class VStackExtensions
{
    extension(ref VStack stack)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Pop<A>(out A top)
        {
            if (typeof(A) == typeof(LE.Unit))
            {
                top = default!;
                return stack.values.Pop<byte>(out _);
            }
            else
            {
                return VStackGen<A>.Instance.PopImpl(ref stack, out top);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Pop<A>()
        {
            if (typeof(A) == typeof(LE.Unit))
            {
                return stack.values.Pop<byte>(out _);
            }
            else
            {
                return VStackGen<A>.Instance.PopImpl(ref stack);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Swap<A>(in A value) =>
            stack.Pop<A>() &&
            stack.Push(in value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool PopUnit() =>
            stack.values.Pop<byte>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Prepend<A>(in A top) =>
            VStackGen<A>.Instance.PrependImpl(ref stack, in top);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool AddArg<A>(in A top) =>
            stack.Prepend(in top);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Push<A>(in A top) =>
            typeof(A) == typeof(LE.Unit)
                ? stack.values.Push<byte>(0)
                : VStackGen<A>.Instance.PushImpl(ref stack, in top);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool PushUnit() =>
            stack.values.Push<byte>(0);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Peek<A>(out A top)
        {
            if (typeof(A) == typeof(LE.Unit))
            {
                top = default!;
                return stack.values.Peek<byte>(out _);
            }
            else
            {
                return VStackGen<A>.Instance.PeekImpl(ref stack, out top);
            }
        }
    }
}

abstract class VStackGen<A>
{
    public static VStackGen<A> Instance;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static VStackGen()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedVStack<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            var type = typeof(ManagedVStack<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }

    public abstract bool PopImpl(ref VStack frame, out A top);
    public abstract bool PopImpl(ref VStack frame);
    public abstract bool PrependImpl(ref VStack frame, in A top);
    public abstract bool PushImpl(ref VStack frame, in A top);
    public abstract bool PeekImpl(ref VStack frame, out A top);    
}

class ManagedVStack<A> : VStackGen<A>
    where A : class
{
    static ManagedVStack() =>
        Instance = new ManagedVStack<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref VStack stack, out A top) =>
        stack.objs.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref VStack stack) =>
        stack.objs.Pop();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref VStack stack, in A top) =>
        stack.objs.Push(top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref VStack stack, in A top) =>
        stack.objs.Prepend(top);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref VStack stack, out A top) =>
        stack.objs.Peek(out top);
}

class UnmanagedVStack<A> : VStackGen<A>
    where A : unmanaged
{
    static UnmanagedVStack() =>
        Instance = new UnmanagedVStack<A>();

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref VStack stack, out A top) =>
        stack.values.Pop(out top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PopImpl(ref VStack stack) =>
        stack.values.Pop<A>();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PushImpl(ref VStack stack, in A top) =>
        stack.values.Push(top);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PrependImpl(ref VStack stack, in A top) =>
        stack.values.Prepend(top);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool PeekImpl(ref VStack stack, out A top) =>
        stack.values.Peek(out top);
}