using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

static class OpsMutableExtensions
{
   extension(in OpsMutable ops)
    {
        [MethodImpl(Optimisations.InliningOnly)]
        public Span<Op> Block(uint from)
        {
            var count = Unsafe.AddByteOffset(ref Unsafe.AsRef(in ops.Blk00), from);
            return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in ops.Fun00), from), count);
        }
    }    
    
    extension(ref OpsMutable ops)
    {
        [MethodImpl(Optimisations.Default)]
        public unsafe bool Add(
            IterOp f,
            int varBytesIn,
            int varBytesOut,
            int varObjsIn,
            int varObjsOut,
            OpReturn @return)
        {
            if (ops.Count + 1 > Ops.Capacity) return false;
            
            var blockSpan  = MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in ops.Blk00), 0), Ops.Capacity);
            var entrySpan  = MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in ops.Fun00), 0), Ops.Capacity);
            var blockStart = ops.Count - ops.BlockSize;

            ops.VarBytes = (short)Math.Clamp(ops.VarBytes + varBytesOut - varBytesIn, 0, short.MaxValue);
            ops.MaxVarBytes = Math.Max(ops.VarBytes, ops.MaxVarBytes);

            ops.VarObjs = (byte)Math.Clamp(ops.VarObjs + varObjsOut - varObjsIn, 0, 255);
            ops.MaxVarObjs = Math.Max(ops.VarObjs, ops.MaxVarObjs);

            ops.Frames = (byte)Math.Clamp(ops.Frames + (@return == OpReturn.CoRoutine ? 1 : 0x0), 0, 255);

            ops.IsRunnable = ops is
                             {
                                 MaxVarBytes: < ByteStack.Capacity, 
                                 MaxVarObjs: < ObjStack.Capacity, 
                                 Frames: < Tops.Capacity
                             };

            ops.BlockSize++;

            ReadOnlySpan<byte> sizes = OpsBlocks.BS[ops.BlockSize];
            sizes.CopyTo(blockSpan[blockStart..]);

            if (@return == OpReturn.CanVoid)
            {
                ops.BlockSize = 0;
            }
            else if (@return == OpReturn.CoRoutine)
            {
                blockSpan[ops.Count] = ops.BlockSize;
            }

            entrySpan[ops.Count] = new Op((nint)f, @return);
            ops.Count++;
            
            return true;
        }

        [MethodImpl(Optimisations.Default)]
        public unsafe bool Prepend(
            IterOp f,
            int varBytesIn,
            int varBytesOut,
            int varObjsIn,
            int varObjsOut,
            OpReturn @return)
        {
            if (ops.Count + 1 > Ops.Capacity) return false;

            var blockSpan = MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in ops.Blk00), 0), Ops.Capacity - 1);
            var entrySpan = MemoryMarshal.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in ops.Fun00), 0), Ops.Capacity     - 1);

            blockSpan[..ops.Count].CopyTo(blockSpan[1..]);
            entrySpan[..ops.Count].CopyTo(entrySpan[1..]);

            ops.VarBytes = (short)Math.Clamp(ops.VarBytes + varBytesOut - varBytesIn, 0, short.MaxValue);
            ops.MaxVarBytes = Math.Max(ops.VarBytes, ops.MaxVarBytes);

            ops.VarObjs = (byte)Math.Clamp(ops.VarObjs + varObjsOut - varObjsIn, 0, 255);
            ops.MaxVarObjs = Math.Max(ops.VarObjs, ops.MaxVarObjs);

            ops.Frames = (byte)Math.Clamp(ops.Frames + (@return == OpReturn.CoRoutine ? 1 : 0x0), 0, 255);

            ops.IsRunnable = ops is
                             {
                                 MaxVarBytes: < ByteStack.Capacity, 
                                 MaxVarObjs: < ObjStack.Capacity, 
                                 Frames: < Tops.Capacity
                             };

            if (@return == OpReturn.CanVoid)
            {
                ops.BlockSize = 0;
                blockSpan[0] = 1;
            }
            else
            {
                blockSpan[0] = (byte)(blockSpan[0] + 1);
            }

            entrySpan[0] = new Op((nint)f, @return);
            ops.Count++;
            return true;
        }
    }
}