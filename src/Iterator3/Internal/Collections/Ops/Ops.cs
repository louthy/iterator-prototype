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
    public readonly bool IsRunnable;
    public readonly int Count;
    readonly Op Fun00;
    readonly Op Fun01;
    readonly Op Fun02;
    readonly Op Fun03;
    readonly Op Fun04;
    readonly Op Fun05;
    readonly Op Fun06;
    readonly Op Fun07;
    readonly Op Fun08;
    readonly Op Fun09;
    readonly Op Fun0A;
    readonly Op Fun0B;
    readonly Op Fun0C;
    readonly Op Fun0D;
    readonly Op Fun0E;
    readonly Op Fun0F;
    readonly Op Fun10;
    readonly Op Fun11;
    readonly Op Fun12;
    readonly Op Fun13;
    readonly Op Fun14;
    readonly Op Fun15;
    readonly Op Fun16;
    readonly Op Fun17;
    readonly Op Fun18;
    readonly Op Fun19;
    readonly Op Fun1A;
    readonly Op Fun1B;
    readonly Op Fun1C;
    readonly Op Fun1D;
    readonly Op Fun1E;
    readonly Op Fun1F;

    public ref Op Block(int from, out int count)
    {
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
        return ref start;
    }
    
    public ref readonly Op this[int index]
    {
        [MethodImpl(Optimisations.Max)]
        get => ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), index);
    }

    public ref readonly Op this[uint index]
    {
        [MethodImpl(Optimisations.Max)]
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
        ref var framesR     = ref Unsafe.AsRef(in Frames);
        ref var isRunnable  = ref Unsafe.AsRef(in IsRunnable);
        ref var entry       = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), count);

        varBytes    = (short)Math.Clamp(VarBytes + varBytesOut - varBytesIn, 0, short.MaxValue);
        maxVarBytes = Math.Max(VarBytes, MaxVarBytes);

        varObjs    = (byte)Math.Clamp(VarObjs + varObjsOut - varObjsIn, 0, 255);
        maxVarObjs = Math.Max(VarObjs, MaxVarObjs);

        framesR = (byte)Math.Clamp(Frames + (@return == OpReturn.CoRoutine ? 1 : 0x0), 0, 255);
        
        isRunnable = maxVarBytes < ByteStack.Capacity &&
                     maxVarObjs  < ObjStack.Capacity  &&
                     framesR     < Tops.Capacity;
        
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
        ref var framesR     = ref Unsafe.AsRef(in Frames);
        ref var isRunnable  = ref Unsafe.AsRef(in IsRunnable);
        ref var start       = ref Unsafe.AsRef(in Fun00);
        ref var next        = ref Unsafe.Add(ref start, 1);
        
        Unsafe.CopyBlock(
            ref Unsafe.As<Op, byte>(ref next), 
            ref Unsafe.As<Op, byte>(ref start), 
            (uint)(Unsafe.SizeOf<Op>() * count));

        varBytes    = (short)Math.Clamp(VarBytes + varBytesOut - varBytesIn, 0, short.MaxValue);
        maxVarBytes = Math.Max(VarBytes, MaxVarBytes);

        varObjs    = (byte)Math.Clamp(VarObjs + varObjsOut - varObjsIn, 0, 255);
        maxVarObjs = Math.Max(VarObjs, MaxVarObjs);

        framesR = (byte)Math.Clamp(Frames + (@return == OpReturn.CoRoutine ? 1 : 0x0), 0, 255);
        
        isRunnable = maxVarBytes < ByteStack.Capacity &&
                     maxVarObjs  < ObjStack.Capacity  &&
                     framesR     < Tops.Capacity;
        
        start = new Op((nint)f, @return);
        count++;
        return true;
    }
}
