using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;
using IteratorPrototype.Internal.VM;

namespace IteratorPrototype;

public static class IteratorExtensions2
{
    extension<A>(ref Iterator2<A> ta)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool TryGetValue(out A head, out Iterator2<A> tail)
        {
            // Copy
            // Consider better ways, but remember, `ta` might also be `tail`, which means doing `tail = default` to
            // initialise it will overwrite `ta`.
            tail = ta; 
            
            ref var vm1 = ref Unsafe.AsRef(in tail.vm);

            var frame = new StackFrame(
                ref vm1,
                ref Unsafe.AsRef(in tail.ops),
                ref Unsafe.AsRef(in tail.objs),
                ref Unsafe.AsRef(in tail.values));

            ref var vm2 = ref Unsafe.As<IteratorVM, IteratorVM<A>>(ref vm1);
            return vm2.Run(ref frame, out head);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Prepend(A head)
        {
            Iterator2<A> iter = default;
            ta.CopyTo(ref iter);
            ref var vm1 = ref Unsafe.AsRef(in iter.vm);
            vm1 = ((IteratorVM<A>)iter.vm).Prepend(head);
            return iter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Map<B>(Func<A, B> f)
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(ref Iterator2<A> other)
        {
            other.SetVM(in ta.vm);
            ta.ops.CopyTo(ref Unsafe.AsRef(in other.ops));
            ta.objs.CopyTo(ref Unsafe.AsRef(in other.objs));
            ta.values.CopyTo(ref Unsafe.AsRef(in other.values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        internal void SetVM(in IteratorVM tvm)
        {
            ref var vm = ref Unsafe.AsRef(in ta.vm);
            vm = tvm;
        }
    }
}