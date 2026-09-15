using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct IntMetaData4
{
    [FieldOffset(0)] public int Value;
    
    [FieldOffset(0)] byte Byte0;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData4(params ReadOnlySpan<int> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public IntMetaData4(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<int> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Value, 1);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 4);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        $"{Value}";

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData4(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData4(ReadOnlySpan<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData4(Span<int> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator IntMetaData4(ReadOnlySpan<int> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(IntMetaData4 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(IntMetaData4 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<int>(IntMetaData4 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<int>(IntMetaData4 metaData) =>
        metaData.Values;
}