using Range = System.Range;

namespace IteratorPrototype;

public partial class Array
{
    public static Array<A> create<A>(params ReadOnlySpan<A> items) =>
        new ([.. items]);
    
    public static Array<int> create(Range items) =>
        create (LanguageExt.Range.fromMinMax(items.Start.Value, items.End.Value).AsEnumerable());
    
    public static Array<A> create<A>(IEnumerable<A> items) =>
        new ([.. items]);
}
