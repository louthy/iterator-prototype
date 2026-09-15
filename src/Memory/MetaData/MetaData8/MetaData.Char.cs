using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct CharMetaData8
{
    [FieldOffset(0)] public char Char0;
    [FieldOffset(2)] public char Char1;
    [FieldOffset(4)] public char Char2;
    [FieldOffset(6)] public char Char3;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public CharMetaData8(params ReadOnlySpan<char> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public CharMetaData8(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<char> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Char0, 4);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 8);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        new (Values);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData8(Span<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData8(ReadOnlySpan<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData8(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData8(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(CharMetaData8 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(CharMetaData8 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<char>(CharMetaData8 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<char>(CharMetaData8 metaData) =>
        metaData.Values;
}