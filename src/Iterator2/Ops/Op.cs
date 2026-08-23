using System.Runtime.CompilerServices;

namespace IteratorPrototype;

abstract class Op;

abstract class Op<A> : Op
{
    public abstract bool Run(ref StackFrame frame);
}
    