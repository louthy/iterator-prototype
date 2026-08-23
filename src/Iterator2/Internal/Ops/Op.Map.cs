using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal;

class Map_UnmanagedUnmanaged_Op<A, B>(Func<A, B> f) : Op<B>
    where A : unmanaged
    where B : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ref var x = ref frame.Values.Peek<A>();
        var y = f(x);
        frame.Values.Push(in y);
        frame.Values.Pop();
        return true;
    }
}

class Map_UnmanagedManaged_Op<A, B>(Func<A, B> f) : Op<B>
    where A : unmanaged
    where B : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ref var x = ref frame.Values.Peek<A>();
        var     y = f(x);
        frame.Objs.Push(in y);
        frame.Values.Pop();
        return true;
    }
}

class Map_ManagedUnmanaged_Op<A, B>(Func<A, B> f) : Op<B>
    where A : class
    where B : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ref var x = ref frame.Objs.Peek<A>();
        var     y = f(x);
        frame.Values.Push(in y);
        frame.Objs.Pop();
        return true;
    }
}

class Map_ManagedManaged_Op<A, B>(Func<A, B> f) : Op<B>
    where A : class
    where B : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ref var x = ref frame.Objs.Peek<A>();
        var     y = f(x);
        frame.Objs.Push(in y);
        frame.Objs.Pop();
        return true;
    }
}
