using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsExtensions
{
    extension(ref Globals list)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add<A>(in A value) =>
            GlobalsGen<A>.Instance.Add(ref list, in value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.Add(ref list, in value, out index);
    }
}