using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;
using StackFrame = IteratorPrototype.Iterator3.Internal.StackFrame;

namespace IteratorPrototype.Iterator3;

static partial class Pull
{
    public static unsafe int bimap1<A, B, C>(ref StackFrame frame) =>
        PullGen<C>.bimap1<A, B>()(ref frame);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapManaged1<A, B, C>(ref StackFrame frame)
        where C : class 
    {
        var f = PullManaged.arg1<Func<A, B, C>>(ref frame);
        return pop<(A, B)>(ref frame, out var pair) &&
               PullManaged.@return(ref frame, f(pair.Item1, pair.Item2))
                   ? PullState.Continue
                   : PullState.Void;
    }
        
    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapUnmanaged1<A, B, C>(ref StackFrame frame)
        where C : unmanaged 
    {
        var f = PullManaged.arg1<Func<A, B, C>>(ref frame);
        return pop<(A, B)>(ref frame, out var pair) &&
               PullUnmanaged.@return(ref frame, f(pair.Item1, pair.Item2))
                   ? PullState.Continue
                   : PullState.Void;
    }
            
    [MethodImpl(Optimisations.InliningOnly)]
    public static int bimapStruct1<A, B, C>(ref StackFrame frame)
        where C : struct 
    {
        var f = PullManaged.arg1<Func<A, B, C>>(ref frame);
        return pop<(A, B)>(ref frame, out var pair) &&
               PullStruct.@return(ref frame, f(pair.Item1, pair.Item2))
                   ? PullState.Continue
                   : PullState.Void;
    }
}