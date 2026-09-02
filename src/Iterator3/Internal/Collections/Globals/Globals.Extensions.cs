using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsExtensions
{
    extension(ref Globals list)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAt<A>(in ushort ix, out A value) =>
            GlobalsGen<A>.Instance.ResetAt(ref list, in ix, out value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAt<A>(in ushort ix) =>
            GlobalsGen<A>.Instance.ResetAt(ref list, in ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A DeclaredAt<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.DeclaredAt(ref list, ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A At<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.At(ref list, ix);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool At<A>(in ushort ix, out A value) =>
            GlobalsGen<A>.Instance.At(ref list, in ix, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AtEnd<A>(in ushort ix, out Global<A> global) =>
            GlobalsGen<A>.Instance.AtEnd(ref list, in ix, out global);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool Add<A>(in A value) =>
            GlobalsGen<A>.Instance.Add(ref list, in value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool Add<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.Add(ref list, in value, out index);
    }
}