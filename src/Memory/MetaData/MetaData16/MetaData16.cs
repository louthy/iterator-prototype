using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct MetaData16
{
    [FieldOffset(0)]
    public readonly ByteMetaData16 Bytes;
    
    [FieldOffset(0)]
    public readonly CharMetaData16 Chars;
    
    [FieldOffset(0)]
    public readonly IntMetaData16 Ints;    
    
    [FieldOffset(0)]
    public readonly StringMetaData16 String;
}
