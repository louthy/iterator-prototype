using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct ByteMetaData8
{
    [FieldOffset(0)] public byte Byte0;
    [FieldOffset(1)] public byte Byte1;
    [FieldOffset(2)] public byte Byte2;
    [FieldOffset(3)] public byte Byte3;
    [FieldOffset(4)] public byte Byte4;
    [FieldOffset(5)] public byte Byte5;
    [FieldOffset(6)] public byte Byte6;
    [FieldOffset(7)] public byte Byte7;

    [MethodImpl(Optimisations.InliningOnly)]
    public ByteMetaData8(params ReadOnlySpan<byte> values) =>
        values.CopyTo(Values);

    public Span<byte> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 8);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"[{Byte0}, {Byte1}, {Byte2}, {Byte3}, {Byte4}, {Byte5}, {Byte6}, {Byte7}]";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData8(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData8(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(ByteMetaData8 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(ByteMetaData8 metaData) =>
        metaData.Values;
}