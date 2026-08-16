using System.Diagnostics.Contracts;
using LanguageExt.Traits;

namespace IteratorPrototype;

public static partial class ArrExtensions
{
    extension<A>(K<Arr, A> self)
    {
        [Pure]
        public static bool operator ==(K<Arr, A> lhs, K<Arr, A> rhs) =>
            (+lhs).Equals(rhs);
        
        [Pure]
        public static bool operator !=(K<Arr, A> lhs, K<Arr, A> rhs) =>
            !(+lhs == +rhs);
    }
}
