using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class ByteStackExtensions
{
    extension(in ByteStack stack)
    {
        public ref ByteStackMutable Ref
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<ByteStack, ByteStackMutable>(ref Unsafe.AsRef(in stack));
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
    }
}