#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.

using System.Runtime.CompilerServices;
using IteratorPrototype.Memory;

namespace IteratorPrototype;

[SkipLocalsInit]
public readonly struct Sq<A>
{
    static readonly uint InitialItems = Pages<A>.ItemsPerPage * InitialPages;

    readonly int count = 0;     // 4 bytes
    readonly int currentPage;   // 4 bytes 
    readonly TreeList<A> pages; // 8 bytes (managed reference)
    readonly PageList<A> page0; // 16 bytes
    readonly PageList<A> page1; // 16 bytes
    readonly PageList<A> page2; // 16 bytes
    const int InitialPages = 3; // --------
                                // 64 bytes -- This is the size of a single cache-line.

    public Sq() =>
        page0 = PageList<A>.Alloc();

    Sq(in PageList<A> page0,
       int count,
       int currentPage)
    {
        this.page0 = page0;
        this.count = count;
        this.currentPage = currentPage;
    }

    Sq(in PageList<A> page0,
       in PageList<A> page1,
       int count,
       int currentPage)
    {
        this.page0 = page0;
        this.page1 = page1;
        this.count = count;
        this.currentPage = currentPage;
    }

    Sq(in PageList<A> page0,
       in PageList<A> page1,
       in PageList<A> page2,
       int count,
       int currentPage)
    {
        this.page0 = page0;
        this.page1 = page1;
        this.page2 = page2;
        this.count = count;
        this.currentPage = currentPage;
    }

    public ref readonly A this[int index]
    {
        get
        {
            if (index >= 0 && index < count)
            {
                return ref PageAt(index).At(index % (int)Pages<A>.ItemsPerPage);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    public int Count
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => count;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public LE.Option<A> At(int index) =>
        index >= 0 && index < count
            ? PageAt(index).At(index % (int)Pages<A>.ItemsPerPage)
            : default;

    [MethodImpl(Optimisations.InliningOnly)]
    ref PageList<A> PageAt(int index)
    {
        var bid = index / (int)Pages<A>.ItemsPerPage;
        return ref Unsafe.Add(ref Unsafe.AsRef(in page0), bid);
    }

    ref PageList<A> CurrentPage
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in page0), currentPage);
    }

    public Sq<A> Add(in A value)
    {
        if (currentPage < InitialPages)
        {
            ref var b = ref CurrentPage;
            if (b.IsFullOrDisposed)
            {
                return AddPage().Add(in value);
            }

            b.Add(in value, out var rb);

            // copy this structure for immutability
            var ns = this;

            // Get the new-page and update it with the returned page
            ref var nb = ref ns.CurrentPage;
            nb = rb;

            // Update the new-count
            ref var nc = ref Unsafe.AsRef(in ns.count);
            nc++;
            
            return ns;
        }
        else
        {
            // copy this structure for immutability
            var     ns = this;
            ref var np = ref Unsafe.AsRef(in ns.pages);
            ref var nc = ref Unsafe.AsRef(in ns.count);
            np = pages.Add(in value);
            nc++;
            return ns;
        }
    }

    Sq<A> AddPage()
    {
        var     newSeq   = this; //Copy
        ref var ncurrent = ref Unsafe.AsRef(in newSeq.currentPage);

        switch (currentPage)
        {
            case 0:
                if (page0.IsDisposed)
                {
                    // We get here when someone has done: `Sq<A> xs = default`
                    ref var ncount = ref Unsafe.AsRef(in newSeq.count);
                    ncount = 0;

                    ref var b0 = ref Unsafe.AsRef(in newSeq.page0);
                    b0 = PageList<A>.Alloc();
                }
                else
                {
                    ncurrent = 1;
                    ref var b1 = ref Unsafe.AsRef(in newSeq.page1);
                    b1 = PageList<A>.Alloc();
                }

                return newSeq;

            case 1:
                ncurrent = 2;
                ref var b2 = ref Unsafe.AsRef(in newSeq.page2);
                b2 = PageList<A>.Alloc();
                return newSeq;

            case 2:
                ncurrent = 3;
                ref var bs = ref Unsafe.AsRef(in newSeq.pages);
                bs = TreeList<A>.Empty;
                return newSeq;

            default:
                return newSeq;
        }
    }

    public override string ToString()
    {
        if (count == 0) return "[]";
        var sm = new StringMaker(stackalloc char[4096]);
        sm.Append("[");
        page0.AppendToStringMaker(ref sm);
        page1.AppendToStringMaker(ref sm);
        page2.AppendToStringMaker(ref sm);
        pages.AppendToStringMaker(ref sm);

        if (sm.Length == 1)
        {
            sm.Append("]");
        }
        else
        {
            sm.Undo(2);
            sm.Append("]");
        }

        return sm.ToString();
    }
}

