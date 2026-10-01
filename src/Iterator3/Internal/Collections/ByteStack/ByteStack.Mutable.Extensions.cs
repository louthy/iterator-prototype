using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class ByteStackMutableExtensions
{
    extension(ref ByteStackMutable stack)
    {
        public Span<byte> Values
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in stack.Stack), stack.Count);
        }
    
        public Span<byte> AllValues
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in stack.Stack), ByteStack.Capacity);
        }
    
        [MethodImpl(Optimisations.InliningOnly)]
        public Span<byte> NextValues(int amount) =>
            amount + stack.Count > ByteStack.Capacity
                ? throw new InvalidOperationException("Stack overflow")
                : MemoryMarshal.CreateSpan(ref Unsafe.AddByteOffset(ref stack.Stack, stack.Count), amount);        
        
        [MethodImpl(Optimisations.InliningOnly)]
        public void Add(in ByteStack rhs) =>
            rhs.Values.CopyTo(stack.NextValues(rhs.Count));

        [MethodImpl(Optimisations.InliningOnly)]
        public bool PopToTop(int top)
        {
            stack.Count = Math.Min(stack.Count, top);
            return true;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A>()
        {
            var sizeOf = Unsafe.SizeOf<A>();
            stack.Count -= sizeOf;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A>(out A value)
        {
            var sizeOf = Unsafe.SizeOf<A>();
            stack.Count -= sizeOf;
            value = Unsafe.As<byte, A>(ref Unsafe.AddByteOffset(ref stack.Stack, stack.Count));
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Dup<A>()
        {
            var     sizeOf = Unsafe.SizeOf<A>();
            var     last   = stack.Count - sizeOf;
            var     next   = stack.Count;
            ref var src    = ref Unsafe.AddByteOffset(ref stack.Stack, last);
            ref var dst    = ref Unsafe.AddByteOffset(ref stack.Stack, next);
            Unsafe.CopyBlock(ref dst, ref src, (uint)sizeOf);

            stack.Count += sizeOf;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Push<A>(in A value)
            where A : unmanaged
        {
            var     sizeOf = Unsafe.SizeOf<A>();
            ref var top    = ref Unsafe.AddByteOffset(ref stack.Stack, stack.Count);
            ref var entry  = ref Unsafe.As<byte, A>(ref top);
            entry = value;
            stack.Count += sizeOf;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Prepend<A>(in A value)
            where A : unmanaged
        {
            var     sizeOf = Unsafe.SizeOf<A>();
            ref var src    = ref stack.Stack;
            ref var dest   = ref Unsafe.AddByteOffset(ref src, sizeOf);

            Unsafe.CopyBlock(ref dest, ref src, (uint)sizeOf);

            ref var entry = ref Unsafe.As<byte, A>(ref src);
            entry = value;
            stack.Count += sizeOf;
        }
    }
}