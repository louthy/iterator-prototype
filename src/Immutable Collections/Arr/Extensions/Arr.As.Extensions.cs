using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class ArrExtensions
{
    extension<A>(K<Arr, A> ma)
    {
        public Arr<A> As() =>
            (Arr<A>)ma;
    }

    extension<A>(LE.Pure<A> ma)
    {
        public Arr<A> ToArr() =>
            Arr.singleton(ma.Value);
    }
}
