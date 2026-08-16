using System;
using LanguageExt.Traits;

namespace LanguageExt;

public static partial class ArrExtensions
{
    extension<A, B>(K<Arr, A> self)
    {
        /// <summary>
        /// Monad bind operator
        /// </summary>
        /// <param name="ma">Monad to bind</param>
        /// <param name="f">Binding function</param>
        /// <returns>Mapped monad</returns>
        public static Arr<B> operator >> (K<Arr, A> ma, Func<A, K<Arr, B>> f) =>
            +ma.Bind(f);
        
        /// <summary>
        /// Sequentially compose two actions, discarding any value produced by the first, like sequencing operators (such
        /// as the semicolon) in C#.
        /// </summary>
        /// <param name="lhs">First action to run</param>
        /// <param name="rhs">Second action to run</param>
        /// <returns>Result of the second action</returns>
        public static Arr<B> operator >> (K<Arr, A> lhs, K<Arr, B> rhs) =>
            lhs >> (_ => rhs);
    }
    
    extension<A>(K<Arr, A> self)
    {
        /// <summary>
        /// Sequentially compose two actions.  The second action is a unit-returning action, so the result of the
        /// first action is propagated. 
        /// </summary>
        /// <param name="lhs">First action to run</param>
        /// <param name="rhs">Second action to run</param>
        /// <returns>Result of the first action</returns>
        public static Arr<A> operator >> (K<Arr, A> lhs, K<Arr, Unit> rhs) =>
            lhs >> (x => (_ => x) * rhs);
    }
}
