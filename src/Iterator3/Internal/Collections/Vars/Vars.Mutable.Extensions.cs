using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IteratorPrototype.Memory;

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsMutableExtensions
{
    extension(ref VarsMutable vars)
    {
        public Span<bool> Flags
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, bool>(ref Unsafe.AsRef(in vars.flag0)), vars.top);
        }

        public Span<byte> FlagBytes
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in vars.flag0), vars.top);
        }

        public Span<bool> AllFlags
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.As<byte, bool>(ref Unsafe.AsRef(in vars.flag0)), Vars.Capacity);
        }

        public Span<byte> AllFlagBytes
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get => MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in vars.flag0), Vars.Capacity);
        }

        public uint ObjsCount =>
            vars.objs.Count;

        public uint ValuesCount =>
            vars.values.Count;

        public bool PeekIsCoRoutineArgument
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get
            {
                var s = vars.FlagBytes;
                return s.Length > 0 && (s[^1] & 1) == 1;
            }
        }

        public bool PeekIsManaged
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get
            {
                var s = vars.FlagBytes;
                return s.Length > 0 && (s[^1] & 2) == 0;
            }
        }

        public bool PeekIsUnmanaged
        {
            [MethodImpl(Optimisations.InliningOnly)]
            get
            {
                var s = vars.FlagBytes;
                return s.Length > 0 && (s[^1] & 2) == 2;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PushFlagManaged(bool isCoRoutineArgument)
        {
            // Set the flag for whether this is a coroutine argument
            vars.top++;
            if (vars.top == 0) throw new InvalidOperationException("HOW?");
            vars.Flags[^1] = isCoRoutineArgument;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PushFlagUnmanaged(bool isCoRoutineArgument)
        {
            // Set the flag for whether this is a coroutine argument
            vars.top++;
            vars.FlagBytes[^1] = (byte)(2 | Unsafe.As<bool, byte>(ref isCoRoutineArgument));
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopFlag()
        {
            vars.top--;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void DupStruct<A>()
            where A : struct =>
            vars.objs.Dup();

        [MethodImpl(Optimisations.InliningOnly)]
        public void DupManaged<A>()
            where A : class =>
            vars.objs.Dup();

        [MethodImpl(Optimisations.InliningOnly)]
        public void DupUnmanaged<A>()
            where A : unmanaged =>
            vars.values.Dup<A>();

        [MethodImpl(Optimisations.InliningOnly)]
        public void PushStruct<A>(in A value, bool isCoRoutineArgument)
            where A : struct =>
            vars.PushManaged(Box.alloc(in value), isCoRoutineArgument);

        [MethodImpl(Optimisations.InliningOnly)]
        public void PushManaged<A>(in A value, bool isCoRoutineArgument)
            where A : class
        {
            vars.objs.Push(in value);
            vars.PushFlagManaged(isCoRoutineArgument);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PushUnmanaged<A>(in A value, bool isCoRoutineArgument)
            where A : unmanaged
        {
            vars.values.Push(in value);
            vars.PushFlagUnmanaged(isCoRoutineArgument);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopStruct<A>(out A value, bool force)
            where A : struct
        {
            if (!force && vars.PeekIsCoRoutineArgument)
            {
                vars.PeekStruct(out value);
            }
            else
            {
                vars.PopManaged<Box<A>>(out var box, force);
                value = box.Value;
                box.Free();
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopManaged<A>(out A value, bool force)
            where A : class
        {
            if (!force && vars.PeekIsCoRoutineArgument)
            {
                vars.PeekManaged(out value);
                return;
            }

            vars.objs.Pop(out value);
            vars.PopFlag();
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopUnmanaged<A>(out A value, bool force)
            where A : unmanaged
        {
            if (!force && vars.PeekIsCoRoutineArgument)
            {
                vars.PeekUnmanaged(out value);
                return;
            }

            vars.values.Pop(out value);
            vars.PopFlag();
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopStruct<A>(bool force)
            where A : struct
        {
            if (!force && vars.PeekIsCoRoutineArgument)
            {
                return;
            }

            vars.PopManaged<Box<A>>(out var box, force);
            box.Free();
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopManaged(bool force)
        {
            if (!force && vars.PeekIsCoRoutineArgument)
            {
                return;
            }

            vars.objs.Pop();
            vars.PopFlag();
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PopUnmanaged<A>(bool force)
            where A : unmanaged
        {
            if (!force && vars.PeekIsCoRoutineArgument)
            {
                return;
            }

            vars.values.Pop<A>();
            vars.PopFlag();
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAtStruct<A>()
            where A : struct =>
            ref vars.objs.PeekAt<Box<A>>().Ref;

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAtManaged<A>()
            where A : class =>
            ref vars.objs.PeekAt<A>();

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAtUnmanaged<A>()
            where A : unmanaged =>
            ref vars.values.PeekAt<A>();

        [MethodImpl(Optimisations.InliningOnly)]
        public void PeekStruct<A>(out A value)
            where A : struct
        {
            vars.objs.Peek<Box<A>>(out var box);
            value = box.Value;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void PeekManaged<A>(out A value)
            where A : class =>
            vars.objs.Peek(out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public void PeekUnmanaged<A>(out A value)
            where A : unmanaged =>
            vars.values.Peek(out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public void SyncTo(ref TopsMutable tops)
        {
            var os      = (uint)(vars.objs.Count   << Tops.ObjsShift)   & Tops.ObjsMask;
            var vs      = (uint)(vars.values.Count << Tops.ValuesShift) & Tops.ValuesMask;
            var t       = (uint)(vars.top          << Tops.VarsShift)   & Tops.VarsMask;
            var current = tops.Current & ~(Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
            tops.Current = current | os | vs | t;
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void SyncFrom(in TopsMutable tops)
        {
            var snapshot = tops.Current & (Tops.ObjsMask | Tops.ValuesMask | Tops.VarsMask);
            var os       = (snapshot & Tops.ObjsMask)   >> Tops.ObjsShift;
            var vs       = (snapshot & Tops.ValuesMask) >> Tops.ValuesShift;
            var nt       = (snapshot & Tops.VarsMask)   >> Tops.VarsShift;

            // Set the flags top to reflect how many objs and vals we're losing:
            vars.top = (byte)nt;

            // Reset the tops
            vars.objs.PopToTop(os);
            vars.values.PopToTop(vs);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Zero()
        {
            // Set the flags top to zero.
            vars.top = 0;

            return vars.objs.PopToTop(0) && vars.values.PopToTop(0);
        }
        
        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A>(out A value, bool force) =>
            VarsGen<A>.Instance.PopImpl(ref Unsafe.AsRef(in vars), out value, force);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B>(out A value1, out B value2)
        {
            var topCo2 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Pop(out value1, false);
            if (topCo2) vars.Push(in value2, true);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B, C>(out A value1, out B value2, out C value3)
        {
            var topCo3 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value3, true);
            var topCo2 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Pop(out value1, false);
            if (topCo2) vars.Push(in value2, true);
            if (topCo3) vars.Push(in value3, true);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B, C, D>(out A value1, out B value2, out C value3, out D value4)
        {
            var topCo4 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value4, true);
            var topCo3 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value3, true);
            var topCo2 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Pop(out value1, false);
            if (topCo2) vars.Push(in value2, true);
            if (topCo3) vars.Push(in value3, true);
            if (topCo4) vars.Push(in value4, true);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B, C, D, E>(out A value1, out B value2, out C value3, out D value4, out E value5)
        {
            var topCo5 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value5, true);
            var topCo4 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value4, true);
            var topCo3 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value3, true);
            var topCo2 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Pop(out value1, false);
            if (topCo2) vars.Push(in value2, true);
            if (topCo3) vars.Push(in value3, true);
            if (topCo4) vars.Push(in value4, true);
            if (topCo5) vars.Push(in value5, true);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B, C, D, E, F>(out A value1, out B value2, out C value3, out D value4, out E value5,
                                          out F value6)
        {
            var topCo6 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value6, true);
            var topCo5 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value5, true);
            var topCo4 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value4, true);
            var topCo3 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value3, true);
            var topCo2 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Pop(out value1, false);
            if (topCo2) vars.Push(in value2, true);
            if (topCo3) vars.Push(in value3, true);
            if (topCo4) vars.Push(in value4, true);
            if (topCo5) vars.Push(in value5, true);
            if (topCo6) vars.Push(in value6, true);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B, C, D, E, F, G>(out A value1, out B value2, out C value3, out D value4, out E value5, out F value6, out G value7)
        {
            var topCo7 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value7, true);
            var topCo6 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value6, true);
            var topCo5 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value5, true);
            var topCo4 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value4, true);
            var topCo3 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value3, true);
            var topCo2 = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Pop(out value1, false);
            if (topCo2) vars.Push(in value2, true);
            if (topCo3) vars.Push(in value3, true);
            if (topCo4) vars.Push(in value4, true);
            if (topCo5) vars.Push(in value5, true);
            if (topCo6) vars.Push(in value6, true);
            if (topCo7) vars.Push(in value7, true);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A>(bool force) =>
            VarsGen<A>.Instance.PopImpl(ref Unsafe.AsRef(in vars), force);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Pop<A, B>(bool force)
        {
            vars.Pop<B>(force);
            vars.Pop<A>(force);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Push<A>(in A value, bool isCoRoutineArgument) =>
            VarsGen<A>.Instance.PushImpl(ref Unsafe.AsRef(in vars), in value, isCoRoutineArgument);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Peek<A>(out A value) =>
            VarsGen<A>.Instance.PeekImpl(ref Unsafe.AsRef(in vars), out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public void Peek<A, B>(out A value1, out B value2)
        {
            var isco = vars.PeekIsCoRoutineArgument;
            vars.Pop(out value2, true);
            vars.Peek(out value1);
            vars.Push(in value2, isco);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAt<A>() =>
            ref VarsGen<A>.Instance.PeekAtImpl(ref Unsafe.AsRef(in vars));

        [MethodImpl(Optimisations.InliningOnly)]
        public bool PeekAt<A, B>()
        {
            throw new NotSupportedException("Not supported for tuples, because they're not at a single region");
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Push<A, B>(in (A, B) pair, bool isCoRoutineArgument)
        {
            vars.Push(in pair.Item1, isCoRoutineArgument);
            vars.Push(in pair.Item2, isCoRoutineArgument);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Push<A, B>(in A first, in B second, bool isCoRoutineArgument)
        {
            vars.Push(in first, isCoRoutineArgument);
            vars.Push(in second, isCoRoutineArgument);
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public void Dup<A>() =>
            VarsGen<A>.Instance.DupImpl(ref Unsafe.AsRef(in vars));
    }
}