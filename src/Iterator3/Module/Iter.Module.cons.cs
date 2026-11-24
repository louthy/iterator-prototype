#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public partial class Iter
{
    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A head, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in head)
                   ? iter
                   : Insert.cons(in frame, in head, in tail)
                       ? iter
                       : throw new NotImplementedException("TODO: Deal with a full StackFrame");
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A head, Func<Iter<A>> lazyTail) =>
        cons(head, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, Func<Iter<A>> lazyTail) =>
        cons(a, b, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, Func<Iter<A>> lazyTail) =>
        cons(a, b, c, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, A d, Func<Iter<A>> lazyTail) =>
        cons(a, b, c, d, lazy(lazyTail));

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in b)
                   ? Insert.yield(in frame, in a)
                         ? iter
                         : Insert.cons(in frame, in b, in tail)
                             ? cons(a, iter)
                             : cons(a, cons(b, tail))
                   : Insert.cons(in frame, in b, in tail)
                       ? cons(a, iter)
                       : cons(a, cons(b, tail));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in c)
                   ? Insert.yield(in frame, in b)
                         ? Insert.yield(in frame, in a)
                               ? iter
                               : cons(a, cons(b, cons(c, tail)))
                         : cons(a, cons(b, cons(c, tail)))
                   : Insert.cons(in frame, in b, in tail)
                       ? cons(a, iter)
                       : cons(a, cons(b, cons(c, tail)));
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public static Iter<A> cons<A>(A a, A b, A c, A d, Iter<A> tail)
    {
        var frame = Iter<A>.Next(in tail, out var iter);
        return Insert.yield(in frame, in d)
                   ? Insert.yield(in frame, in c)
                         ? Insert.yield(in frame, in b)
                               ? Insert.yield(in frame, in a)
                                     ? iter
                                     : cons(a, cons(b, cons(c, cons(d, tail))))
                               : cons(a, cons(b, cons(c, cons(d, tail))))
                         : Insert.cons(in frame, in b, in tail)
                             ? cons(a, iter)
                             : cons(a, cons(b, cons(c, cons(d, tail))))
                   : cons(a, cons(b, cons(c, cons(d, tail))));
    }
}