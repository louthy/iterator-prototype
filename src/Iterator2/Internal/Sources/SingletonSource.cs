using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;

namespace IteratorPrototype.Internal.Sources;

record SingletonSource<A>(A Head, IteratorSource Next) : IteratorSource<A>(Next)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        unsafe
        {
            ValueStack<A>.Push(ref frame, Head);
            frame.Source = Next;
            return false;
        }
    }

    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this); 
}
