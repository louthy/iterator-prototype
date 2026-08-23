using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Source.Factories;

static class ValueStack<A>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static ValueStack()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var type = typeof(UnmanagedValueStack<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
        else
        {
            var type = typeof(ManagedValueStack<>).MakeGenericType(typeof(A));
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }

    public static unsafe delegate*<ref StackFrame, out A, void> Pop;
    public static unsafe delegate*<ref StackFrame, in A, void> Push;

    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the ValueStack instance");    
}

static class ManagedValueStack<A>
    where A : class
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static unsafe ManagedValueStack()
    {
        ValueStack<A>.Pop = &PopImpl;
        ValueStack<A>.Push = &PushImpl;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static void PopImpl(ref StackFrame frame, out A value)
    {
        value = frame.Objs.Peek<A>();
        frame.Objs.Pop();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static void PushImpl(ref StackFrame frame, in A value) =>
        frame.Objs.Push(value);
}

static class UnmanagedValueStack<A>
    where A : unmanaged
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static unsafe UnmanagedValueStack()
    {
        ValueStack<A>.Pop = &PopImpl;
        ValueStack<A>.Push = &PushImpl;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static void PopImpl(ref StackFrame frame, out A value)
    {
        value = frame.Values.Peek<A>();
        frame.Values.Pop();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    static void PushImpl(ref StackFrame frame, in A value) =>
        frame.Values.Push(value);
}