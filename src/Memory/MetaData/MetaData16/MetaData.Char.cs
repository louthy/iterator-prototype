using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct CharMetaData16
{
    [FieldOffset(0)] public char Char0;
    [FieldOffset(2)] public char Char1;
    [FieldOffset(4)] public char Char2;
    [FieldOffset(6)] public char Char3;
    [FieldOffset(8)] public char Char4;
    [FieldOffset(10)] public char Char5;
    [FieldOffset(12)] public char Char6;
    [FieldOffset(14)] public char Char7;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public CharMetaData16(params ReadOnlySpan<char> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public CharMetaData16(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<char> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Char0, 8);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 16);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        new (Values);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData16(Span<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData16(ReadOnlySpan<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData16(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData16(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(CharMetaData16 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(CharMetaData16 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<char>(CharMetaData16 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<char>(CharMetaData16 metaData) =>
        metaData.Values;
}