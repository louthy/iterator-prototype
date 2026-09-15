using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

readonly partial struct TreeList<A>
{
    [SkipLocalsInit]
    class EmptyNode : Node
    {
        public static readonly Node Default = new EmptyNode();
        
        EmptyNode() : base(0, 0, true, 0){}

        internal override string NodeInfo =>
            "empty";

        internal override ref A At(uint index) =>
            throw new IndexOutOfRangeException();

        internal override bool Add(A item, out Node node)
        {
            node = Leaf.Alloc(item);
            return true;
        }

        internal override bool Add(ReadOnlySpan<A> items, out Node node)
        {
            if(items.Length > Pages<A>.ItemsPerPage)
            {
                // TODO: Support for larger initial items
                throw new NotImplementedException();
            }
            node = Leaf.Alloc(items);
            return true;
        }

        public override string ToString() =>
            "[]";

        internal override void AppendToStringMaker(ref StringMaker sm)
        {
        }

        internal override void Dispose() {}
    }
}