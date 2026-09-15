namespace IteratorPrototype.Memory;

readonly partial struct TreeList<A> : IDisposable
{
    readonly Node root;

    TreeList(Node root) =>
        this.root = root;

    public static readonly TreeList<A> Empty =
        new(EmptyNode.Default);

    public int Count => 
        (int)root.Count;

    public ref A this[uint index] =>
        ref At(index);

    public ref A this[int index] =>
        ref At(index);

    public ref A this[Index index] =>
        ref At(index.GetOffset(Count));
    
    public ref A At(uint index) =>
        ref root.At(index);

    public ref A At(int index)
    {
        if (index < 0) throw new IndexOutOfRangeException();
        return ref root.At((uint)index);
    }

    public TreeList<A> Add(in A item)
    {
        if (root.Add(item, out var node))
        {
            return new TreeList<A>(node); // Add succeeded
        }
        else if(root.IsFull)
        {
            // The Add failed, which means we need a new level
            var nroot = new Inner(node, Leaf.Alloc(item));
            return new TreeList<A>(nroot);
        }
        else
        {
            throw new InvalidOperationException("Add failed");
        }
    }

    public TreeList<A> Add(params ReadOnlySpan<A> items)
    {
        if(items.Length == 0) return this;
        var ipp  = Pages<A>.ItemsPerPage;
        var node = root;
        
        while (items.Length > 0)
        {
            var remain = Math.Min(items.Length, (int)(ipp - node.Count % ipp));
            if(remain == 0) break;
            var window = items[..remain];
            items = items[remain..];
            if (!node.Add(window, out node))
            {
                // The Add failed, which means we need a new level
                node = new Inner(node, Leaf.Alloc(window));
            }
        }
        return new TreeList<A>(node);
    }

    internal string NodeInfo =>
        root.NodeInfo;
    
    public override string ToString()
    {
        if (Count == 0) return "[]";
        var sm = new StringMaker(stackalloc char[4096]);
        sm.Append("[");
        AppendToStringMaker(ref sm);
        sm.Append("]");
        return sm.ToString();
    }

    internal void AppendToStringMaker(ref StringMaker sm) =>
        root.AppendToStringMaker(ref sm);
    
    public void Dispose() =>
        root.Dispose();
}