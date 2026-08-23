using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using LanguageExt.Traits;

namespace IteratorPrototype;

/// <summary>
/// OpFrame is a sequence of Op objects. Basically a highly optimised list.
/// Not a stack like the other related types.
/// </summary>
[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit)]
readonly struct OpFrame
{
    public const uint HeaderSize = 16;
    const int MaxCapacity = 16;
    
    [FieldOffset(0)]
    public readonly object Self;
    [FieldOffset(8)]
    public readonly short PC;
    [FieldOffset(12)]
    public readonly short Count;
    [FieldOffset(16)]
    public readonly Op Op0;
    [FieldOffset(24)]
    public readonly Op Op1;
    [FieldOffset(32)]
    public readonly Op Op2;
    [FieldOffset(40)]
    public readonly Op Op3;
    [FieldOffset(48)]
    public readonly Op Op4;
    [FieldOffset(56)]
    public readonly Op Op5;
    [FieldOffset(64)]
    public readonly Op Op6;
    [FieldOffset(72)]
    public readonly Op Op7;
    [FieldOffset(80)]
    public readonly Op Op8;
    [FieldOffset(88)]
    public readonly Op Op9;
    [FieldOffset(96)]
    public readonly Op OpA;
    [FieldOffset(104)]
    public readonly Op OpB;
    [FieldOffset(112)]
    public readonly Op OpC;
    [FieldOffset(120)]
    public readonly Op OpD;
    [FieldOffset(128)]
    public readonly Op OpE;
    [FieldOffset(136)]
    public readonly Op OpF;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpFrame(object self, params ReadOnlySpan<Op> ops)
    {
        var     size = Unsafe.SizeOf<Op>() * ops.Length;
        ref var dst  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in Op0)!);
        ref var src  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in ops.GetPinnableReference()));
        Unsafe.CopyBlock(ref dst, ref src, (uint)size);
        Count = (short)ops.Length;
        Self = self;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpFrame(object self, short pc, params ReadOnlySpan<Op> ops)
    {
        var     size = Unsafe.SizeOf<Op>() * ops.Length;
        ref var dst  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in Op0)!);
        ref var src  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in ops.GetPinnableReference()));
        Unsafe.CopyBlock(ref dst, ref src, (uint)size);
        Self = self;
        PC = pc;
        Count = (short)ops.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Add(in Op op)
    {
        ref var count = ref Unsafe.AsRef(in Count);
        if(count == MaxCapacity) throw new InvalidOperationException("OpFrame is full");
        ref var dest  = ref Unsafe.Add(ref Unsafe.AsRef(in Op0), count);
        dest = op;
        count++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpFrame AddSafe(in Op op)
    {
        OpFrame frame1 = default;
        CopyTo(ref frame1);
        frame1.Add(op);
        return frame1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Clear()
    {
        var size = (uint)(Count * Unsafe.SizeOf<Op>()) + HeaderSize;
        Unsafe.InitBlock(ref Unsafe.As<OpFrame, byte>(ref Unsafe.AsRef(in this)), 0, size);
    }

    public ref Op AtPC
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Op0), PC); 
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool NextPC(out Op op)
    {
        ref var pc = ref Unsafe.AsRef(in PC);
        if (pc < Count)
        {
            op = AtPC;
            pc++;
            return true;
        }
        else
        {
            op = null!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool NextPC<A>(out Op<A> op)
    {
        ref var pc = ref Unsafe.AsRef(in PC);
        if (pc < Count)
        {
            op = Unsafe.As<Op, Op<A>>(ref AtPC);
            pc++;
            return true;
        }
        else
        {
            op = null!;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void ResetPC()
    {
        ref var pc = ref Unsafe.AsRef(in PC);
        pc = 0;
    }

    public ref Op this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Op0), index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void CopyTo(ref OpFrame dest)
    {
        var blockSize = Unsafe.SizeOf<Op>() * Count + HeaderSize;
        
        Unsafe.CopyBlock(ref Unsafe.As<OpFrame, byte>(ref Unsafe.AsRef(in dest)), 
                         ref Unsafe.As<OpFrame, byte>(ref Unsafe.AsRef(in this)), 
                         (uint)blockSize);
    }
}
