using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Internal.Sources;

record IterableManagedToManagedSource<T, IS, A, B>(IteratorSource? Next) : IteratorManagedSource<B>(Next)
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
    where A : class
    where B : class
{
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
            frame.Ops.Pop();                     // Remove the `ops` stack-frame
            frame.Source = frame.Source?.Next; // Look for an operation to call back to
            return frame.Source?.Run(ref frame) ?? false;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override IteratorSource<B> Prepend(B value) =>
        new ConsManagedSource<B>(value, this);
}
