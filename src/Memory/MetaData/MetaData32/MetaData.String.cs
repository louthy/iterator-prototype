using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct StringMetaData32
{
    [FieldOffset(0)] public char Char0;
    [FieldOffset(2)] public char Char1;
    [FieldOffset(4)] public char Char2;
    [FieldOffset(6)] public char Char3;
    [FieldOffset(8)] public char Char4;
    [FieldOffset(10)] public char Char5;
    [FieldOffset(12)] public char Char6;
    [FieldOffset(14)] public char Char7;
    [FieldOffset(16)] public char Char8;
    [FieldOffset(18)] public char Char9;
    [FieldOffset(20)] public char Char10;
    [FieldOffset(22)] public char Char11;
    [FieldOffset(24)] public char Char12;
    [FieldOffset(26)] public char Char13;
    [FieldOffset(28)] public char Char14;
    [FieldOffset(30)] public char Char15;
    
    [FieldOffset(0)] byte Byte0;

    [MethodImpl(Optimisations.InliningOnly)]
    public StringMetaData32(string value) : this(value.AsSpan())
    {
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public StringMetaData32(params ReadOnlySpan<char> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public StringMetaData32(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);

    public string Value
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => new (Values);
    }

    public Span<char> Values
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Char0, 16);
    }

    public Span<byte> Bytes
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => MemoryMarshal.CreateSpan(ref Byte0, 32);
    }

    [MethodImpl(Optimisations.InliningOnly)]
    public override string ToString() =>
        Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData32(string metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData32(Span<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData32(ReadOnlySpan<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData32(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData32(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(StringMetaData32 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(StringMetaData32 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<char>(StringMetaData32 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<char>(StringMetaData32 metaData) =>
        metaData.Values;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator string(StringMetaData32 metaData) =>
        metaData.Value;
}