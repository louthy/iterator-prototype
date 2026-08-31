#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
public readonly struct ObjStack
{
    public const int Capacity = 16;
    
    public readonly object Object00;
    public readonly object Object01;
    public readonly object Object02;
    public readonly object Object03;
    public readonly object Object04;
    public readonly object Object05;
    public readonly object Object06;
    public readonly object Object07;
    public readonly object Object08;
    public readonly object Object09;
    public readonly object Object0A;
    public readonly object Object0B;
    public readonly object Object0C;
    public readonly object Object0D;
    public readonly object Object0E;
    public readonly object Object0F;
    public readonly int Count;
    
    public ref object this[int index]
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Object00), index);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Add(in ObjStack rhs)
    {
        if(rhs.Count + Count > Capacity) return false;
        var     sizeOfPtr = Unsafe.SizeOf<nint>();
        var     srcSize   = (uint)(rhs.Count * sizeOfPtr);
        ref var dobj      = ref Unsafe.AsRef(in Object00);
        ref var dest      = ref Unsafe.AddByteOffset(ref Unsafe.As<object, byte>(ref dobj), rhs.Count * sizeOfPtr);
        ref var sobj      = ref Unsafe.AsRef(in rhs.Object00);
        ref var src       = ref Unsafe.As<object, byte>(ref sobj);
        
        Unsafe.CopyBlock(ref dest, ref src, srcSize);
        return true;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool PopToTop(int top)
    {
        if(top > Count) return false;
        if(top == Count) return true;
        
        ref var tref  = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), tref - 1);
        
        while(tref > top)
        {
            tref--;
            entry = null!;
            entry = ref Unsafe.Add(ref entry, -1);
        }
        return true;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool Pop()
    {
        ref var top = ref Unsafe.AsRef(in Count);
        top--;
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = null!;
        return true;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool Pop<A>(out A value)
    {
        ref var top = ref Unsafe.AsRef(in Count);
        top--;
        ref var entry = ref Unsafe.As<object, A>(ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top));
        value = entry;
        entry = default!;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Peek<A>(out A value)
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), Count - 1);
        value = Unsafe.As<object, A>(ref entry);
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public ref A PeekAt<A>()
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), Count - 1);
        return ref Unsafe.As<object, A>(ref entry);
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool Push<A>(in A value)
        where A : class 
    {
        if(Count == Capacity) return false;
        ref var top = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        entry = value;
        top++;
        return true;
    }
        
    [MethodImpl(Optimisations.Default)]
    public bool Prepend<A>(in A value)
        where A : class
    {
        if(Count == Capacity) return false;
        ref var top  = ref Unsafe.AsRef(in Count);
        ref var src  = ref Unsafe.AsRef(in Object00);
        ref var dest = ref Unsafe.Add(ref src, 1);

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(ref Unsafe.As<object, byte>(ref dest), ref Unsafe.As<object, byte>(ref src), (uint)(Count * Unsafe.SizeOf<nint>()));
        src = value;
        top++;
        return true;
    }
}
