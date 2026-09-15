using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct CharMetaData4
{
    [FieldOffset(0)] public char Char0;
    [FieldOffset(2)] public char Char1;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public CharMetaData4(params ReadOnlySpan<char> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public CharMetaData4(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public Span<char> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Char0, 2);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 4);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        new (Values);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData4(Span<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData4(ReadOnlySpan<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData4(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator CharMetaData4(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(CharMetaData4 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(CharMetaData4 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<char>(CharMetaData4 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<char>(CharMetaData4 metaData) =>
        metaData.Values;
}