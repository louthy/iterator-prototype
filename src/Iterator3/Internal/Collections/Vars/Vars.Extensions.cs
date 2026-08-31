using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(ref Vars vars)
    {
        [MethodImpl(Optimisations.Default)]
        public bool Pop<A>(out A value) =>
            VarsGen<A>.Instance.PopImpl(ref vars, out value);

        [MethodImpl(Optimisations.Default)]
        public bool Pop<A>() =>
            VarsGen<A>.Instance.PopImpl(ref vars);

        [MethodImpl(Optimisations.Default)]
        public bool Push<A>(in A value) =>
            VarsGen<A>.Instance.PushImpl(ref vars, in value);

        [MethodImpl(Optimisations.Default)]
        public bool Peek<A>(out A value) =>
            VarsGen<A>.Instance.PeekImpl(ref vars, out value);

        [MethodImpl(Optimisations.Default)]
        public ref A PeekAt<A>() =>
            ref VarsGen<A>.Instance.PeekAtImpl(ref vars);
    }
}
