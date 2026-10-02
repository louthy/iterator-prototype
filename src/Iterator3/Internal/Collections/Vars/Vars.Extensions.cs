using System.Runtime.CompilerServices;

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(in Vars vars)
    {
        public ref VarsMutable Ref
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => ref Unsafe.As<Vars, VarsMutable>(ref Unsafe.AsRef(in vars));
        }
    }
}