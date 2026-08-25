using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
public readonly unsafe struct Ops
{
    public const int Capacity = 32;
    public readonly int Count;
    public readonly nint Ptr00;
    public readonly nint Ptr01;
    public readonly nint Ptr02;
    public readonly nint Ptr03;
    public readonly nint Ptr04;
    public readonly nint Ptr05;
    public readonly nint Ptr06;
    public readonly nint Ptr07;
    public readonly nint Ptr08;
    public readonly nint Ptr09;
    public readonly nint Ptr0A;
    public readonly nint Ptr0B;
    public readonly nint Ptr0C;
    public readonly nint Ptr0D;
    public readonly nint Ptr0E;
    public readonly nint Ptr0F;
    public readonly nint Ptr10;
    public readonly nint Ptr11;
    public readonly nint Ptr12;
    public readonly nint Ptr13;
    public readonly nint Ptr14;
    public readonly nint Ptr15;
    public readonly nint Ptr16;
    public readonly nint Ptr17;
    public readonly nint Ptr18;
    public readonly nint Ptr19;
    public readonly nint Ptr1A;
    public readonly nint Ptr1B;
    public readonly nint Ptr1C;
    public readonly nint Ptr1D;
    public readonly nint Ptr1E;
    public readonly nint Ptr1F;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref Ops dest)
    {
        var     sizeOf = (uint)(Unsafe.SizeOf<nint>() * Count);
        ref var d      = ref Unsafe.As<nint, byte>(ref Unsafe.AsRef(in dest.Ptr00));
        ref var s      = ref Unsafe.As<nint, byte>(ref Unsafe.AsRef(in Ptr00));
        Unsafe.CopyBlock(ref d, in s, sizeOf);
        ref var dtop = ref Unsafe.AsRef(in dest.Count);
        dtop = Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Clear()
    {
        ref var self = ref Unsafe.AsRef(in this);
        self = default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Add(in delegate*<ref StackFrame, bool> value)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Ptr00), count);
        entry = (nint)value;
        count++;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run(ref StackFrame frame)
    {
        var     count = Count;
        ref var ptr   = ref Unsafe.AsRef(in Ptr00);
        
        for(var i = 0; i < count; i++)
        {
            var op = (delegate*<ref StackFrame, bool>)ptr;
            if(!op(ref frame)) return false;
            ptr = ref Unsafe.Add(ref ptr, 1);
        }
        return true;
    }
}
