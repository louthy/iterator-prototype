#pragma warning disable CS8618 
#pragma warning disable CS0169
#pragma warning disable CS0649
// ReSharper disable UnassignedReadonlyField

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratorPrototype.Iterator3.Internal.Collections;

[SkipLocalsInit]
struct TopsMutable
{
    public uint item0;
    public uint item1;
    public uint item2;
    public uint item3;
    public uint item4;
    public uint item5;
    public uint item6;
    public uint item7;
    public uint item8;
    public uint item9;
    public uint itemA;
    public uint itemB;
    public uint itemC;
    public uint itemD;
    public uint itemE;
    public uint itemF;
    public uint Current;
    public uint Begin;
    public int Count;

    [MethodImpl(Optimisations.InliningOnly)]
    public TopsMutable()
    {
        Count = 1;
        Begin = 0;
        Current = 0;
    }
}
