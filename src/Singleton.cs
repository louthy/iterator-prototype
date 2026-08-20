using LanguageExt.Traits;

namespace IteratorPrototype;

record Singleton<A>(A Value) : K<Singleton, A>
{
    
}

public class Singleton : Tr.Iterable<Singleton>
{
    public static Iterator<A> Forward<A>(K<Singleton, A> ta)
    {
        throw new NotImplementedException();
    }
}