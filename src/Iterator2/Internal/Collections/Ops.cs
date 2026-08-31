#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Internal.Collections;

/// <summary>
/// Ops is a sequence of `Op` objects. Basically a highly optimised list.
/// Not a stack like the other related types.
/// </summary>
[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit)]
readonly struct Ops
{
    public const int HeaderSize = 8;    // We're using 8 bytes for the header, so that the Ops are 64 bit aligned
    const int MaxCapacity = 16;
    const int PointerSize = 8;
    
    [FieldOffset(0)]
    public readonly int Count;
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
    
    [MethodImpl(Optimisations.Default)]
    public Ops(params ReadOnlySpan<Op> ops)
    {
        var     size = PointerSize * ops.Length;
        ref var dst  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in Op0)!);
        ref var src  = ref Unsafe.As<Op, byte>(ref Unsafe.AsRef(in ops.GetPinnableReference()));
        Unsafe.CopyBlock(ref dst, ref src, (uint)size);
        Count = (short)ops.Length;
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool Run(ref StackFrame stack)
    {
        switch (Count)
        {
            case 0:  return Run0(ref stack);
            case 1:  return Run1(ref stack);
            case 2:  return Run2(ref stack);
            case 3:  return Run3(ref stack);
            case 4:  return Run4(ref stack);
            case 5:  return Run5(ref stack);
            case 6:  return Run6(ref stack);
            case 7:  return Run7(ref stack);
            case 8:  return Run8(ref stack);
            case 9:  return Run9(ref stack);
            case 10: return RunA(ref stack);
            case 11: return RunB(ref stack);
            case 12: return RunC(ref stack);
            case 13: return RunD(ref stack);
            case 14: return RunE(ref stack);
            case 15: return RunF(ref stack);
            default: throw new InvalidOperationException("Frame is bigger than the storage!");
        }
        
         /*
         //Looped instead of switched

        ref var pc = ref Unsafe.AsRef(in Op0);
        ref var end = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * Count);

        while (!Unsafe.AreSame(ref pc, ref end))
        {
            if (!pc.Run(ref frame))
            {
                return false;
            }
            pc = ref Unsafe.AddByteOffset(ref pc, PointerSize);
        }
        return true;
        */
    }

    [MethodImpl(Optimisations.Default)]
    static bool RunOp(ref StackFrame stack, ref Op op)
    {
        var cont = op.Run(ref stack);
        op = ref Unsafe.AddByteOffset(ref op, PointerSize);
        return cont;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run0(ref StackFrame stack) =>
        true;

    [MethodImpl(Optimisations.Default)]
    public bool Run1(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run2(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run3(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run4(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run5(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run6(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run7(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run8(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool Run9(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool RunA(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool RunB(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool RunC(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool RunD(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool RunE(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }

    [MethodImpl(Optimisations.Default)]
    public bool RunF(ref StackFrame stack)
    {
        ref var pc = ref Unsafe.AsRef(in Op0);
        return RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc) && 
               RunOp(ref stack, ref pc);
    }
    
    [MethodImpl(Optimisations.Default)]
    public void Add(in Op op)
    {
        ref var count = ref Unsafe.AsRef(in Count);
        if(count == MaxCapacity) throw new InvalidOperationException("OpFrame is full");
        ref var dest  = ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * count);
        dest = op;
        count++;
    }

    [MethodImpl(Optimisations.Default)]
    public void Clear()
    {
        var size = (uint)(Count * PointerSize + HeaderSize);
        Unsafe.InitBlock(ref Unsafe.As<Ops, byte>(ref Unsafe.AsRef(in this)), 0, size);
    }

    public ref Op this[int index]
    {
        [MethodImpl(Optimisations.Default)]
        get => ref Unsafe.AddByteOffset(ref Unsafe.AsRef(in Op0), PointerSize * index);
    }

    [MethodImpl(Optimisations.Default)]
    public void CopyTo(ref Ops dest)
    {
        var blockSize = PointerSize * Count + HeaderSize;

        Unsafe.CopyBlock(ref Unsafe.As<Ops, byte>(ref Unsafe.AsRef(in dest)),
                         ref Unsafe.As<Ops, byte>(ref Unsafe.AsRef(in this)),
                         (uint)blockSize);
    }
}
