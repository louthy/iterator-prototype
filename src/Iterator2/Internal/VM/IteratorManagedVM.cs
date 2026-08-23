using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Internal.VM;

class IteratorManagedVM<T, IS, A>(IteratorVM parent) : IteratorManagedVM<A>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
{
    public static readonly IteratorVM Instance = 
        new IteratorManagedVM<T, IS, A>(EmptyIteratorManagedVM<A>.Instance);

    public override IteratorVM Parent
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
            frame.Objs.Push(head);
            
            while (opsFrame.NextPC<A>(out var op) && op.Run(ref frame))
                /* Left empty on purpose */;
            
            opsFrame.ResetPC();
            return true;
        }
        else
        {
            frame.Ops.Pop();            // Remove the `ops` stack-frame
            frame.VM = frame.VM.Parent; // Look for an operation to call back to
            return frame.VM.Run(ref frame);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorVM<A> Prepend(A value) =>
        new ConsManagedVM<A>(value, this);
}
