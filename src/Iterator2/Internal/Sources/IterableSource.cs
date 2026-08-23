using System.Runtime.CompilerServices;
using IteratorPrototype.Internal.Source.Factories;
using LanguageExt.Traits;

namespace IteratorPrototype.Internal.Sources;

record IterableSource<T, IS, A>(IteratorSource? Next) : IteratorSource<A>(Next)
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public static readonly IteratorSource<A> Instance = 
        new IterableSource<T, IS, A>(new EmptyIteratorSource<A>(null!));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        ref var opsFrame = ref frame.Ops.AtTop;
        ref var ta       = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in opsFrame.Self)); 
        ref var space    = ref frame.Values.Peek<IS>();

        if (T.Next(in ta, ref space, out var head))
        {
            ValueStack<A>.Instance.Push(ref frame, in head);
            return true;
        }
        else
        {
            frame.Source = Next;
            return false;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsSource<A>(value, this);
}
