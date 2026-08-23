using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
record SingletonSource<A>(A Head, IteratorSource Next) : IteratorSource<A>(Next)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override unsafe bool Run(ref StackFrame frame)
    {
        ValueStack<A>.Push(ref frame, Head);
        frame.Source = Next;
        return false;
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this); 
}
