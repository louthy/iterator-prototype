#pragma warning disable CS0649
#pragma warning disable CS8618 
#pragma warning disable CS0169
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
struct GlobalsMutable
{
    public ObjStack2 objs;
    public ByteList2 values;
}

