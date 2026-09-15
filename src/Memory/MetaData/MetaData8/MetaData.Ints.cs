using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct IntMetaData8
{
    [FieldOffset(0)] public int Value0;
    [FieldOffset(4)] public int Value1;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData8(params ReadOnlySpan<int> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData8(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<int> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Value0, 2);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 8);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"({Value0}, {Value1})";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData8(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData8(ReadOnlySpan<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData8(Span<int> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData8(ReadOnlySpan<int> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(IntMetaData8 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(IntMetaData8 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<int>(IntMetaData8 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<int>(IntMetaData8 metaData) =>
        metaData.Values;
}