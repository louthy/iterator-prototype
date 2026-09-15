using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct MetaData8
{
    [FieldOffset(0)]
    public readonly ByteMetaData8 Bytes;
    
    [FieldOffset(0)]
    public readonly CharMetaData8 Chars;
    
    [FieldOffset(0)]
    public readonly IntMetaData8 Ints;    
    
    [FieldOffset(0)]
    public readonly StringMetaData8 String;
}
