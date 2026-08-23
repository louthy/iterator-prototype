namespace IteratorPrototype.Internal;

abstract class Op
{
    public abstract bool Run(ref StackFrame frame);
}

abstract class Op<A> : Op
{
}


abstract class Op<A, B> : Op<B>
{
}
