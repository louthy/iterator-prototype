using System.Runtime.CompilerServices;
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class VarsExtensions
{
    extension(ref Vars vars)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A>(out A value, bool force) =>
            VarsGen<A>.Instance.PopImpl(ref vars, out value, force);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B>(out A value1, out B value2)
        {
            var topCo2 = vars.PeekIsCoRoutineArgument;

            if (vars.Pop(out value2, true))
            {
                var r = vars.Pop(out value1, false);
                if (topCo2) vars.Push(in value2, true);
                return r;
            }
            else
            {
                value1 = default!;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B, C>(out A value1, out B value2, out C value3)
        {
            var topCo3 = vars.PeekIsCoRoutineArgument;

            if (vars.Pop(out value3, true))
            {
                var topCo2 = vars.PeekIsCoRoutineArgument;
                if (vars.Pop(out value2, true))
                {
                    var r = vars.Pop(out value1, false);
                    if (topCo2) vars.Push(in value2, true);
                    if (topCo3) vars.Push(in value3, true);
                    return r;
                }
                else
                {
                    if (topCo3) vars.Push(in value3, true);
                    value1 = default!;
                    return false;
                }
            }
            else
            {
                value1 = default!;
                value2 = default!;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B, C, D>(out A value1, out B value2, out C value3, out D value4)
        {
            var topCo4 = vars.PeekIsCoRoutineArgument;

            if (vars.Pop(out value4, true))
            {
                var topCo3 = vars.PeekIsCoRoutineArgument;

                if (vars.Pop(out value3, true))
                {
                    var topCo2 = vars.PeekIsCoRoutineArgument;
                    if (vars.Pop(out value2, true))
                    {
                        var r = vars.Pop(out value1, false);
                        if (topCo2) vars.Push(in value2, true);
                        if (topCo3) vars.Push(in value3, true);
                        if (topCo4) vars.Push(in value4, true);
                        return r;
                    }
                    else
                    {
                        if (topCo3) vars.Push(in value3, true);
                        if (topCo4) vars.Push(in value4, true);
                        value1 = default!;
                        return false;
                    }
                }
                else
                {
                    if (topCo4) vars.Push(in value4, true);
                    value1 = default!;
                    value2 = default!;
                    return false;
                }            
            }
            else
            {
                value1 = default!;
                value2 = default!;
                value3 = default!;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B, C, D, E>(out A value1, out B value2, out C value3, out D value4, out E value5)
        {
            var topCo5 = vars.PeekIsCoRoutineArgument;

            if (vars.Pop(out value5, true))
            {
                var topCo4 = vars.PeekIsCoRoutineArgument;

                if (vars.Pop(out value4, true))
                {
                    var topCo3 = vars.PeekIsCoRoutineArgument;

                    if (vars.Pop(out value3, true))
                    {
                        var topCo2 = vars.PeekIsCoRoutineArgument;
                        if (vars.Pop(out value2, true))
                        {
                            var r = vars.Pop(out value1, false);
                            if (topCo2) vars.Push(in value2, true);
                            if (topCo3) vars.Push(in value3, true);
                            if (topCo4) vars.Push(in value4, true);
                            if (topCo5) vars.Push(in value5, true);
                            return r;
                        }
                        else
                        {
                            if (topCo3) vars.Push(in value3, true);
                            if (topCo4) vars.Push(in value4, true);
                            if (topCo5) vars.Push(in value5, true);
                            value1 = default!;
                            return false;
                        }
                    }
                    else
                    {
                        if (topCo4) vars.Push(in value4, true);
                        if (topCo5) vars.Push(in value5, true);
                        value1 = default!;
                        value2 = default!;
                        return false;
                    }
                }
                else
                {
                    if (topCo5) vars.Push(in value5, true);
                    value1 = default!;
                    value2 = default!;
                    value3 = default!;
                    return false;
                }
            }
            else
            {
                value1 = default!;
                value2 = default!;
                value3 = default!;
                value4 = default!;
                return false;
                
            }
        }        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B, C, D, E, F>(out A value1, out B value2, out C value3, out D value4, out E value5, out F value6)
        {
            var topCo6 = vars.PeekIsCoRoutineArgument;

            if (vars.Pop(out value6, true))
            {
                var topCo5 = vars.PeekIsCoRoutineArgument;

                if (vars.Pop(out value5, true))
                {
                    var topCo4 = vars.PeekIsCoRoutineArgument;

                    if (vars.Pop(out value4, true))
                    {
                        var topCo3 = vars.PeekIsCoRoutineArgument;

                        if (vars.Pop(out value3, true))
                        {
                            var topCo2 = vars.PeekIsCoRoutineArgument;
                            if (vars.Pop(out value2, true))
                            {
                                var r = vars.Pop(out value1, false);
                                if (topCo2) vars.Push(in value2, true);
                                if (topCo3) vars.Push(in value3, true);
                                if (topCo4) vars.Push(in value4, true);
                                if (topCo5) vars.Push(in value5, true);
                                if (topCo6) vars.Push(in value6, true);
                                return r;
                            }
                            else
                            {
                                if (topCo3) vars.Push(in value3, true);
                                if (topCo4) vars.Push(in value4, true);
                                if (topCo5) vars.Push(in value5, true);
                                if (topCo6) vars.Push(in value6, true);
                                value1 = default!;
                                return false;
                            }
                        }
                        else
                        {
                            if (topCo4) vars.Push(in value4, true);
                            if (topCo5) vars.Push(in value5, true);
                            if (topCo6) vars.Push(in value6, true);
                            value1 = default!;
                            value2 = default!;
                            return false;
                        }
                    }
                    else
                    {
                        if (topCo5) vars.Push(in value5, true);
                        if (topCo6) vars.Push(in value6, true);
                        value1 = default!;
                        value2 = default!;
                        value3 = default!;
                        return false;
                    }
                }
                else
                {
                    if (topCo6) vars.Push(in value6, true);
                    value1 = default!;
                    value2 = default!;
                    value3 = default!;
                    value4 = default!;
                    return false;

                }
            }
            else
            {
                value1 = default!;
                value2 = default!;
                value3 = default!;
                value4 = default!;
                value5 = default!;
                return false;
            }
        }        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B, C, D, E, F, G>(out A value1, out B value2, out C value3, out D value4, out E value5, out F value6, out G value7)
        {
            var topCo7 = vars.PeekIsCoRoutineArgument;

            if (vars.Pop(out value7, true))
            {
                var topCo6 = vars.PeekIsCoRoutineArgument;

                if (vars.Pop(out value6, true))
                {
                    var topCo5 = vars.PeekIsCoRoutineArgument;

                    if (vars.Pop(out value5, true))
                    {
                        var topCo4 = vars.PeekIsCoRoutineArgument;

                        if (vars.Pop(out value4, true))
                        {
                            var topCo3 = vars.PeekIsCoRoutineArgument;

                            if (vars.Pop(out value3, true))
                            {
                                var topCo2 = vars.PeekIsCoRoutineArgument;
                                if (vars.Pop(out value2, true))
                                {
                                    var r = vars.Pop(out value1, false);
                                    if (topCo2) vars.Push(in value2, true);
                                    if (topCo3) vars.Push(in value3, true);
                                    if (topCo4) vars.Push(in value4, true);
                                    if (topCo5) vars.Push(in value5, true);
                                    if (topCo6) vars.Push(in value6, true);
                                    if (topCo7) vars.Push(in value7, true);
                                    return r;
                                }
                                else
                                {
                                    if (topCo3) vars.Push(in value3, true);
                                    if (topCo4) vars.Push(in value4, true);
                                    if (topCo5) vars.Push(in value5, true);
                                    if (topCo6) vars.Push(in value6, true);
                                    if (topCo7) vars.Push(in value7, true);
                                    value1 = default!;
                                    return false;
                                }
                            }
                            else
                            {
                                if (topCo4) vars.Push(in value4, true);
                                if (topCo5) vars.Push(in value5, true);
                                if (topCo6) vars.Push(in value6, true);
                                if (topCo7) vars.Push(in value7, true);
                                value1 = default!;
                                value2 = default!;
                                return false;
                            }
                        }
                        else
                        {
                            if (topCo5) vars.Push(in value5, true);
                            if (topCo6) vars.Push(in value6, true);
                            if (topCo7) vars.Push(in value7, true);
                            value1 = default!;
                            value2 = default!;
                            value3 = default!;
                            return false;
                        }
                    }
                    else
                    {
                        if (topCo6) vars.Push(in value6, true);
                        if (topCo7) vars.Push(in value7, true);
                        value1 = default!;
                        value2 = default!;
                        value3 = default!;
                        value4 = default!;
                        return false;

                    }
                }
                else
                {
                    if (topCo7) vars.Push(in value7, true);
                    value1 = default!;
                    value2 = default!;
                    value3 = default!;
                    value4 = default!;
                    value5 = default!;
                    return false;
                }
            }
            else
            {
                value1 = default!;
                value2 = default!;
                value3 = default!;
                value4 = default!;
                value5 = default!;
                value6 = default!;
                return false;
            }
        }        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A>(bool force) =>
            VarsGen<A>.Instance.PopImpl(ref vars, force);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Pop<A, B>(bool force) =>
            vars.Pop<B>(force) &&
            vars.Pop<A>(force);        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Push<A>(in A value, bool isCoRoutineArgument) =>
            VarsGen<A>.Instance.PushImpl(ref vars, in value, isCoRoutineArgument);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Peek<A>(out A value) =>
            VarsGen<A>.Instance.PeekImpl(ref vars, out value);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Peek<A, B>(out A value1, out B value2)
        {
            var isco = vars.PeekIsCoRoutineArgument;
            if (vars.Pop(out value2, true))
            {
                return vars.Peek(out value1) 
                           ? vars.Push(in value2, isco) 
                           : vars.Push(in value2, isco) && false;
            }
            else
            {
                value1 = default!;
                return false;
            }
        }

        [MethodImpl(Optimisations.InliningOnly)]
        public ref A PeekAt<A>() =>
            ref VarsGen<A>.Instance.PeekAtImpl(ref vars);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool PeekAt<A, B>()
        {
            throw new NotSupportedException("Not supported for tuples, because they're not at a single region");
        }        

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Push<A, B>(in (A, B) pair, bool isCoRoutineArgument) =>
            vars.Push(in pair.Item1, isCoRoutineArgument) &&
            vars.Push(in pair.Item2, isCoRoutineArgument);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Push<A, B>(in A first, in B second, bool isCoRoutineArgument) =>
            vars.Push(in first, isCoRoutineArgument) &&
            vars.Push(in second, isCoRoutineArgument);

        [MethodImpl(Optimisations.InliningOnly)]
        public bool Dup<A>() =>
            VarsGen<A>.Instance.DupImpl(ref vars);
        
    }
}
