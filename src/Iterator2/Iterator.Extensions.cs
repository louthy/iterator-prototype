using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;
using IteratorPrototype.Internal.Sources;

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
            
            ref var s1 = ref Unsafe.AsRef(in tail.source);

            var frame = new StackFrame(
                ref s1,
                ref Unsafe.AsRef(in tail.ops),
                ref Unsafe.AsRef(in tail.objs),
                ref Unsafe.AsRef(in tail.values));

            ref var s2 = ref Unsafe.As<IteratorSource, IteratorSource<A>>(ref s1);
            return s2.Run(ref frame, out head);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Prepend(A head)
        {
            Iterator2<A> iter = default;
            ta.CopyTo(ref iter);
            ref var s1 = ref Unsafe.AsRef(in iter.source);
            s1 = ((IteratorSource<A>)iter.source).Prepend(head);
            return iter;
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Map<B>(Func<A, B> f)
        {
            Iterator2<B> iter = default;
            ta.CopyTo(ref iter);
            
            ref var ops = ref Unsafe.AsRef(in iter.ops);
            ops.Add(new MapOp<A, B>(f));
        }*/
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(ref Iterator2<A> other)
        {
            other.SetSource(in ta.source);
            ta.ops.CopyTo(ref Unsafe.AsRef(in other.ops));
            ta.objs.CopyTo(ref Unsafe.AsRef(in other.objs));
            ta.values.CopyTo(ref Unsafe.AsRef(in other.values));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        internal void SetSource(in IteratorSource source)
        {
            ref var s = ref Unsafe.AsRef(in ta.source);
            s = source;
        }
    }
}