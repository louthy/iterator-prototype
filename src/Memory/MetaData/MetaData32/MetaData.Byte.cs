using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct ByteMetaData32
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
    [FieldOffset(16)] public byte Byte16;
    [FieldOffset(17)] public byte Byte17;
    [FieldOffset(18)] public byte Byte18;
    [FieldOffset(19)] public byte Byte19;
    [FieldOffset(20)] public byte Byte20;
    [FieldOffset(21)] public byte Byte21;
    [FieldOffset(22)] public byte Byte22;
    [FieldOffset(23)] public byte Byte23;
    [FieldOffset(24)] public byte Byte24;
    [FieldOffset(25)] public byte Byte25;
    [FieldOffset(26)] public byte Byte26;
    [FieldOffset(27)] public byte Byte27;
    [FieldOffset(28)] public byte Byte28;
    [FieldOffset(29)] public byte Byte29;
    [FieldOffset(30)] public byte Byte30;
    [FieldOffset(31)] public byte Byte31;

    [MethodImpl(Optimisations.InliningOnly)]
    public ByteMetaData32(params ReadOnlySpan<byte> values) =>
        values.CopyTo(Values);

    public Span<byte> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 32);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"[{Byte0}, {Byte1}, {Byte2}, {Byte3}, {Byte4}, {Byte5}, {Byte6}, {Byte7}, {Byte8}, {Byte9}, {Byte10}, {Byte11}, {Byte12}, {Byte13}, {Byte14}, {Byte15}, {Byte16}, {Byte17}, {Byte18}, {Byte19}, {Byte20}, {Byte21}, {Byte22}, {Byte23}, {Byte24}, {Byte25}, {Byte26}, {Byte27}, {Byte28}, {Byte29}, {Byte30}, {Byte31}]";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData32(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData32(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(ByteMetaData32 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(ByteMetaData32 metaData) =>
        metaData.Values;
}