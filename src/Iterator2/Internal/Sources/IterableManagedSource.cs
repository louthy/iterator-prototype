using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Internal.Sources;

class IterableManagedSource<T, IS, A>(IteratorSource parent) : IteratorManagedSource<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
{
    public static readonly IteratorSource Instance = 
        new IterableManagedSource<T, IS, A>(EmptyIteratorManagedSource<A>.Instance);

    public override IteratorSource Parent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => parent;
    }    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Run(ref StackFrame frame)
    {
        // Instruction stack frame
        ref var opsFrame = ref frame.Ops.AtTop;
        ref var ta       = ref Unsafe.As<object, K<T, A>>(ref Unsafe.AsRef(in opsFrame.Self)); 
        ref var space    = ref frame.Values.Peek<IS>();

        if (T.Next(in ta, ref space, out var head))
        {
            frame.Objs.Push(in head);
            
            while (opsFrame.NextPC(out var op) && op.Run(ref frame))
                /* Left empty on purpose */;
            
            opsFrame.ResetPC();
            return true;
        }
        else
        {
            frame.Ops.Pop();                    // Remove the `ops` stack-frame
            frame.Source = frame.Source.Parent; // Look for an operation to call back to
            return frame.Source.Run(ref frame);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<A> Prepend(A value) =>
        new ConsManagedSource<A>(value, this);
}
