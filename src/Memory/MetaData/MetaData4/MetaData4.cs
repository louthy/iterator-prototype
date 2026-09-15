using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct MetaData4
{
    [FieldOffset(0)]
    public readonly ByteMetaData4 Bytes;
    
    [FieldOffset(0)]
    public readonly CharMetaData4 Chars;
    
    [FieldOffset(0)]
    public readonly IntMetaData4 Ints;    
    
    [FieldOffset(0)]
    public readonly StringMetaData4 String;
}
