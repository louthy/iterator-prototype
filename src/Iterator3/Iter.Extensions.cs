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
}