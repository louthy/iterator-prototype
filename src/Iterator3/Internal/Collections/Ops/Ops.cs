#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
readonly unsafe struct Ops
{
    public const int Capacity = 32;
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

    public IterOp this[int index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (IterOp)Unsafe.Add(ref Reference, index).Fun;
    }

    public IterOp this[uint index]
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => (IterOp)Unsafe.Add(ref Reference, index).Fun;
    }

    public ref Op Reference
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => ref Unsafe.AsRef(in Fun00);
    } 
    
    [MethodImpl(Optimisations.Default)]
    public bool Add(in IterOp f)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);
        ref var entry = ref Unsafe.Add(ref Unsafe.AsRef(in Fun00), count);
        entry = new Op((nint)f);
        count++;
        return true;
    }

    [MethodImpl(Optimisations.Default)]
    public bool Prepend(in IterOp f)
    {
        if (Count + 1 > Capacity) return false;
        ref var count = ref Unsafe.AsRef(in Count);

        ref var start = ref Unsafe.AsRef(in Fun00);
        ref var next = ref Unsafe.Add(ref start, 1);
        
        Unsafe.CopyBlock(
            ref Unsafe.As<Op, byte>(ref next), 
            ref Unsafe.As<Op, byte>(ref start), 
            (uint)(Unsafe.SizeOf<Op>() * count));
        
        start = new Op((nint)f);
        count++;
        return true;
    }
}
