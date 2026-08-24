using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Internal.Collections;

/// <summary>
/// OpFrame is a sequence of Op objects. Basically a highly optimised list.
/// Not a stack like the other related types.
/// </summary>
[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit)]
readonly struct OpFrame
{
    public const int HeaderSize = 16;
    const int MaxCapacity = 16;
    const int PointerSize = 8;
    
    [FieldOffset(0)]
    public readonly short PC;
    [FieldOffset(2)]
    public readonly short Count;
    [FieldOffset(HeaderSize + PointerSize * 0)]
    public readonly Op Op0;
    [FieldOffset(HeaderSize + PointerSize * 1)]
    public readonly Op Op1;
    [FieldOffset(HeaderSize + PointerSize * 2)]
    public readonly Op Op2;
    [FieldOffset(HeaderSize + PointerSize * 3)]
    public readonly Op Op3;
    [FieldOffset(HeaderSize + PointerSize * 4)]
    public readonly Op Op4;
    [FieldOffset(HeaderSize + PointerSize * 5)]
    public readonly Op Op5;
    [FieldOffset(HeaderSize + PointerSize * 6)]
    public readonly Op Op6;
    [FieldOffset(HeaderSize + PointerSize * 7)]
    public readonly Op Op7;
    [FieldOffset(HeaderSize + PointerSize * 8)]
    public readonly Op Op8;
    [FieldOffset(HeaderSize + PointerSize * 9)]
    public readonly Op Op9;
    [FieldOffset(HeaderSize + PointerSize * 10)]
    public readonly Op OpA;
    [FieldOffset(HeaderSize + PointerSize * 11)]
    public readonly Op OpB;
    [FieldOffset(HeaderSize + PointerSize * 12)]
    public readonly Op OpC;
    [FieldOffset(HeaderSize + PointerSize * 13)]
    public readonly Op OpD;
    [FieldOffset(HeaderSize + PointerSize * 14)]
    public readonly Op OpE;
    [FieldOffset(HeaderSize + PointerSize * 15)]
    public readonly Op OpF;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpFrame(params ReadOnlySpan<Op> ops)
    {
        var     size = Unsafe.SizeOf<Op>() * ops.Length;
        ref var dst  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in Op0)!);
        ref var src  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in ops.GetPinnableReference()));
        Unsafe.CopyBlock(ref dst, ref src, (uint)size);
        Count = (short)ops.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public OpFrame(short pc, params ReadOnlySpan<Op> ops)
    {
        var     size = Unsafe.SizeOf<Op>() * ops.Length;
        ref var dst  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in Op0)!);
        ref var src  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in ops.GetPinnableReference()));
        Unsafe.CopyBlock(ref dst, ref src, (uint)size);
        PC = pc;
        Count = (short)ops.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run(ref StackFrame frame)
    {
        switch (Count - PC)
        {
            case 0:  return Run0(ref frame);
            case 1:  return Run1(ref frame);
            case 2:  return Run2(ref frame);
            case 3:  return Run3(ref frame);
            case 4:  return Run4(ref frame);
            case 5:  return Run5(ref frame);
            case 6:  return Run6(ref frame);
            case 7:  return Run7(ref frame);
            case 8:  return Run8(ref frame);
            case 9:  return Run9(ref frame);
            case 10: return RunA(ref frame);
            case 11: return RunB(ref frame);
            case 12: return RunC(ref frame);
            case 13: return RunD(ref frame);
            case 14: return RunE(ref frame);
            case 15: return RunF(ref frame);
            default: throw new InvalidOperationException("Frame is bigger than the storage!");
        }
        
        /*ref var pc  = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        ref var end = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * Count);

        while (!Unsafe.AreSame(ref pc, ref end))
        {
            if (!pc.Run(ref frame))
            {
                return false;
            }
            pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        }
        return true;*/
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run0(ref StackFrame frame) =>
        true;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run1(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run2(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run3(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run4(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run5(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run6(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run7(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run8(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Run9(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool RunA(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool RunB(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool RunC(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool RunD(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool RunE(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool RunF(ref StackFrame frame)
    {
        ref var pc = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * PC);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        if (!pc.Run(ref frame)) return false;
        pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        return pc.Run(ref frame);
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
