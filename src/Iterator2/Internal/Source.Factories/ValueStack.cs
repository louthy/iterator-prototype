using System.Reflection;
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Internal.Source.Factories;

static class ValueStack<A>
{
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
    static ManagedValueStack()
    {
        unsafe
        {
            ValueStack<A>.Pop = &PopImpl;
            ValueStack<A>.Push = &PushImpl;
        }
    }
    
    static void PopImpl(ref StackFrame frame, out A value)
    {
        value = frame.Objs.Peek<A>();
        frame.Objs.Pop();
    }
    
    static void PushImpl(ref StackFrame frame, in A value) =>
        frame.Objs.Push(value);
}

static class UnmanagedValueStack<A>
    where A : unmanaged
{
    static UnmanagedValueStack()
    {
        unsafe
        {
            ValueStack<A>.Pop = &PopImpl;
            ValueStack<A>.Push = &PushImpl;
        }
    }
    
    static void PopImpl(ref StackFrame frame, out A value)
    {
        value = frame.Values.Peek<A>();
        frame.Values.Pop();
    }
    
    static void PushImpl(ref StackFrame frame, in A value) =>
        frame.Values.Push(value);
}