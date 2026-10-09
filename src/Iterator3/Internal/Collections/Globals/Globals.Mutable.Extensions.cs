#pragma warning disable CS0649
#pragma warning disable CS8618 
#pragma warning disable CS0169
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using IteratorPrototype.Iterator3.Internal.Memory;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class GlobalsMutableExtensions
{
    extension(ref GlobalsMutable globals)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAt<A>(ushort ix, out A value) =>
            GlobalsGen<A>.Instance.ResetAt(ref globals, ix, out value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAt<A>(ushort ix) =>
            GlobalsGen<A>.Instance.ResetAt(ref globals, ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A DeclaredAt<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.DeclaredAt(ref globals, ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A At<A>(ushort ix) =>
            ref GlobalsGen<A>.Instance.At(ref globals, ix);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool At<A>(ushort ix, out A value) =>
            GlobalsGen<A>.Instance.At(ref globals, ix, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AtEnd<A>(ushort ix, out Global<A> global) =>
            GlobalsGen<A>.Instance.AtEnd(ref globals, ix, out global);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutable<A>(in A value) =>
            GlobalsGen<A>.Instance.AddMutable(ref globals, in value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutable<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.AddMutable(ref globals, in value, out index);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConst<A>(in A value) =>
            GlobalsGen<A>.Instance.AddConst(ref globals, in value);
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConst<A>(in A value, out ushort index) =>
            GlobalsGen<A>.Instance.AddConst(ref globals, in value, out index);
        
        
        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAtUnmanaged<A>(ushort ix, out A value)
            where A : unmanaged =>
            globals.values.RestoreAt(in ix, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAtManaged<A>(ushort ix, out A value)
            where A : class =>
            globals.objs.RestoreAt(ix, out value);

        [MethodImpl(Optimisations.Default)]
        public bool ResetAtStruct<A>(ushort ix, out A value)
            where A : struct
        {
            if (ix < globals.objs.Count)
            {
                ref var declared = ref globals.DeclaredAtStruct<A>(ix);
                ref var variable = ref globals.AtStruct<A>(ix);
                variable = declared;
                value = variable;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAtUnmanaged<A>(ushort ix)
            where A : unmanaged =>
            globals.values.RestoreAt<A>(ix, out _);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAtManaged(ushort ix) =>
            globals.objs.RestoreAt(ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool ResetAtStruct<A>(ushort ix)
            where A : struct
        {
            if (ix >= globals.objs.Count) return false;
            ref var declared = ref globals.DeclaredAtStruct<A>(ix);
            ref var variable = ref globals.AtStruct<A>(ix);
            variable = declared;
            return true;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A DeclaredAtUnmanaged<A>(ushort ix)
            where A : unmanaged =>
            ref globals.values.DeclaredAt<A>(ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A DeclaredAtManaged<A>(ushort ix)
            where A : class =>
            ref globals.objs.DeclaredAt<A>(ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A DeclaredAtStruct<A>(ushort ix)
            where A : struct =>
            ref globals.DeclaredAtManaged<Box<A>>(ix).Ref;

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
        public ref A AtUnmanaged<A>(ushort ix)
            where A : unmanaged =>
            ref globals.values.At<A>(ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A AtManaged<A>(ushort ix)
            where A : class =>
            ref globals.objs.At<A>(ix);

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A AtStruct<A>(ushort ix)
            where A : struct =>
            ref globals.AtManaged<Box<A>>(ix).Ref;

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

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutableStruct<A>(in A value)
            where A : struct =>
            globals.AddMutableStruct(in value, out _);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutableStruct<A>(in A value, out ushort index)
            where A : struct =>
            globals.objs.PushMutable(Box.alloc(in value), Box.alloc(in value), out index);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutableManaged<A>(in A value)
            where A : class =>
            globals.AddMutableManaged(in value, out _);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutableManaged<A>(in A value, out ushort index)
            where A : class =>
            globals.objs.PushMutable(value, out index);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutableUnmanaged<A>(in A value)
            where A : unmanaged =>
            globals.values.AddMutable(in value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddMutableUnmanaged<A>(in A value, out ushort index)
            where A : unmanaged =>
            globals.values.AddMutable(in value, out index);




        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConstStruct<A>(in A value)
            where A : struct =>
            globals.AddMutableStruct(in value, out _);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConstStruct<A>(in A value, out ushort index)
            where A : struct =>
            globals.objs.PushConst(Box.alloc(in value), out index);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConstManaged<A>(in A value)
            where A : class =>
            globals.AddConstManaged(in value, out _);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConstManaged<A>(in A value, out ushort index)
            where A : class =>
            globals.objs.PushConst(value, out index);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConstUnmanaged<A>(in A value)
            where A : unmanaged =>
            globals.values.AddConst(in value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool AddConstUnmanaged<A>(in A value, out ushort index)
            where A : unmanaged =>
            globals.values.AddConst(in value, out index);


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
    }
}