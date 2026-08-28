using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsExtensions
{
    extension(ref Globals list)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref A At<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.At(ref list, ix);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool At<A>(in ushort ix, out A value) =>
            GlobalsGen<A>.Instance.At(ref list, in ix, out value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add<A>(in A value) =>
            GlobalsGen<A>.Instance.Add(ref list, in value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.Add(ref list, in value, out index);
    }
}