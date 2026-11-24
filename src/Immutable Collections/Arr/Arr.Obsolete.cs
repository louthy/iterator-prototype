using LanguageExt.Traits;
using IteratorPrototype.Traits;

namespace IteratorPrototype;

public sealed partial class Arr<A>
{
    [Obsolete("Use Prepend instead")]
    public Arr<A> Cons(in A value) =>
        Prepend(value);
    
    [Obsolete("Use PrependRange instead")]
    public Arr<A> ConsRange(in ReadOnlySpan<A> range) => 
        PrependRange(in range);

    [Obsolete("Use PrependRange instead")]
    public Arr<A> ConsRange(IEnumerable<A> range) => 
        PrependRange(range);

    [Obsolete("Use PrependRange instead")]
    public Arr<A> ConsRange(Arr<A> range) => 
        PrependRange(range);

    [Obsolete("Use PrependRange instead")]
    public Arr<A> ConsRange<T>(K<T, A> range) 
        where T : Iterable<T> =>
        PrependRange(range);
}
