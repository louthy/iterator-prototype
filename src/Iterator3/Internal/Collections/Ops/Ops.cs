// ReSharper disable UnassignedReadonlyField
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly unsafe struct Ops
{
    public const int Capacity = 32;
    
    public readonly short VarBytes;
    public readonly short MaxVarBytes;
    public readonly byte VarObjs;
    public readonly byte MaxVarObjs;
    public readonly byte Frames;
    public readonly byte BlockSize;
    public readonly bool IsRunnable;
    public readonly int Count;
    
    readonly Op Fun00, Fun01, Fun02, Fun03, Fun04, Fun05, Fun06, Fun07,
                Fun08, Fun09, Fun0A, Fun0B, Fun0C, Fun0D, Fun0E, Fun0F,
                Fun10, Fun11, Fun12, Fun13, Fun14, Fun15, Fun16, Fun17,
                Fun18, Fun19, Fun1A, Fun1B, Fun1C, Fun1D, Fun1E, Fun1F; 
    
    readonly byte Blk00, Blk01, Blk02, Blk03, Blk04, Blk05, Blk06, Blk07, 
                  Blk08, Blk09, Blk0A, Blk0B, Blk0C, Blk0D, Blk0E, Blk0F,
                  Blk10, Blk11, Blk12, Blk13, Blk14, Blk15, Blk16, Blk17, 
                  Blk18, Blk19, Blk1A, Blk1B, Blk1C, Blk1D, Blk1E, Blk1F;
    
    // This is because the writing of the block-sizes can write 8 bytes
    // at a time, with up to 3 padded overflow bytes. So, we put this
    // buffer here in case of any overflow.
    readonly uint blkBuffer;

    [MethodImpl(Optimisations.InliningOnly)]
    public Span<Op> BlockSpan(int from)
    {
        var count = Unsafe.AddByteOffset(ref Unsafe.AsRef(in Blk00), from);
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), from), count);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public ref Op Block(int from, out int count)
    {
        count = Unsafe.AddByteOffset(ref Unsafe.AsRef(in Blk00), from);
        return ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), from);
        
        /*
        if (from >= Count)
        {
            count = 0;
            return ref Unsafe.NullRef<Op>();
        }

        ref var start   = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), from);
        ref var current = ref start;

        for (var i = from; i < Count; i++)
        {
            if (current.Return is OpReturn.CanVoid)
            {
                count = i - from + 1;
                return ref start;
            }

            current = ref Unsafe.Add(ref Unsafe.AsRef(in current), 1);
        }
        count = Count - from;
        return ref start;*/
    }
    
    public ref readonly Op this[int index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), index);
    }

    public ref readonly Op this[uint index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), index);
    }
    
    [MethodImpl(Optimisations.Default)]
    public bool Add(
        IterOp f, 
        int varBytesIn, 
        int varBytesOut, 
        int varObjsIn, 
        int varObjsOut, 
        OpReturn @return)
    {
        if (Count + 1 > Capacity) return false;
        ref var count       = ref Unsafe.AsRef(in Count);
        ref var varBytes    = ref Unsafe.AsRef(in VarBytes);
        ref var maxVarBytes = ref Unsafe.AsRef(in MaxVarBytes);
        ref var varObjs     = ref Unsafe.AsRef(in VarObjs);
        ref var maxVarObjs  = ref Unsafe.AsRef(in MaxVarObjs);
        ref var frames      = ref Unsafe.AsRef(in Frames);
        ref var blockSize   = ref Unsafe.AsRef(in BlockSize);
        ref var block       = ref Unsafe.Add(ref Unsafe.AsRef(in Blk00), count - BlockSize);
        ref var isRunnable  = ref Unsafe.AsRef(in IsRunnable);
        ref var entry       = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), count);

        varBytes    = (short)Math.Clamp(VarBytes + varBytesOut - varBytesIn, 0, short.MaxValue);
        maxVarBytes = Math.Max(VarBytes, MaxVarBytes);

        varObjs    = (byte)Math.Clamp(VarObjs + varObjsOut - varObjsIn, 0, 255);
        maxVarObjs = Math.Max(VarObjs, MaxVarObjs);

        frames = (byte)Math.Clamp(Frames + (@return == OpReturn.CoRoutine ? 1 : 0x0), 0, 255);
        
        isRunnable = maxVarBytes < ByteStack.Capacity &&
                     maxVarObjs  < ObjStack.Capacity  &&
                     frames      < Tops.Capacity;

        blockSize++;
        BlockSizeWriter(ref block, BlockSize);
        if (@return == OpReturn.CanVoid)
        {
            blockSize = 0;
        }
        else if (@return == OpReturn.CoRoutine)
        {
            ref var coRoutineBlock = ref Unsafe.Add(ref Unsafe.AsRef(in Blk00), count);
            coRoutineBlock = blockSize;
        }
        
        entry = new Op((nint)f, @return);
        count++;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Prepend(
        IterOp f, 
        int varBytesIn, 
        int varBytesOut, 
        int varObjsIn, 
        int varObjsOut, 
        OpReturn @return)
    {
        if (Count + 1 > Capacity) return false;
        ref var count       = ref Unsafe.AsRef(in Count);
        ref var varBytes    = ref Unsafe.AsRef(in VarBytes);
        ref var maxVarBytes = ref Unsafe.AsRef(in MaxVarBytes);
        ref var varObjs     = ref Unsafe.AsRef(in VarObjs);
        ref var maxVarObjs  = ref Unsafe.AsRef(in MaxVarObjs);
        ref var frames      = ref Unsafe.AsRef(in Frames);
        ref var blockSize   = ref Unsafe.AsRef(in BlockSize);
        ref var startBlock  = ref Unsafe.AsRef(in Blk00);
        ref var nextBlock   = ref Unsafe.Add(ref startBlock, 1);
        ref var isRunnable  = ref Unsafe.AsRef(in IsRunnable);
        ref var start       = ref Unsafe.AsRef(in Fun00);
        ref var next        = ref Unsafe.Add(ref start, 1);
        
        Unsafe.CopyBlock(ref Unsafe.As<Op, byte>(ref next),
                         ref Unsafe.As<Op, byte>(ref start), 
                         (uint)(Unsafe.SizeOf<Op>() * count));
        
        Unsafe.CopyBlock(ref nextBlock, ref startBlock, (uint)count);

        varBytes    = (short)Math.Clamp(VarBytes + varBytesOut - varBytesIn, 0, short.MaxValue);
        maxVarBytes = Math.Max(VarBytes, MaxVarBytes);

        varObjs    = (byte)Math.Clamp(VarObjs + varObjsOut - varObjsIn, 0, 255);
        maxVarObjs = Math.Max(VarObjs, MaxVarObjs);

        frames = (byte)Math.Clamp(Frames + (@return == OpReturn.CoRoutine ? 1 : 0x0), 0, 255);
        
        isRunnable = maxVarBytes < ByteStack.Capacity &&
                     maxVarObjs  < ObjStack.Capacity  &&
                     frames      < Tops.Capacity;

        if (@return == OpReturn.CanVoid)
        {
            blockSize = 0;
            startBlock = 1;
        }
        else
        {
            startBlock++;
        }
        
        start = new Op((nint)f, @return);
        count++;
        return true;
    }
        
    static void BlockSizeWriter(ref byte block, uint size)
    {
        switch (size)
        {
            case 0: 
                return;
            
            case 1: 
                block = 1;
                return;
            
            case 2:
                ref var block16 = ref Unsafe.As<byte, ushort>(ref block);
                block16 = 0x01_02;
                return;
            
            case 3:
                ref var block24 = ref Unsafe.As<byte, uint>(ref block);
                block24   = 0x00_01_02_03;
                return;
            
            case 4:
                ref var block32 = ref Unsafe.As<byte, uint>(ref block);
                block32 = 0x01_02_03_04;
                return;
            
            case 5:
                ref var block40 = ref Unsafe.As<byte, ulong>(ref block);
                block40 = 0x00_00_00_01_02_03_04_05;
                return;
            
            case 6:
                ref var block48 = ref Unsafe.As<byte, ulong>(ref block);
                block48 = 0x00_00_01_02_03_04_05_06;
                return;
            
            case 7:
                ref var block56 = ref Unsafe.As<byte, ulong>(ref block);
                block56 = 0x00_01_02_03_04_05_06_07;
                return;
            
            case 8:
                ref var block64 = ref Unsafe.As<byte, ulong>(ref block);
                block64 = 0x01_02_03_04_05_06_07_08;
                return;
            
            default:
                Span<byte> sizes = stackalloc byte[]
                {
                    32, 31, 30, 29, 28, 27, 26, 25,
                    24, 23, 22, 21, 20, 19, 18, 17,
                    16, 15, 14, 13, 12, 11, 10, 9,
                    8, 7, 6, 5, 4, 3, 2, 1
                };
                Unsafe.CopyBlock(ref block, ref Unsafe.AddByteOffset(ref sizes.GetPinnableReference(), 32 - size), size); 
                return;

        }
    }
}
