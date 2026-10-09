using System.Runtime.CompilerServices;
using IteratorPrototype.Memory;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsExtensions
{
    extension(in Globals globals)
    {
        ref GlobalsMutable Ref
        {
            [MethodImpl(Optimisations.Default)]
            get => ref Unsafe.As<Globals, GlobalsMutable>(ref Unsafe.AsRef(in globals));
        } 
        
        [MethodImpl(Optimisations.Default)]
        public bool AtEndStruct<A>(ushort ix, out Global<A> global)
            where A : struct
        {
            var count = globals.objs.Count;
            if (ix <= count)
            {
                global = new Global<A>((ushort)(count - ix));
                return true;
            }
            else
            {
                global = default;
                return false;
            }
        }

        [MethodImpl(Optimisations.Default)]
        public bool AtEndManaged<A>(ushort ix, out Global<A> global)
            where A : class
        {
            var count = globals.objs.Count;
            if (ix <= count)
            {
                global = new Global<A>((ushort)(count - ix));
                return true;
            }
            else
            {
                global = default;
                return false;
            }
        }
        
        [MethodImpl(Optimisations.Default)]
        public bool AtEndUnmanaged<A>(ushort ix, out Global<A> global)
            where A : unmanaged
        {
            var count = globals.values.Count;
            if (ix <= count)
            {
                global = new Global<A>((ushort)(count - ix));
                return true;
            }
            else
            {
                global = default;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool DeclaredAtUnmanaged<A>(ushort ix, out A value)
            where A : unmanaged =>
            globals.values.DeclaredAt(ix, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool DeclaredAtManaged<A>(ushort ix, out A value)
            where A : class =>
            globals.objs.DeclaredAt(ix, out value);

        [MethodImpl(Optimisations.Default)]
        public bool DeclaredAtStruct<A>(ushort ix, out A value)
            where A : struct
        {
            if (globals.DeclaredAtManaged<Box<A>>(ix, out var box))
            {
                value = box.Ref;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AtUnmanaged<A>(ushort ix, out A value)
            where A : unmanaged =>
            globals.values.At(ix, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AtManaged<A>(ushort ix, out A value)
            where A : class =>
            globals.objs.At(ix, out value);

        [MethodImpl(Optimisations.Default)]
        public bool AtStruct<A>(ushort ix, out A value)
            where A : struct
        {
            if (globals.AtManaged<Box<A>>(ix, out var box))
            {
                value = box.Ref;
                return true;
            }
            else
            {
                value = default!;
                return false;
            }
        }
    }
}