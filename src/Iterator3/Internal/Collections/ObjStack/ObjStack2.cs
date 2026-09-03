#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
public readonly struct ObjStack2
{
    public const int Capacity = 16;

    public readonly object Object00, Object100;
    public readonly object Object01, Object101;
    public readonly object Object02, Object102;
    public readonly object Object03, Object103;
    public readonly object Object04, Object104;
    public readonly object Object05, Object105;
    public readonly object Object06, Object106;
    public readonly object Object07, Object107;
    public readonly object Object08, Object108;
    public readonly object Object09, Object109;
    public readonly object Object0A, Object10A;
    public readonly object Object0B, Object10B;
    public readonly object Object0C, Object10C;
    public readonly object Object0D, Object10D;
    public readonly object Object0E, Object10E;
    public readonly object Object0F, Object10F;
    public readonly int Count;

    public ref object this[int index]
    {
        [MethodImpl(Optimisations.InliningOnly)] 
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Object00), index << 1);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Add(in ObjStack2 rhs)
    {
        if (rhs.Count + Count > Capacity) return false;
        
        var     sizeOfPtr = Unsafe.SizeOf<nint>() << 1;
        var     srcSize   = (uint)(rhs.Count * sizeOfPtr);
        
        ref var dobj      = ref Unsafe.AsRef(in Object00);
        ref var dest      = ref Unsafe.AddByteOffset(ref Unsafe.As<object, byte>(ref dobj), rhs.Count * sizeOfPtr);
        
        ref var sobj      = ref Unsafe.AsRef(in rhs.Object00);
        ref var src       = ref Unsafe.As<object, byte>(ref sobj);

        Unsafe.CopyBlock(ref dest, ref src, srcSize);
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool PopToTop(int newTop)
    {
        if (newTop > Count) return false;
        if (newTop == Count) return true;

        ref var count = ref Unsafe.AsRef(in Count);
        var     top   = (count - 1) << 2;
        
        ref var entry0 = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), top);
        ref var entry1 = ref Unsafe.Add(ref entry0, 1);

        while (count > newTop)
        {
            count--;

            entry0 = null!;
            entry1 = null!;
            
            entry0 = ref Unsafe.Add(ref entry0, -2);
            entry1 = ref Unsafe.Add(ref entry1, -2);
        }

        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Pop()
    {
        var     count2 = Count << 1;
        ref var entry0 = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), count2);
        ref var entry1 = ref Unsafe.Add(ref entry0, 1);

        entry0 = null!;
        entry1 = null!;
        
        ref var top = ref Unsafe.AsRef(in Count);
        top--;
        
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Pop<A>(out A value)
        where A : class
    {
        var     count2 = Count << 1;
        ref var entry0 = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), count2);
        ref var entry1 = ref Unsafe.Add(ref entry0, 1);
        ref var entry  = ref Unsafe.As<object, A>(ref entry0);
        value = entry;

        entry0 = null!;
        entry1 = null!;
        
        ref var top = ref Unsafe.AsRef(in Count);
        top--;
        
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Peek<A>(out A value)
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), (Count - 1) << 1);
        value = Unsafe.As<object, A>(ref entry);
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool DeclaredPeek<A>(out A value)
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object100), (Count - 1) << 1);
        value = Unsafe.As<object, A>(ref entry);
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A PeekAt<A>()
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), (Count - 1) << 1);
        return ref Unsafe.As<object, A>(ref entry);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A DeclaredPeekAt<A>()
        where A : class
    {
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Object100), (Count - 1) << 1);
        return ref Unsafe.As<object, A>(ref entry);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Push<A>(in A value)
        where A : class =>
        Push(in value, in value, out _);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Push<A>(in A value, out ushort ix)
        where A : class =>
        Push(in value, in value, out ix);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool Push<A>(in A variable, in A declared)
        where A : class =>
        Push(in variable, in declared, out _);

    [MethodImpl(Optimisations.Default)]
    public bool Push<A>(in A variable, in A declared, out ushort ix)
        where A : class
    {
        if (Count == Capacity)
        {
            ix = ushort.MinValue;
            return false;
        }
        ix = (ushort)Count;

        var     count2 = Count << 1;
        ref var entry0 = ref Unsafe.Add(ref Unsafe.AsRef(in Object00), count2);
        ref var entry1 = ref Unsafe.Add(ref entry0, 1);
        
        entry0 = variable;
        entry1 = declared;
        
        ref var top = ref Unsafe.AsRef(in Count);
        top++;
        
        return true;
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool Prepend<A>(in A variable)
        where A : class =>
        Prepend(in variable, in variable, out _);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool Prepend<A>(in A variable, out ushort ix)
        where A : class =>
        Prepend(in variable, in variable, out ix);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool Prepend<A>(in A variable, in A declared)
        where A : class =>
        Prepend(in variable, in declared, out _);

    [MethodImpl(Optimisations.Default)]
    public bool Prepend<A>(in A variable, in A declared, out ushort ix)
        where A : class
    {
        var count = Count;
        if (count == Capacity)
        {
            ix = ushort.MinValue;
            return false;
        }
        ix = (ushort)Count;
        
        ref var src0      = ref Unsafe.AsRef(in Object00);
        ref var src1      = ref Unsafe.AsRef(in Object100);
        ref var dest      = ref Unsafe.AsRef(in Object01);
        var     blockSize = (uint)((count << 1) * Unsafe.SizeOf<nint>());

        // TODO: Make sure CopyBlock can handle overlapping memory regions
        Unsafe.CopyBlock(
            ref Unsafe.As<object, byte>(ref dest),
            ref Unsafe.As<object, byte>(ref src0), 
            blockSize);

        src0 = variable;
        src1 = declared;

        ref var top = ref Unsafe.AsRef(in Count);
        top++;
        
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref object At(int index) =>
        ref Unsafe.Add(ref Unsafe.AsRef(in Object00), index << 1);

    [MethodImpl(Optimisations.InliningOnly)]
    public bool At(int index, out object value)
    {
        if (index < Count)
        {
            ref var item = ref At(index);
            value = item;
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A At<A>(int index) 
        where A : class =>
        ref Unsafe.As<object, A>(ref At(index));

    [MethodImpl(Optimisations.InliningOnly)]
    public bool At<A>(int index, out A value)
        where A : class
    {
        if (index < Count)
        {
            ref var item = ref At<A>(index);
            value = item;
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref object DeclaredAt(int index) =>
        ref Unsafe.Add(ref Unsafe.AsRef(in Object100), index << 1);

    [MethodImpl(Optimisations.Default)]
    public bool DeclaredAt(int index, out object value)
    {
        if (index < Count)
        {
            ref var item = ref DeclaredAt(index);
            value = item;
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }

    [MethodImpl(Optimisations.Default)]
    public ref A DeclaredAt<A>(int index) 
        where A : class =>
        ref Unsafe.As<object, A>(ref DeclaredAt(index));
    
    [MethodImpl(Optimisations.Default)]
    public bool DeclaredAt<A>(int index, out A value)
        where A : class
    {
        if (index < Count)
        {
            ref var item = ref DeclaredAt<A>(index);
            value = item;
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }
    
    [MethodImpl(Optimisations.InliningOnly)]
    public bool RestoreAt(int index)
    {
        if(index >= Count) return false;
        ref var variable = ref At(index);
        ref var declared = ref Unsafe.Add(ref variable, 1);
        variable = declared;
        return true;
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref A RestoreAt<A>(int index)
        where A : class 
    {
        ref var variable = ref At(index);
        ref var declared = ref Unsafe.Add(ref variable, 1);
        variable = declared;
        return ref Unsafe.As<object, A>(ref variable);
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool RestoreAt<A>(int index, out A value)
        where A : class 
    {
        if (index < Count)
        {
            ref var variable = ref At(index);
            ref var declared = ref Unsafe.Add(ref variable, 1);
            variable = declared;
            value = Unsafe.As<object, A>(ref variable);
            return true;
        }
        else
        {
            value = null!;
            return false;
        }
    }
}
