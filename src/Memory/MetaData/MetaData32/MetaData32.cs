using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Memory;

[SkipLocalsInit]
[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct MetaData32
{
    [FieldOffset(0)]
    public readonly ByteMetaData32 Bytes;
    
    [FieldOffset(0)]
    public readonly CharMetaData32 Chars;
    
    [FieldOffset(0)]
    public readonly IntMetaData32 Ints;    
    
    [FieldOffset(0)]
    public readonly StringMetaData32 String;
}
