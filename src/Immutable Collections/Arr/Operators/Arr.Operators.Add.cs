using System.Diagnostics.Contracts;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class ArrExtensions
{
    extension<A>(Arr<A> self)
    {
        [Pure]
        public static Arr<A> operator +(Arr<A> lhs, Arr<A> rhs) =>
            lhs.AddRange(rhs);

        [Pure]
        public static Arr<A> operator +(LE.Pure<A> lhs, Arr<A> rhs) =>
            rhs.Cons(lhs.Value);

        [Pure]
        public static Arr<A> operator +(Arr<A> lhs, LE.Pure<A> rhs) =>
            lhs.Add(rhs.Value);

        [Pure]
        public static Arr<A> operator +(A lhs, Arr<A> rhs) =>
            rhs.Cons(lhs);

        [Pure]
        public static Arr<A> operator +(Arr<A> lhs, A rhs) =>
            lhs.Add(rhs);
        
        [Pure]
        public static Arr<A> operator +(Arr<A> lhs, ReadOnlySpan<A> rhs) =>
            lhs.AddRange(rhs);
        
        [Pure]
        public static Arr<A> operator +(Arr<A> lhs, IEnumerable<A> rhs) =>
            lhs.AddRange(rhs);
    }

    extension<A>(K<Arr, A> self)
    {
        [Pure]
        public static Arr<A> operator +(K<Arr, A> lhs, K<Arr, A> rhs) =>
            (+lhs).AddRange(rhs);

        [Pure]
        public static Arr<A> operator +(LE.Pure<A> lhs, K<Arr, A> rhs) =>
            (+rhs).Cons(lhs.Value);

        [Pure]
        public static Arr<A> operator +(K<Arr, A> lhs, LE.Pure<A> rhs) =>
            (+lhs).Add(rhs.Value);

        [Pure]
        public static Arr<A> operator +(A lhs, K<Arr, A> rhs) =>
            (+rhs).Cons(lhs);

        [Pure]
        public static Arr<A> operator +(K<Arr, A> lhs, A rhs) =>
            (+lhs).Add(rhs);
        
        [Pure]
        public static Arr<A> operator +(K<Arr, A> lhs, ReadOnlySpan<A> rhs) =>
            (+lhs).AddRange(rhs);
        
        [Pure]
        public static Arr<A> operator +(K<Arr, A> lhs, IEnumerable<A> rhs) =>
            (+lhs).AddRange(rhs);
    }
}
