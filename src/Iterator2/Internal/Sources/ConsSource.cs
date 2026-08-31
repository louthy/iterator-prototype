using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
sealed record ConsSource<A>(A Head, IteratorSource? Next) : IteratorSource<A>(Next)
{
    [MethodImpl(Optimisations.Default)]
    public override bool Run(ref StackFrame stack)
    {
        ValueStack<A>.Push(ref stack, Head);
        stack.frame.SetSource(Next);
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this);
}
