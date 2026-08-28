using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class ArgsExtensions
{
    extension(ref Args list)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add<A>(in A value) =>
            ArgsGen<A>.Instance.Add(ref list, in value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Add<A>(in A value, out ushort index) =>
            ArgsGen<A>.Instance.Add(ref list, in value, out index);
    }
}