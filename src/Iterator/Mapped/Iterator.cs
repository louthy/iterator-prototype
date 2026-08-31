using System.Runtime.CompilerServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Iterator<T, IS, A, B>
    where T : Tr.IterableImmutable<T, IS>
    where IS : unmanaged
{
    readonly MiniStack<IteratorFields<T, IS, A, B>> fields;

    /*
    [MethodImpl(Optimisations.Default)]
    internal Iterator(K<T, A> ta, IteratorAction<B> action, in IS space) =>
        fields = new IteratorFields<T, IS, A, B>(ta, action, in space);
        */

    [MethodImpl(Optimisations.Default)]
    internal Iterator(in IteratorFields<T, IS, A, B> entry) =>
        fields = MiniStack.singleton(in entry);

    [MethodImpl(Optimisations.Default)]
    internal Iterator(in MiniStack<IteratorFields<T, IS, A, B>> fields) =>
        this.fields = fields;

    public Iterator<B> Lower
    {
        [MethodImpl(Optimisations.Default)]
        get
        {
            ref var fs = ref fields.Cast<IteratorFields<T, IS, A, B>, IteratorFields<B>>();
            return new(in fs);
        }
    }

    [MethodImpl(Optimisations.Default)]
    public bool TryGetValue(out B head, out Iterator<T, IS, A, B> tail)
    {
        tail = this; // Copy

        ref var fs  = ref Unsafe.AsRef(in tail.fields);
        ref var top = ref fs.Peek();
        if (top.action is null)
        {
            throw new InvalidOperationException("action is null, which means A -> B can't be done");
        }
        else
        {
            ref var a = ref Unsafe.AsRef(in top.action);
            return a.TryGetValue(ref fs.Cast<IteratorFields<T, IS, A, B>, IteratorFields>(), out head);
        }        
    }
}
