using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class ByteStackExtensions
{
    extension(in ByteStack stack)
    {
        ref ByteStackMutable Ref
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<ByteStack, ByteStackMutable>(ref Unsafe.AsRef(in stack));
        }

        public ReadOnlySpan<byte> Values
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateReadOnlySpan(ref stack.Ref.Stack, (int)stack.Count);
        }

        public ReadOnlySpan<byte> AllValues
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateReadOnlySpan(ref stack.Ref.Stack, ByteStack.Capacity);
        }
    }
}