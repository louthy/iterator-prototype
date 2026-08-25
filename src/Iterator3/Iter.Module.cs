using IteratorPrototype.Iterator3.Internal;
using IteratorPrototype.Iterator3.Internal.Collections;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

public static class Iter
{
    public static Iter<A> from<T, IS, A>(in K<T, A> ta)
        where T : Tr.IterableImmutable<T, IS>
        where IS : unmanaged
    {
        var ops   = new Ops();
        var vars  = new Vars();
        var state = new ByteStack();
        var frame = new StackFrame(ref ops, ref vars, ref state);
        
        Push.iterable<T, IS, A>(ref frame, ta);
        return new Iter<A>(in ops, in vars, in state);
    }
    
}
