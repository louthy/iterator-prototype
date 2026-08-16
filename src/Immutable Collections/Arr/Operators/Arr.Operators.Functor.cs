using System;
using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace LanguageExt;

public static partial class ArrExtensions
{
    extension<A, B>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<B> operator *(Func<A, B> f, K<Arr, A> ma) =>
            +ma.Map(f);
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<B> operator *(K<Arr, A> ma, Func<A, B> f) =>
            +ma.Map(f);
    }
    
    extension<A, B, C>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, C>> operator * (
            Func<A, B, C> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, C>> operator * (
            K<Arr, A> ma,
            Func<A, B, C> f) =>
            curry(f) * ma;
    }
        
    extension<A, B, C, D>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, D>>> operator * (
            Func<A, B, C, D> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, D>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D> f) =>
            curry(f) * ma;
    }
            
    extension<A, B, C, D, E>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, E>>>> operator * (
            Func<A, B, C, D, E> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, E>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E> f) =>
            curry(f) * ma;
    }
                
    extension<A, B, C, D, E, F>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, F>>>>> operator * (
            Func<A, B, C, D, E, F> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, F>>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E, F> f) =>
            curry(f) * ma;
    }
                    
    extension<A, B, C, D, E, F, G>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, G>>>>>> operator * (
            Func<A, B, C, D, E, F, G> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, G>>>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E, F, G> f) =>
            curry(f) * ma;
    }    
                        
    extension<A, B, C, D, E, F, G, H>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, H>>>>>>> operator * (
            Func<A, B, C, D, E, F, G, H> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, H>>>>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E, F, G, H> f) =>
            curry(f) * ma;
    }
                        
    extension<A, B, C, D, E, F, G, H, I>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, I>>>>>>>> operator * (
            Func<A, B, C, D, E, F, G, H, I> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, I>>>>>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E, F, G, H, I> f) =>
            curry(f) * ma;
    }    
                        
    extension<A, B, C, D, E, F, G, H, I, J>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, J>>>>>>>>> operator * (
            Func<A, B, C, D, E, F, G, H, I, J> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, J>>>>>>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E, F, G, H, I, J> f) =>
            curry(f) * ma;
    }
                            
    extension<A, B, C, D, E, F, G, H, I, J, K>(K<Arr, A> _)
    {
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, Func<J, K>>>>>>>>>> operator * (
            Func<A, B, C, D, E, F, G, H, I, J, K> f, 
            K<Arr, A> ma) =>
            curry(f) * ma;
        
        /// <summary>
        /// Functor map operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, Func<J, K>>>>>>>>>> operator * (
            K<Arr, A> ma,
            Func<A, B, C, D, E, F, G, H, I, J, K> f) =>
            curry(f) * ma;
    }
}
