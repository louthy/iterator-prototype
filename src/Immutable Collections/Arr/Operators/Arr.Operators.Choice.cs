using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class ArrExtensions
{
    extension<A>(Arr<A> self)
    {
        public static Arr<A> operator |(Arr<A> lhs, Arr<A> rhs) =>
            lhs.Choose(rhs);

        public static Arr<A> operator |(Arr<A> lhs, LE.Pure<A> rhs) =>
            lhs.Choose(rhs.ToArr());
    }

    extension<A>(K<Arr, A> self)
    {
        public static Arr<A> operator |(K<Arr, A> lhs, K<Arr, A> rhs) =>
            (+lhs).Choose(rhs);

        public static Arr<A> operator |(K<Arr, A> lhs, LE.Pure<A> rhs) =>
            (+lhs).Choose(rhs.ToArr());
    }
}
