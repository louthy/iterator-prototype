using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class TopsExtensions
{
    extension(in Tops tops)
    {
        public ref TopsMutable Ref
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<Tops, TopsMutable>(ref Unsafe.AsRef(in tops));
        }
    }
}