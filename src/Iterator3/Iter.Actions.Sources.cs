#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype.Iterator3;

[SkipLocalsInit]
public readonly record struct Iter<T, IS, A>(in K<T, A> ta)
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    public Iter<A> ToIterator()
    {
        var frame = Iter<A>.Default(out var iter);
        return Push.iterable<T, IS, A>(in frame, ta)
                   ? iter
                   : default;
    }
    
    internal Iter<B> Map<B>(Func<A, B> f)
    {
        var frame = Iter<B>.Default(out var iter);
        return Push.iterable<T, IS, A>(in frame, ta) &&
               Push.map(in frame, f)
                   ? iter
                   : default;
    }
}
