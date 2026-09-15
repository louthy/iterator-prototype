using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct IntMetaData32
{
    [FieldOffset(0)] public int Value0;
    [FieldOffset(4)] public int Value1;
    [FieldOffset(8)] public int Value2;
    [FieldOffset(12)] public int Value3;
    [FieldOffset(16)] public int Value4;
    [FieldOffset(20)] public int Value5;
    [FieldOffset(24)] public int Value6;
    [FieldOffset(28)] public int Value7;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData32(params ReadOnlySpan<int> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData32(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<int> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Value0, 8);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 32);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"({Value0}, {Value1}, {Value2}, {Value3}, {Value4}, {Value5}, {Value6}, {Value7})";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData32(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData32(ReadOnlySpan<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData32(Span<int> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData32(ReadOnlySpan<int> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(IntMetaData32 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(IntMetaData32 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<int>(IntMetaData32 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<int>(IntMetaData32 metaData) =>
        metaData.Values;
}