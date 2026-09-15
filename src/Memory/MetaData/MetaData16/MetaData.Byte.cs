using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct ByteMetaData16
{
    [FieldOffset(0)] public byte Byte0;
    [FieldOffset(1)] public byte Byte1;
    [FieldOffset(2)] public byte Byte2;
    [FieldOffset(3)] public byte Byte3;
    [FieldOffset(4)] public byte Byte4;
    [FieldOffset(5)] public byte Byte5;
    [FieldOffset(6)] public byte Byte6;
    [FieldOffset(7)] public byte Byte7;
    [FieldOffset(8)] public byte Byte8;
    [FieldOffset(9)] public byte Byte9;
    [FieldOffset(10)] public byte Byte10;
    [FieldOffset(11)] public byte Byte11;
    [FieldOffset(12)] public byte Byte12;
    [FieldOffset(13)] public byte Byte13;
    [FieldOffset(14)] public byte Byte14;
    [FieldOffset(15)] public byte Byte15;

    [MethodImpl(Optimisations.InliningOnly)]
    public ByteMetaData16(params ReadOnlySpan<byte> values) =>
        values.CopyTo(Values);

    public Span<byte> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 16);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"[{Byte0}, {Byte1}, {Byte2}, {Byte3}, {Byte4}, {Byte5}, {Byte6}, {Byte7}, {Byte8}, {Byte9}, {Byte10}, {Byte11}, {Byte12}, {Byte13}, {Byte14}, {Byte15}]";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData16(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData16(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(ByteMetaData16 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(ByteMetaData16 metaData) =>
        metaData.Values;
}