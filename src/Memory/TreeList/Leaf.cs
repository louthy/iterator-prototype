using System.Runtime.CompilerServices;
using IteratorPrototype.Types;

namespace IteratorPrototype.Memory;

readonly partial struct TreeList<A>
{
    [SkipLocalsInit]
    class Leaf : Node
    {
        readonly PageRef<A> page; // 8 bytes (managed-reference)

        Leaf(in PageRef<A> page, ushort count) :
            base(count, count, count >= Pages<A>.ItemsPerPage, 1) =>
            this.page = page;

        /*
         This frees too early because of the Leaf churn. We probably need reference counting.
         
        ~Leaf()
        {
            page.Dispose();
        }
        */

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Leaf Alloc() =>
            new (PageAlloc(), 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static PageRef<A> PageAlloc() =>
            Pages<A>.alwaysAlloc();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Leaf Alloc(A item)
        {
            var p = PageAlloc();
            p.SetVersion(0, 1);
            p[0] = item;
            return new Leaf(p, 1);
        }

        public static Leaf Alloc(params ReadOnlySpan<A> items)
        {
            if(items.Length == 0)
            {
                return Alloc();
            }

            if (items.Length > Pages<A>.ItemsPerPage)
            {
                throw new ArgumentException(
                    $"Too many initial items for a leaf-node of {Ty<A>.Pretty}, limit is {Pages<A>.ItemsPerPage}");
            }

            var p = PageAlloc();
            p.SetVersion(0, items.Length);
            items.CopyTo(p.AsSpan());
            return new Leaf(p, (ushort)items.Length);
        }

        internal override string NodeInfo =>
            $"leaf : {Count}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal override ref A At(uint index) =>
            ref page[index];

        internal override bool Add(A item, out Node node)
        {
            var ncount = Count + 1;
            if (ncount > Pages<A>.ItemsPerPage)
            {
                node = this;
                return false;
            }

            if (page.SetVersion(Count, ncount))
            {
                page[Count] = item;
                node = new Leaf(page, (ushort)ncount);
                return true;
            }
            else
            {
                var npage = PageAlloc();
                page.AsSpan(0, Count).CopyTo(npage.AsSpan());
                page[Count] = item;
                node = new Leaf(page, (ushort)ncount);
                return true;
            }
        }

        internal override bool Add(ReadOnlySpan<A> items, out Node node)
        {
            var ncount = (uint)(Count + items.Length);
            if (ncount > Pages<A>.ItemsPerPage)
            {
                node = this;
                return false;
            }

            if (page.SetVersion(Count, ncount))
            {
                items.CopyTo(page.AsSpan(Count));
                node = new Leaf(page, (ushort)ncount);
                return true;
            }
            else
            {
                var npage = PageAlloc();
                page.AsSpan(0, Count).CopyTo(npage.AsSpan());
                items.CopyTo(page.AsSpan(Count));
                node = new Leaf(page, (ushort)ncount);
                return true;
            }
        }

        public override string ToString()
        {
            if (Count == 0 || InnerCount == 0) return "[]";
            var sm = new StringMaker(stackalloc char[4096]);
            sm.Append("[");
            AppendToStringMaker(ref sm);
            sm.Append("]");
            return sm.ToString();
        }

        internal override void AppendToStringMaker(ref StringMaker sm)
        {
            if (Count == 0 || InnerCount == 0) return;
            var len = sm.Length;
            foreach (var node in page.AsSpan(0, Count))
            {
                sm.Append(node);
                sm.Append(", ");
            }

            if (sm.Length > len)
            {
                // Remove trailing comma and space
                sm.Undo(2);
            }
        }

        internal override void Dispose() =>
            page.Dispose();

        public Page<A>.Enumerator GetEnumerator() =>
            new(page.Page, 0, (int)Count);
    }
}