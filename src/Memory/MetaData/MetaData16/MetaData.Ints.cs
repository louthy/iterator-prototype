using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct IntMetaData16
{
    [FieldOffset(0)] public int Value0;
    [FieldOffset(4)] public int Value1;
    [FieldOffset(8)] public int Value2;
    [FieldOffset(12)] public int Value3;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData16(params ReadOnlySpan<int> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData16(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<int> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Value0, 4);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 16);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"({Value0}, {Value1}, {Value2}, {Value3})";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData16(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData16(ReadOnlySpan<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData16(Span<int> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData16(ReadOnlySpan<int> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(IntMetaData16 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(IntMetaData16 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<int>(IntMetaData16 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<int>(IntMetaData16 metaData) =>
        metaData.Values;
}