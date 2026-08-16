using System;
using LanguageExt.Traits;
using static LanguageExt.Prelude;

namespace LanguageExt;

public static partial class ArrExtensions
{
    extension<A, B>(K<Arr, A> self)
    {
        
        /// <summary>
        /// Applicative sequence operator
        /// </summary>
        public static Arr<B> operator >>> (K<Arr, A> ma, K<Arr, B> mb) =>
            ma.Action(mb).As();
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<B> operator * (K<Arr, Func<A, B>> mf, K<Arr, A> ma) =>
            mf.Apply(ma);
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<B> operator * (K<Arr, A> ma, K<Arr, Func<A, B>> mf) =>
            mf.Apply(ma);        
    }
    
    extension<A, B, C>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, C>> operator * (
            K<Arr, Func<A, B, C>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, C>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C>> mf) =>
            curry * mf * ma;
    }
        
    extension<A, B, C, D>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, D>>> operator * (
            K<Arr, Func<A, B, C, D>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;

        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, D>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D>> mf) =>
            curry * mf * ma;
    }
            
    extension<A, B, C, D, E>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, E>>>> operator * (
            K<Arr, Func<A, B, C, D, E>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, E>>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E>> mf) =>
            curry * mf * ma;
    }
                
    extension<A, B, C, D, E, F>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, F>>>>> operator * (
            K<Arr, Func<A, B, C, D, E, F>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, F>>>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E, F>> mf) =>
            curry * mf * ma;
    }
                    
    extension<A, B, C, D, E, F, G>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, G>>>>>> operator * (
            K<Arr, Func<A, B, C, D, E, F, G>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, G>>>>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E, F, G>> mf) =>
            curry * mf * ma;
    }
                    
    extension<A, B, C, D, E, F, G, H>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, H>>>>>>> operator * (
            K<Arr, Func<A, B, C, D, E, F, G, H>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, H>>>>>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E, F, G, H>> mf) =>
            curry * mf * ma;
    }
                        
    extension<A, B, C, D, E, F, G, H, I>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, I>>>>>>>> operator * (
            K<Arr, Func<A, B, C, D, E, F, G, H, I>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, I>>>>>>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E, F, G, H, I>> mf) =>
            curry * mf * ma;
    }
                            
    extension<A, B, C, D, E, F, G, H, I, J>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, J>>>>>>>>> operator * (
            K<Arr, Func<A, B, C, D, E, F, G, H, I, J>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;
        
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, J>>>>>>>>> operator * (
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E, F, G, H, I, J>> mf) =>
            curry * mf * ma;
    }
                                
    extension<A, B, C, D, E, F, G, H, I, J, K>(K<Arr, A> self)
    {
        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, Func<J, K>>>>>>>>>> operator * (
            K<Arr, Func<A, B, C, D, E, F, G, H, I, J, K>> mf, 
            K<Arr, A> ma) =>
            curry * mf * ma;

        /// <summary>
        /// Applicative apply operator
        /// </summary>
        public static Arr<Func<B, Func<C, Func<D, Func<E, Func<F, Func<G, Func<H, Func<I, Func<J, K>>>>>>>>>> operator *(
            K<Arr, A> ma,
            K<Arr, Func<A, B, C, D, E, F, G, H, I, J, K>> mf) =>
            curry * mf * ma;
    }
}
