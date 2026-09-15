using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct ByteMetaData4
{
    [FieldOffset(0)] public byte Byte0;
    [FieldOffset(1)] public byte Byte1;
    [FieldOffset(2)] public byte Byte2;
    [FieldOffset(3)] public byte Byte3;

    [MethodImpl(Optimisations.InliningOnly)]
    public ByteMetaData4(params ReadOnlySpan<byte> values) =>
        values.CopyTo(Values);

    public Span<byte> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 4);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"[{Byte0}, {Byte1}, {Byte2}, {Byte3}]";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData4(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ByteMetaData4(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(ByteMetaData4 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(ByteMetaData4 metaData) =>
        metaData.Values;
}