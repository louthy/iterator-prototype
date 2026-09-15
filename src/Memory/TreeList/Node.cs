namespace IteratorPrototype.Memory;

readonly partial struct TreeList<A>
{
    public abstract class Node
    {
        public readonly uint Count;        // 4 bytes      
        public readonly ushort InnerCount; // 2 bytes
        public readonly bool IsFull;       // 1 byte
        public readonly byte Depth;        // 1 byte

        internal Node(uint count, ushort innerCount, bool isFull, byte depth)
        {
            Count = count;
            InnerCount = innerCount;
            IsFull = isFull;
            Depth = depth;
        }

        internal abstract string NodeInfo { get; }
        internal abstract ref A At(uint index);
        internal abstract bool Add(A item, out Node node);
        internal abstract bool Add(ReadOnlySpan<A> items, out Node node);
        internal abstract void AppendToStringMaker(ref StringMaker sm);
        internal abstract void Dispose();
    }
}