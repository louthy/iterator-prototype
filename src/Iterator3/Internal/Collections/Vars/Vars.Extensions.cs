using System.Numerics;
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(ref Vars vars)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Pop<A>(out A value) =>
            VarsGen<A>.Instance.PopImpl(ref vars, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Pop<A>() =>
            VarsGen<A>.Instance.PopImpl(ref vars);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Push<A>(in A value) =>
            VarsGen<A>.Instance.PushImpl(ref vars, in value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Peek<A>(out A value) =>
            VarsGen<A>.Instance.PeekImpl(ref vars, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref A PeekAt<A>() =>
            ref VarsGen<A>.Instance.PeekAtImpl(ref vars);
    }
}
