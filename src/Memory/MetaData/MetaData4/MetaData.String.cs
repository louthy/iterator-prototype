using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct StringMetaData4
{
    [FieldOffset(0)] public char Char0;
    [FieldOffset(2)] public char Char1;
    
    [FieldOffset(0)] byte Byte0;


    [MethodImpl(Optimisations.InliningOnly)]
    public StringMetaData4(string value) : this(value.AsSpan())
    { }

    [MethodImpl(Optimisations.InliningOnly)]
    public StringMetaData4(params ReadOnlySpan<char> values) =>
        values.CopyTo(Values);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public StringMetaData4(ReadOnlySpan<byte> values) =>
        values.CopyTo(Bytes);


    public string Value
    {
        [MethodImpl(Optimisations.InliningOnly)]
        get => new (Values);
    }

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
        Value;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData4(string metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData4(Span<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData4(ReadOnlySpan<char> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData4(Span<byte> metaData) =>
        new(metaData);

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator StringMetaData4(ReadOnlySpan<byte> metaData) =>
        new(metaData);
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<byte>(StringMetaData4 metaData) =>
        metaData.Bytes;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<byte>(StringMetaData4 metaData) =>
        metaData.Bytes;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator Span<char>(StringMetaData4 metaData) =>
        metaData.Values;

    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator ReadOnlySpan<char>(StringMetaData4 metaData) =>
        metaData.Values;
    
    [MethodImpl(Optimisations.InliningOnly)]
    public static implicit operator string(StringMetaData4 metaData) =>
        metaData.Value;
}