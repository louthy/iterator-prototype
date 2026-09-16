using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(ref Vars vars)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A>(out A value) =>
            VarsGen<A>.Instance.PopImpl(ref vars, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B>(out A value1, out B value2)
        {
            if (vars.Pop(out value2) && vars.Pop(out value1)) return true;
            value1 = default!;
            return false;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A>() =>
            VarsGen<A>.Instance.PopImpl(ref vars);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B>() =>
            vars.Pop<B>() &&
            vars.Pop<A>();        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Push<A>(in A value) =>
            VarsGen<A>.Instance.PushImpl(ref vars, in value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Peek<A>(out A value) =>
            VarsGen<A>.Instance.PeekImpl(ref vars, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Peek<A, B>(out A value1, out B value2)
        {
            if (vars.Pop(out value2))
            {
                return vars.Pop(out value1) 
                           ? vars.Push(in value2) 
                           : vars.Push(in value2) && false;
            }
            else
            {
                value1 = default!;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAt<A>() =>
            ref VarsGen<A>.Instance.PeekAtImpl(ref vars);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool PeekAt<A, B>()
        {
            throw new NotSupportedException("Not supported for tuples, because they're not at a single region");
        }        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Push<A, B>(in (A, B) pair) =>
            vars.Push(in pair.Item1) &&
            vars.Push(in pair.Item2);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Push<A, B>(in A first, in B second) =>
            vars.Push(in first) &&
            vars.Push(in second);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Dup<A>() =>
            VarsGen<A>.Instance.DupImpl(ref vars);
        
    }
}
