using System.Runtime.CompilerServices;

namespace IteratorPrototype;

[SkipLocalsInit]
public ref struct IteratorFieldsMutable<A>
{
    public IteratorTag tag;
    public A head;
    public object? ta;
    public Func<Iterator<A>>? lazy;
    public VirtualTable<A>? vt; //< Used, do not remove (it supports casting between Iterator<T, TS, A> and Iterator<A>)
    public Space128 space;
}