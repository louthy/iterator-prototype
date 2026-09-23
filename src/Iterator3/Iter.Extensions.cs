using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

public static class IterExtensions
{
    extension<A>(in Iter<A> self)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        internal StackFrame Next(out Iter<A> next) =>
            Iter<A>.Next(in self, out next);
        
        [MethodImpl(Optimisations.InliningOnly)]
        internal StackFrame Next<B>(out Iter<B> next) =>
            Iter<A>.Next(in self, out next);
    }
    
    extension<A>(ref Iter<A> self)
    {
        /// <summary>
        /// This is a mutable version of `TryGetValue`, it will mutate `this` even though it's notionally
        /// an immutable type.
        /// </summary>
        [MethodImpl(Optimisations.InliningOnly)]
        internal bool TryGetValue(out A head) =>
            Iter<A>.TryRef(ref self, out head);
    }
}