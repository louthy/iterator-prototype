using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class OpsExtensions
{
    extension(in Ops ops)
    {
        public ref OpsMutable Ref
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<Ops, OpsMutable>(ref Unsafe.AsRef(in ops));
        }
    }
}