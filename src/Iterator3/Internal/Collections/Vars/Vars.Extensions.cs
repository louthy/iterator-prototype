using System.Runtime.CompilerServices;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(in Vars vars)
    {
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
        public void Pop<A, B, C, D, E, F>(out A value1, out B value2, out C value3, out D value4, out E value5, out F value6)
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
