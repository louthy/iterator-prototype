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

        [MethodImpl(Optimisations.InliningOnly)]
        public unsafe bool Add(
            IterOp f,
            int varBytesIn,
            int varBytesOut,
            int varObjsIn,
            int varObjsOut,
            OpReturn @return) =>
            ops.Ref.Add(f, varBytesIn, varBytesOut, varObjsIn, varObjsOut, @return);

        [MethodImpl(Optimisations.InliningOnly)]
        public unsafe bool Prepend(
            IterOp f,
            int varBytesIn,
            int varBytesOut,
            int varObjsIn,
            int varObjsOut,
            OpReturn @return) =>
            ops.Ref.Prepend(f, varBytesIn, varBytesOut, varObjsIn, varObjsOut, @return);
    }
}