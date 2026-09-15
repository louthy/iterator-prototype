#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

public readonly partial struct TreeList<A>
{
    readonly struct ChildNodes
    {
        public const uint Capacity = 7;

        public readonly Node? Node0;
        public readonly Node? Node1;
        public readonly Node? Node2;
        public readonly Node? Node3;
        public readonly Node? Node4;
        public readonly Node? Node5;
        public readonly Node? Node6;
        
        public ref Node? this[uint index] =>
            ref Unsafe.Add(ref Unsafe.AsRef(in Node0), index);
        
        public ref Node? this[int index] =>
            ref Unsafe.Add(ref Unsafe.AsRef(in Node0), index);
        
        public ref Node AtAllocated(uint index) =>
            ref Unsafe.Add(ref Unsafe.AsRef(in Node0), index)!;
        
        public ref Node AtAllocated(int index) =>
            ref Unsafe.Add(ref Unsafe.AsRef(in Node0), index)!;
        
        public ref Node? At(uint index) =>
            ref Unsafe.Add(ref Unsafe.AsRef(in Node0), index);
        
        public ref Node? At(int index) =>
            ref Unsafe.Add(ref Unsafe.AsRef(in Node0), index);
        
        public Span<Node?> All =>
            MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in Node0), (int)Capacity);

        public Span<Node> Allocated(int count) =>
            MemoryMarshal.CreateSpan(ref Unsafe.AsRef(in Node0), count)!;
    }
}