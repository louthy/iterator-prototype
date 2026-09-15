#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using System.Runtime.CompilerServices;

namespace IteratorPrototype.Memory;

readonly partial struct TreeList<A>
{
    [SkipLocalsInit]
    class Inner : Node
    {
        public readonly ChildNodes Nodes;

        Inner(uint count, ushort innerCount, bool isFull, byte depth) :
            base(count, innerCount, isFull, depth)
        {
        }

        Inner(uint count, ushort innerCount, bool isFull, byte depth, params ReadOnlySpan<Node?> nodes) :
            base(count, innerCount, isFull, depth)
        {
            nodes.CopyTo(All);
        }

        Inner(uint count, ushort innerCount, bool isFull, params ReadOnlySpan<Node?> nodes) :
            base(count, innerCount, isFull, CalculateDepth(nodes))
        {
            nodes.CopyTo(All);
        }

        internal Inner(Node node0) : 
            base(node0.Count, 1, false, (byte)(node0.Depth + 1))
        {
            Nodes[0] = node0;
        }

        internal Inner(Node node0, Node node1) : 
            base(node0.Count + node1.Count, 2, false, (byte)(Math.Max(node0.Depth, node1.Depth) + 1))
        {
            Nodes[0] = node0;
            Nodes[1] = node1;
        }

        static byte CalculateDepth(params ReadOnlySpan<Node?> nodes)
        {
            var depth = 0;
            foreach (var node in nodes)
            {
                if (node?.Depth > depth)
                {
                    depth = node.Depth;
                }
            }
            return (byte)(depth + 1);
        }

        Span<Node?> All =>
            Nodes.All;

        Span<Node> Allocated =>
            Nodes.Allocated(InnerCount);

        internal override ref A At(uint index)
        {
            foreach (var node in Allocated)
            {
                if (index < node.Count)
                {
                    return ref node.At(index);
                }
                else
                {
                    index -= node.Count;
                }
            }

            throw new IndexOutOfRangeException();
        }

        internal override bool Add(A value, out Node node)
        {
            if (InnerCount == 0)
            {
                // TODO: Consider always initialising with at least one leaf, so we don't need this check            
                // There's nothing in this inner-node, so create a new leaf-node and add it as the
                // first child of this inner-node
                var leaf = Leaf.Alloc(value);
                node = new Inner(1, 1, false, leaf);
                return true;
            }

            var     current = InnerCount - 1;
            ref var child   = ref Nodes.AtAllocated(current);

            if (child.Add(value, out var nchild))
            {
                // The child has space
                var newIsFull = nchild.IsFull && InnerCount == ChildNodes.Capacity;
                var newCount  = Count + 1;
                var newInner  = new Inner((uint)newCount, InnerCount, newIsFull, Depth);

                ref var newNodes = ref Unsafe.AsRef(in newInner.Nodes);
                newNodes = Nodes;
                newNodes[current] = nchild;
                node = newInner;
                
                return true;
            }
            else if (child.Depth + 1 < Depth)
            {
                // The child has run out of space, but isn't as tall as the other child nodes, so let it grow.
                var newCount  = Count + 1;
                var nchild1   = new Inner(child, Leaf.Alloc(value));
                var newIsFull = nchild1.IsFull && InnerCount == ChildNodes.Capacity;
                var newInner  = new Inner((uint)newCount, InnerCount, newIsFull, Depth);

                ref var newNodes = ref Unsafe.AsRef(in newInner.Nodes);
                newNodes = Nodes;
                newNodes[current] = nchild1;
                node = newInner;
                
                return true;
            }
            else if (InnerCount < ChildNodes.Capacity)
            {
                // The child has no space, so create a sibling with space
                var nchild1       = Leaf.Alloc(value);
                var newInnerCount = InnerCount + 1;
                var newIsFull     = nchild1.IsFull && newInnerCount == ChildNodes.Capacity;
                var newCount      = Count + 1;
                var newInner      = new Inner((uint)newCount, (ushort)newInnerCount, newIsFull, Depth);

                ref var newNodes = ref Unsafe.AsRef(in newInner.Nodes);
                newNodes = Nodes;
                newNodes[InnerCount] = nchild1;
                node = newInner;
                
                return true;
            }
            else
            {
                // The child has no space and there's no space for siblings!
                node = this;
                return false;
            }            
        }

        internal override bool Add(ReadOnlySpan<A> values, out Node node)
        {
            if (values.IsEmpty)
            {
                node = this;
                return true;
            }
            
            if (InnerCount == 0)
            {
                // TODO: Consider always initialising with at least one leaf, so we don't need this check            
                // There's nothing in this inner-node, so create a new leaf-node and add it as the
                // first child of this inner-node
                var leaf = Leaf.Alloc(values);
                node = new Inner((uint)values.Length, 1, false, leaf);
                return true;
            }

            var     current = InnerCount - 1;
            ref var child   = ref Nodes.AtAllocated(current);

            if (child.Add(values, out var nchild))
            {
                // The child has space
                var newIsFull = nchild.IsFull && InnerCount == ChildNodes.Capacity;
                var newCount  = Count + values.Length;
                var newInner  = new Inner((uint)newCount, InnerCount, newIsFull, Depth);

                ref var newNodes = ref Unsafe.AsRef(in newInner.Nodes);
                newNodes = Nodes;
                newNodes[current] = nchild;
                node = newInner;
                
                return true;
            }
            else if (child.Depth + 1 < Depth)
            {
                // The child has run out of space, but isn't as tall as the other child nodes, so let it grow.
                var newCount  = Count + values.Length;
                var nchild1   = new Inner(child, Leaf.Alloc(values));
                var newIsFull = nchild1.IsFull && InnerCount == ChildNodes.Capacity;
                var newInner  = new Inner((uint)newCount, InnerCount, newIsFull, Depth);

                ref var newNodes = ref Unsafe.AsRef(in newInner.Nodes);
                newNodes = Nodes;
                newNodes[current] = nchild1;
                node = newInner;
                
                return true;
            }
            else if (InnerCount < ChildNodes.Capacity)
            {
                // The child has no space, so create a sibling with space
                var nchild1       = Leaf.Alloc(values);
                var newInnerCount = InnerCount + 1;
                var newIsFull     = nchild1.IsFull && newInnerCount == ChildNodes.Capacity;
                var newCount      = Count + values.Length;
                var newInner      = new Inner((uint)newCount, (ushort)newInnerCount, newIsFull, Depth);

                ref var newNodes = ref Unsafe.AsRef(in newInner.Nodes);
                newNodes = Nodes;
                newNodes[InnerCount] = nchild1;
                node = newInner;
                
                return true;
            }
            else
            {
                // The child has no space and there's no space for siblings!
                node = this;
                return false;
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
            foreach (var node in Allocated)
            {
                node.AppendToStringMaker(ref sm);
            }
        }

        internal override string NodeInfo
        {
            get
            {
                var sm = new StringMaker(stackalloc char[512]);
                sm.Append("[");
                foreach (var node in Allocated)
                {
                    switch (node)
                    {
                        case Inner inner:
                            sm.Append(inner.NodeInfo);
                            break;
                        
                        case Leaf leaf:
                            sm.Append($"leaf : {leaf.Count}");
                            break;
                    }
                    sm.Append(", ");
                }
                if (sm.Length > 1)
                {
                    sm.Undo(2);
                }
                sm.Append("]");
                return sm.ToString();
            }
        }
        

        internal override void Dispose()
        {
            // Takes a copy of the current nodes and node-count so that we can dispose of
            // the nodes without holding a lock
            var innerCount = InnerCount; 
            var nodes      = Nodes;      
            
            // Take a reference to the Count field (which should be the first 4 bytes in the Node)
            // Convert it to a long to capture the InnerCount (2 bytes) and IsFull (1 byte) and IsLeaf (1 byte)
            ref var headerBytes = ref Unsafe.As<uint, ulong>(ref Unsafe.AsRef(in Count));
            var     headerValue = headerBytes;
            
            if (Interlocked.CompareExchange(ref Unsafe.AsRef(in headerBytes), 0, headerValue) == headerValue)
            {
                // We've now managed to zero the entire header bytes, which means:
                //
                //  Count == 0
                //  InnerCount == 0
                //  IsFull == false
                //  IsLeaf == false
                //
                // So, this structure is now safe to be re-used, and we can now safely dispose of the nodes...
                
                for(var ix = 0; ix < innerCount; ix++)
                {
                    nodes.AtAllocated(ix).Dispose();
                    nodes[ix] = null!;
                }
            }
        }

        /*
        [MethodImpl(Optimisations.InliningOnly)]
        public Enumerator GetEnumerator() =>
            new (this, 0, Capacity);

        [MethodImpl(Optimisations.InliningOnly)]
        public Enumerator GetEnumerator(int take) =>
            take == 0
                ? new(this, 0, 0)
                : new(this, 0, Math.Min(take, Math.Min(take, Capacity)));

        [MethodImpl(Optimisations.InliningOnly)]
        public Enumerator GetEnumerator(int skip, int take) =>
            skip >= Capacity
                ? new(this, 0, 0)
                : new(this, skip, Math.Min(take, Capacity - skip));

    */
    }
}