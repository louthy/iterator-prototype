using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsExtensions
{
    extension(in Globals list)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAt<A>(ushort ix, out A value) =>
            GlobalsGen<A>.Instance.ResetAt(in list, ix, out value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAt<A>(ushort ix) =>
            GlobalsGen<A>.Instance.ResetAt(in list, ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A DeclaredAt<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.DeclaredAt(in list, ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A At<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.At(in list, ix);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool At<A>(ushort ix, out A value) =>
            GlobalsGen<A>.Instance.At(in list, ix, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AtEnd<A>(ushort ix, out Global<A> global) =>
            GlobalsGen<A>.Instance.AtEnd(in list, ix, out global);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutable<A>(in A value) =>
            GlobalsGen<A>.Instance.AddMutable(in list, in value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutable<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.AddMutable(in list, in value, out index);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConst<A>(in A value) =>
            GlobalsGen<A>.Instance.AddConst(in list, in value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConst<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.AddConst(in list, in value, out index);        
    }
}