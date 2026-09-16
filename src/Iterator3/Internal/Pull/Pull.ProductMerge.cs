#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    [MethodImpl(Optimisations.Default)]
    public static int productMerge<A, B>(ref StackFrame frame) =>

        pop<B>(ref frame, out var b) &&
        dup<A>(ref frame)            &&
        push(ref frame, in b)
            ? PullState.Continue
            : PullState.Void;
}