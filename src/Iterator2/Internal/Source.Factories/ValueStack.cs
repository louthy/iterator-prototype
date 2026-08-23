namespace IteratorPrototype.Internal.Source.Factories;

abstract class ValueStack<A>
{
    public static readonly ValueStack<A> Instance;
    
    static ValueStack() =>
        Instance = MakeInstance();
    
    public abstract void Pop(ref StackFrame frame, out A value);
    public abstract void Push(ref StackFrame frame, in A value);

    public static ValueStack<A> MakeInstance()
    {
        if (Ty<A>.IsUnmanaged)
        {
            var ty = typeof(UnmanagedValueStack<>).MakeGenericType(typeof(A));
            var c  = ty.GetConstructors().First(c => c.GetParameters().Length == 0);
            var i  = c.Invoke([]);
            return (ValueStack<A>?)i ?? throw ShouldntHappenException;
        }
        else
        {
            var ty = typeof(ManagedValueStack<>).MakeGenericType(typeof(A));
            var c  = ty.GetConstructors().First(c => c.GetParameters().Length == 0);
            var i  = c.Invoke([]);
            return (ValueStack<A>?)i ?? throw ShouldntHappenException;
        }
    }

    static Exception ShouldntHappenException =>
        throw new InvalidOperationException("Factory failed to access the ValueStack instance");    
}

class ManagedValueStack<A> : ValueStack<A>
    where A : class
{
    public override void Pop(ref StackFrame frame, out A value)
    {
        value = frame.Objs.Peek<A>();
        frame.Objs.Pop();
    }
    
    public override void Push(ref StackFrame frame, in A value) =>
        frame.Objs.Push(value);
}

class UnmanagedValueStack<A> : ValueStack<A>
    where A : unmanaged
{
    public override void Pop(ref StackFrame frame, out A value)
    {
        value = frame.Values.Peek<A>();
        frame.Values.Pop();
    }
    
    public override void Push(ref StackFrame frame, in A value) =>
        frame.Values.Push(value);
}