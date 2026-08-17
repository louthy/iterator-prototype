using System.Runtime.CompilerServices;
using IteratorPrototype.Traits;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct IteratorFieldsMutable<T, IS, A>
    where T : IterableImmutable<T, IS>
    where IS : struct
{
    public IteratorTag tag;
    public A head;
    public K<T, A> ta;
    public Func<Iterator<A>>? lazy;
    public VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public IS space;
}