using System.Runtime.CompilerServices;
using IteratorPrototype.Internal;
using IteratorPrototype.Internal.Collections;
using IteratorPrototype.Internal.Source.Factories;
using IteratorPrototype.Internal.Sources;

namespace IteratorPrototype;

public static class IteratorExtensions2
{
    extension<A>(ref Iterator2<A> ta)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public unsafe bool TryGetValue(out A head, out Iterator2<A> tail)
        {
            // Copy
            // Consider better ways, but remember, `ta` might also be `tail`, which means doing `tail = default` to
            // initialise it will overwrite `ta`.
            tail = ta;

            ref var source = ref Unsafe.AsRef(in tail.source);
            ref var ops    = ref Unsafe.AsRef(in tail.ops);
            ref var objs   = ref Unsafe.AsRef(in tail.objs);
            ref var values = ref Unsafe.AsRef(in tail.values);
            var     frame  = new StackFrame(ref source, ref objs, ref values);

            while (source is not null)
            {
                if(!source.Run(ref frame)) continue;
                
                var hasValue = true;
                while (ops.NextPC(out var op))
                {
                    if (op.Run(ref frame)) continue;
                    hasValue = false;
                    break;
                }
                ops.ResetPC();
                    
                if(hasValue)
                {
                    ValueStack<A>.Pop(ref frame, out head);
                    return true;
                }
            }
            head = default!;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Prepend(A head)
        {
            Iterator2<A> iter = default;
            ta.CopyTo(ref iter);
            ref var s1 = ref Unsafe.AsRef(in iter.source);
            s1 = ((IteratorSource<A>?)iter.source)?.Prepend(head);
            return iter;
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Iterator2<A> Map<B>(Func<A, B> f)
        {
            Iterator2<B> iter = default;
            ref var      tb   = ref Unsafe.As<Iterator2<A>, Iterator2<B>>(ref ta); 
            
            tb.CopyTo(ref iter);

            ref var opStack = ref Unsafe.AsRef(in iter.ops);
            ref var objs    = ref Unsafe.AsRef(in iter.objs);
            ref var values  = ref Unsafe.AsRef(in iter.values);

            opStack.Add();
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
        internal void SetSource(in IteratorSource? source)
        {
            ref var s = ref Unsafe.AsRef(in ta.source);
            s = source;
        }
    }
}