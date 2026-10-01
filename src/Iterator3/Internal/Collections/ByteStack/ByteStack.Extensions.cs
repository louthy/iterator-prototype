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

        ref byte Bytes
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.AddByteOffset(ref Unsafe.As<ByteStack, byte>(ref Unsafe.AsRef(in stack)), 4);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Add(in ByteStack rhs) =>
            stack.Ref.Add(in rhs);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool PopToTop(int top) =>
            stack.Ref.PopToTop(top);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A>()
            where A : unmanaged =>
            stack.Ref.Pop<A>();

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A>(out A value)
            where A : unmanaged =>
            stack.Ref.Pop(out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Dup<A>()
            where A : unmanaged =>
            stack.Ref.Dup<A>();

        [MethodImpl(Optimisations.InliningOnly)]
        public void Push<A>(in A value)
            where A : unmanaged =>
            stack.Ref.Push(in value);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Prepend<A>(in A value)
            where A : unmanaged =>
            stack.Ref.Prepend(in value);

        public ReadOnlySpan<byte> Values
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateReadOnlySpan(ref stack.Ref.Stack, stack.Count);
        }

        public ReadOnlySpan<byte> AllValues
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateReadOnlySpan(ref stack.Ref.Stack, ByteStack.Capacity);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Peek<A>(out A value)
            where A : unmanaged
        {
            ref var top = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in stack.Bytes), stack.Count - Unsafe.SizeOf<A>());
            value = Unsafe.As<byte, A>(ref top);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAt<A>()
            where A : unmanaged
        {
            ref var top = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in stack.Bytes), stack.Count - Unsafe.SizeOf<A>());
            return ref Unsafe.As<byte, A>(ref top);
        }
    }
}