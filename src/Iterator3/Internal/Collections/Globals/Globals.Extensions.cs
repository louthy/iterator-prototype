using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsExtensions
{
    extension(ref Globals list)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ResetAt<A>(in ushort ix, out A value) =>
            GlobalsGen<A>.Instance.ResetAt(ref list, in ix, out value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ResetAt<A>(in ushort ix) =>
            GlobalsGen<A>.Instance.ResetAt(ref list, in ix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref A DeclaredAt<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.DeclaredAt(ref list, ix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref A At<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.At(ref list, ix);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool At<A>(in ushort ix, out A value) =>
            GlobalsGen<A>.Instance.At(ref list, in ix, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref A AtEnd<A>(ushort ix) =>
            ref list.At<A>((ushort)(list.Count - ix));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AtEnd<A>(in ushort ix, out Global<A> global, out A value)
        {
            var ixe = (ushort)(list.Count - ix);
            if (list.At(ixe, out A x))
            {
                global = new Global<A>(ixe);
                value = x;
                return true;
            }
            else
            {
                global = default;
                value = default!;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AtEnd<A>(in ushort ix, out Global<A> global)
        {
            if (ix <= list.Count)
            {
                global = new Global<A>((ushort)(list.Count - ix));
                return true;
            }
            else
            {
                global = default;
                return false;
            }
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Put<A>(ushort ix, in A value)
        {
            if(ix < list.Count)
            {
                ref var g = ref GlobalsGen<A>.Instance.At(ref list, ix);
                g = value;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add<A>(in A value) =>
            GlobalsGen<A>.Instance.Add(ref list, in value);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.Add(ref list, in value, out index);
    }
}