using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;
using LanguageExt.Traits;

namespace IteratorPrototype.Internal.Sources;

[SkipLocalsInit]
record IterableSource<T, IS, A>(IteratorSource? Next) : IteratorSource<A>(Next)
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<A> Instance = 
        new IterableSource<T, IS, A>(new EmptyIteratorSource<A>(null!));
    
    [MethodImpl(Optimisations.Default)]
    public override bool Run(ref StackFrame stack)
    {
        ref var frame = ref stack.frame;
        ref var ta    = ref frame.objs.Peek<K<T, A>>();
        ref var space = ref frame.values.Peek<IS>();
        if (T.StepImmutable(in ta, in space, out var head, out space))
        //if (T.Next(in ta, ref space, out var head))
        {
            ValueStack<A>.Push(ref frame, in head);
            return true;
        }
        else
        {
            frame.SetSource(Next);
            return false;
        }
    }
    
    [MethodImpl(Optimisations.Default)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this);
}
