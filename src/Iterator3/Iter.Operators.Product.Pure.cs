using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3;

public static partial class IterOperators
{
    extension<A, B>(IterProduct<A, B>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second)> operator | (in IterProduct<A, B> lhs, in IterPure _) =>
            lhs.ToIterator();
    }

    extension<A, B, C>(IterProduct<A, B, C>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third)> operator | (in IterProduct<A, B, C> lhs, in IterPure _) =>
            lhs.ToIterator();
    }

    extension<A, B, C, D>(IterProduct<A, B, C, D>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth)> operator | (in IterProduct<A, B, C, D> lhs, in IterPure _) =>
            lhs.ToIterator();
    }

    extension<A, B, C, D, E>(IterProduct<A, B, C, D, E>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth)> operator | (in IterProduct<A, B, C, D, E> lhs, in IterPure _) =>
            lhs.ToIterator();
    }
    
    extension<A, B, C, D, E, F>(IterProduct<A, B, C, D, E, F>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth)> operator | (in IterProduct<A, B, C, D, E, F> lhs, in IterPure _) =>
            lhs.ToIterator();
    }
        
    extension<A, B, C, D, E, F, G>(IterProduct<A, B, C, D, E, F, G>)
    {
        [MethodImpl(Optimisations.Default)]
        public static Iter<(A First, B Second, C Third, D Fourth, E Fifth, F Sixth, G Seventh)> operator | (in IterProduct<A, B, C, D, E, F, G> lhs, in IterPure _) =>
            lhs.ToIterator();
    }
}