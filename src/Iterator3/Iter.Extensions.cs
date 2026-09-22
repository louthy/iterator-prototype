using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal;

namespace IteratorPrototype.Iterator3;

public static class IterExtensions
{
    extension<A>(in Iter<A> self)
    {
        [MethodImpl(Optimisations.Default)]
        internal StackFrame Next(out Iter<A> next) =>
            Iter<A>.Next(in self, out next);
        
        [MethodImpl(Optimisations.Default)]
        internal StackFrame Next<B>(out Iter<B> next) =>
            Iter<A>.Next(in self, out next);
    }
    
    extension<A>(ref Iter<A> self)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        internal bool TryGetValue(out A head) =>
            Iter<A>.TryRef(ref self, out head);
    }
}