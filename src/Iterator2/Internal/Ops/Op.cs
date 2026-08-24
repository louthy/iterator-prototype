using IteratorPrototype.Internal.Collections;

namespace IteratorPrototype.Internal;

abstract class Op
{
    public abstract bool Run(ref OpFrame frame);
}

abstract class Op<A> : Op
{
}


abstract class Op<A, B> : Op<B>
{
}
